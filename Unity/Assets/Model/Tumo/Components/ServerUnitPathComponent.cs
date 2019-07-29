using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ETModel
{
    public class ServerUnitPathComponent : Component
    {
        public List<Vector3> Path = new List<Vector3>();

        public Vector3 ServerPos;
        public Vector3 TargetPos;

        public Vector3 ServerEul;
        public Vector3 TargetEul;

        public CancellationTokenSource moveCancellationTokenSource;
        public CancellationTokenSource turnCancellationTokenSource;

        public void StartMT(M2C_ServerPathResult message)
        {
            ServerPos = new Vector3(message.X, message.Y, message.Z);
            TargetPos = new Vector3(message.Xs[0], message.Ys[0], message.Zs[0]);

            //ServerEul = new Vector3(0, message.W, 0);
            //TargetEul = new Vector3(0, message.Ws[0], 0);

            StartTurn(message.Ws[0]);
            StartMove().Coroutine();
        }

        public void StartTurn(float targetAngles)
        {
            // 取消之前的移动协程
            this.turnCancellationTokenSource?.Cancel();
            this.turnCancellationTokenSource = new CancellationTokenSource();

            //await StartTurn(this.turnCancellationTokenSource.Token);
            StartTurn(targetAngles, this.turnCancellationTokenSource.Token);

            this.turnCancellationTokenSource.Dispose();
            this.turnCancellationTokenSource = null;
        }

        public void StartTurn(float targetAngles, CancellationToken cancellationToken)
        {
            Vector3 ve = this.TargetEul;

            float speed = 5;

            // 矫正移动速度
            Vector3 clientEul = this.GetParent<Unit>().GameObject.transform.eulerAngles;
            float serverf = (ServerEul - ve).magnitude;
            if (Math.Abs(serverf) > 0.1f)
            {
                float clientf = (clientEul - ve).magnitude;
                speed = clientf / serverf * speed;
            }

            this.Entity.GetComponent<TurnComponent>().TurnAngles(targetAngles, cancellationToken, speed);

            //await this.Entity.GetComponent<TurnComponent>().Turn(ve.y, speed, cancellationToken);
        }
       
        //public void StartTurn(CancellationToken cancellationToken)
        //{
        //    Vector3 ve = this.TargetEul;

        //    float speed = 5;

        //    // 矫正移动速度
        //    Vector3 clientEul = this.GetParent<Unit>().GameObject.transform.eulerAngles;
        //    float serverf = (ServerEul - ve).magnitude;
        //    if (serverf > 0.1f)
        //    {
        //        float clientf = (clientEul - ve).magnitude;
        //        speed = clientf / serverf * speed;
        //    }

        //    this.Entity.GetComponent<TurnComponent>().Turn(ve.y, speed);
        //    //await this.Entity.GetComponent<TurnComponent>().Turn(ve.y, speed, cancellationToken);
        //}



        public async ETVoid StartMove()
        {
            // 取消之前的移动协程
            this.moveCancellationTokenSource?.Cancel();
            this.moveCancellationTokenSource = new CancellationTokenSource();
            await StartMove(this.moveCancellationTokenSource.Token);
            this.moveCancellationTokenSource.Dispose();
            this.moveCancellationTokenSource = null;
        }
        public async ETTask StartMove(CancellationToken cancellationToken)
        {
            Vector3 v = this.TargetPos;

            float speed = 5;

            // 矫正移动速度
            Vector3 clientPos = this.GetParent<Unit>().Position;
            float serverf = (ServerPos - v).magnitude;
            if (serverf > 0.1f)
            {
                float clientf = (clientPos - v).magnitude;
                speed = clientf / serverf * speed;
            }

            await this.Entity.GetComponent<MoveComponent>().MoveToAsync(v, speed, cancellationToken);
        }


        #region

        //public async ETVoid StartMove(M2C_ServerPathResult message)
        //{
        //    //this.GetParent<Unit>().GameObject.transform.eulerAngles = Vector3.Lerp(new Vector3(0, message.W, 0), new Vector3(0, message.Ws[0], 0), 1f);
        //    // 取消之前的移动协程
        //    this.moveCancellationTokenSource?.Cancel();
        //    this.moveCancellationTokenSource = new CancellationTokenSource();

        //    this.Path.Clear();
        //    for (int i = 0; i < message.Xs.Count; ++i)
        //    {
        //        this.Path.Add(new Vector3(message.Xs[i], message.Ys[i], message.Zs[i]));
        //    }

        //    ServerPos = new Vector3(message.X, message.Y, message.Z);
        //    TargetPos = new Vector3(message.Xs[0], message.Ys[0], message.Zs[0]);

        //    ServerEul = new Vector3(0, message.W, 0);
        //    TargetEul = new Vector3(0, message.Ws[0], 0);


        //    await StartMove(this.moveCancellationTokenSource.Token);
        //    this.moveCancellationTokenSource.Dispose();
        //    this.moveCancellationTokenSource = null;
        //}

        //public async ETTask StartMove(CancellationToken cancellationToken)
        //{
        //    Unit unit = this.GetParent<Unit>();
        //    for (int i = 0; i < this.Path.Count; ++i)
        //    {
        //        Vector3 v = this.Path[i];

        //        float speed = 5;

        //        if (i == 0)
        //        {
        //            // 矫正移动速度
        //            Vector3 clientPos = this.GetParent<Unit>().Position;
        //            float serverf = (ServerPos - v).magnitude;
        //            if (serverf > 0.1f)
        //            {
        //                float clientf = (clientPos - v).magnitude;
        //                speed = clientf / serverf * speed;
        //            }
        //        }

        //        //this.Entity.GetComponent<TurnComponent>().Turn(v);

        //        await this.Entity.GetComponent<MoveComponent>().MoveToAsync(v, speed, cancellationToken);
        //    }
        //    //this.Entity.GetComponent<TurnComponent>().Turn(v);

        //    //this.GetParent<Unit>().GameObject.transform.eulerAngles = Vector3.Lerp(ServerEul, TargetEul, 1f);

        //}


        #endregion


    }
}

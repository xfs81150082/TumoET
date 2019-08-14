using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ETModel
{
    public class UnitAnglersComponent : Component
    {
        public List<Vector3> Path = new List<Vector3>();

        public Vector3 ServerEul;

        public CancellationTokenSource CancellationTokenSource;

        public async ETVoid StartTurn(M2C_KeyboardDirection message)
        {
            Debug.Log(" UnitAnglersComponent-21-angles: " + message.Y + " / " + message.Ys[0]);

            // 取消之前的移动协程
            this.CancellationTokenSource?.Cancel();
            this.CancellationTokenSource = new CancellationTokenSource();

            this.Path.Clear();
            for (int i = 0; i < message.Xs.Count; ++i)
            {
                this.Path.Add(new Vector3(message.Xs[i], message.Ys[i], message.Zs[i]));
            }
            ServerEul = new Vector3(message.X, message.Y, message.Z);

            await StartTurn(this.CancellationTokenSource.Token);
            this.CancellationTokenSource.Dispose();
            this.CancellationTokenSource = null;
        }

        public async ETTask StartTurn(CancellationToken cancellationToken)
        {
            for (int i = 0; i < this.Path.Count; ++i)
            {
                Vector3 targetEul = this.Path[i];

                float speed = 0;

                if (i == 0)
                {
                    // 矫正移动速度
                    Vector3 clientEul = this.GetParent<Unit>().GameObject.transform.eulerAngles;
                    float serverAngles = Math.Abs(ServerEul.y - targetEul.y);
                    if (serverAngles > 0.1f)
                    {
                        float clientAngles = Math.Abs(clientEul.y - targetEul.y);
                        speed = clientAngles / serverAngles;
                    }
                }
                Debug.Log(" UnitAnglersComponent-58-angles/speed: " + targetEul.y + " / " + speed);

                await this.Entity.GetComponent<TurnEulerAnglesComponent>().TurnToAsync(targetEul, speed, cancellationToken);
            }
        }



    }
}

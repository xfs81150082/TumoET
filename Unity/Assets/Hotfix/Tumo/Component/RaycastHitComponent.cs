using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class RaycastHitComponentAwakeSystem : AwakeSystem<RaycastHitComponent>
    {
        public override void Awake(RaycastHitComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class RaycastHitComponentUpdateSystem : UpdateSystem<RaycastHitComponent>
    {
        public override void Update(RaycastHitComponent self)
        {
            self.Update();           
        }
    }

    public class RaycastHitComponent : Component
    {
        public Vector3 ClickPoint;

        public int mapMask;

        public void Awake()
        {
            this.mapMask = LayerMask.GetMask("Unit");
        }

        private readonly Frame_ClickMap frameClickMap = new Frame_ClickMap();

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, this.mapMask))
                {
                    if (hit.transform.GetComponent<ComponentView>().Component != null)
                    {
                        Unit unit = hit.transform.GetComponent<ComponentView>().Component as Unit;

                        Debug.Log("  hitTarget: " + unit.Id + ".  mapMask: " + this.mapMask);

                        ///鼠标选择目标 发消息 给服务端
                        this.RayHit(unit).Coroutine();
                    }
                }
            }
        }

        public async ETVoid RayHit(Unit unit)
        {
            try
            {
                M2C_RaycastHitActorResponse response = (M2C_RaycastHitActorResponse)await SessionComponent.Instance.Session.Call(
                        new C2M_RaycastHitActorRequest() { Info = unit.Id.ToString() });

                Debug.Log("  RaycastHitComponent: " + response.Message);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}

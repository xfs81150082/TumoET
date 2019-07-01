using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class CameraComponentAwakeSystem : AwakeSystem<CameraComponent ,Unit>
    {
        public override void Awake(CameraComponent self, Unit a)
        {
            self.Awake(a);
        }
    }  

    [ObjectSystem]
	public class CameraComponentLateUpdateSystem : LateUpdateSystem<CameraComponent>
	{
		public override void LateUpdate(CameraComponent self)
		{
			self.LateUpdate();
		}
	}

	public class CameraComponent : Component
	{
        // 战斗摄像机
        private Camera mainCamera;
        private Unit playerUnit;
        public Camera MainCamera
		{
			get
			{
				return this.mainCamera;
			}
		}

        // 摄像机跟随参数
        private float distance = 8f;
        private float disSpeed = 5f;
        private float TargetHeight = 1.5f;
        private float x = 0.0f;
        private float y = 0.0f;
        private Vector3 angles = Vector3.zero;

        public void Awake(Unit player)
        {
            this.mainCamera = Camera.main;
            this.playerUnit = player;
            Init();
        }

        public void LateUpdate()
        {
            //得到 PlayerUnit
            //GetPlayerUnit();

            // 摄像机每帧更新位置
            UpdatePosition();

            // 摄像机远近
            GetDistance();

            ///摄像机水平角度
            ViewGigAndLittle();
        }

        // 初始化参数
        private void Init()
        {
            angles = Camera.main.gameObject.transform.eulerAngles;
            x = angles.x;
            y = angles.y;
            x = 30;   
        }

        private void GetPlayerUnit()
        {
            if (UnitComponent.Instance != null && UnitComponent.Instance.MyUnit != null)
            {
                playerUnit = UnitComponent.Instance.MyUnit;
            }
        }

        // 摄像机每帧更新位置
        private void UpdatePosition()
        {
            if (playerUnit != null)
            {
                //Vector3 cameraPos = this.mainCamera.transform.position;
                //this.mainCamera.transform.position = new Vector3(this.Unit.Position.x, cameraPos.y, this.Unit.Position.z - 10);

                y = playerUnit.GameObject.transform.eulerAngles.y;
                Quaternion rotation = Quaternion.Euler(x, y, 0);
                mainCamera.transform.rotation = Quaternion.Euler(x, y, 0);

                Vector3 position = playerUnit.GameObject.transform.position - (rotation * Vector3.forward * distance + new Vector3(0, -TargetHeight, 0));
                mainCamera.transform.position = position;
            }
        }

        // 摄像机远近
        private void GetDistance()
        {
            distance -= Input.GetAxis("Mouse ScrollWheel") * disSpeed;
            distance = Mathf.Clamp(distance, -2, 18);
        }

        ///摄像机水平角度
        private void ViewGigAndLittle()
        {
            if (Input.GetKeyDown("9"))
            {
                x = 45;
            }
            if (Input.GetKeyDown("8"))
            {
                x = 30;
            }
            if (Input.GetKeyDown("7"))
            {
                x = 15;
            }
        }

    }
}

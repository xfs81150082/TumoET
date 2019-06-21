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
        private Unit Unit;
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
            //this.mainCamera = Camera.main;
            this.Unit = player;
            Init();
        }

        public void LateUpdate()
        {
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
            this.mainCamera = Camera.main;
            angles = Camera.main.gameObject.transform.eulerAngles;
            x = angles.x;
            y = angles.y;
            x = 30;   
        } 

        // 摄像机每帧更新位置
        private void UpdatePosition()
        {
            if (Unit != null)
            {
                //Vector3 cameraPos = this.mainCamera.transform.position;
                //this.mainCamera.transform.position = new Vector3(this.Unit.Position.x, cameraPos.y, this.Unit.Position.z - 10);

                y = Unit.GameObject.transform.eulerAngles.y;
                Quaternion rotation = Quaternion.Euler(x, y, 0);
                mainCamera.transform.rotation = Quaternion.Euler(x, y, 0);

                Vector3 position = Unit.GameObject.transform.position - (rotation * Vector3.forward * distance + new Vector3(0, -TargetHeight, 0));
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

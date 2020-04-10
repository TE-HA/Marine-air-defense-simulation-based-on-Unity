using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class free : MonoBehaviour
{
    private GameObject km_main;
    public Transform tourCamera;
    #region 相机移动参数
    public float moveSpeed = 1.0f;
    public float rotateSpeed = 90.0f;
    public float shiftRate = 2.0f;// 按住Shift加速
    public float minDistance = 0.5f;// 相机离不可穿过的表面的最小距离（小于等于0时可穿透任何表面）
    #endregion
    #region 运动速度和其每个方向的速度分量
    private Vector3 direction = Vector3.zero;
    private Vector3 speedForward;
    private Vector3 speedBack;
    private Vector3 speedLeft;
    private Vector3 speedRight;
    private Vector3 speedUp;
    private Vector3 speedDown;
    private Camera m_camera;
    #endregion

    // Use this for initialization
    void Start()
    {
        if (tourCamera == null) tourCamera = gameObject.transform;
        km_main = GameObject.Find("km_main");
    }

    private void GetDirection()
    {

        #region 加速移动
        if (Input.GetKeyDown(KeyCode.LeftShift)) moveSpeed *= shiftRate;
        if (Input.GetKeyUp(KeyCode.LeftShift)) moveSpeed /= shiftRate;
        #endregion
        #region 键盘移动
        // 复位
        speedForward = Vector3.zero;
        speedBack = Vector3.zero;
        speedLeft = Vector3.zero;
        speedRight = Vector3.zero;
        speedUp = Vector3.zero;
        speedDown = Vector3.zero;
        // 获取按键输入
        //Debug.Log(camera.GetComponent<Camera>().depth);
        if (Input.mousePosition.y <= 10) speedForward = -tourCamera.up;
        if (Input.mousePosition.y > Screen.height - 10) speedBack = tourCamera.up;
        if (Input.mousePosition.x <= 30) speedLeft = -tourCamera.right;
        if (Input.mousePosition.x > Screen.width - 30) speedRight = tourCamera.right;


        if (Input.GetKey(KeyCode.Z))
        {


            float x = km_main.transform.position.x;
            float z = km_main.transform.position.z;

            Vector3 point = new Vector3(x, 400f, z);
            //gameObject.transform.Translate(point);

            float step = 500f * Time.deltaTime;
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, point, step);
            gameObject.transform.forward = Vector3.down;
        }
        float wheel = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 10000;

        //改变相机的位置
        tourCamera.transform.Translate(Vector3.forward * wheel);
        direction = (speedForward + speedBack + speedLeft + speedRight) * 100;
        #endregion
        #region 鼠标旋转
        if (Input.GetMouseButton(0))
        {
            // 转相机朝向
            tourCamera.RotateAround(tourCamera.position, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime);
            tourCamera.RotateAround(tourCamera.position, tourCamera.right, -Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime);
            // 转运动速度方向
            direction = V3RotateAround(direction, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime);
            direction = V3RotateAround(direction, tourCamera.right, -Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime);
        }

        #endregion
    }
    /// <summary>
    /// 计算一个Vector3绕旋转中心旋转指定角度后所得到的向量。
    /// </summary>
    /// <param name="source">旋转前的源Vector3</param>
    /// <param name="axis">旋转轴</param>
    /// <param name="angle">旋转角度</param>
    /// <returns>旋转后得到的新Vector3</returns>
    public Vector3 V3RotateAround(Vector3 source, Vector3 axis, float angle)
    {
        Quaternion q = Quaternion.AngleAxis(angle, axis);// 旋转系数
        return q * source;// 返回目标点
    }

    void FixedUpdate()
    {
        GameObject go = GameObject.Find("Camera2D");
        if (gameObject.transform.position.y > 600 && go == null)
        {
            Vector3 point = GameManger.Instance.MiddlePoint();
            GameObject instance = (GameObject)Instantiate(Resources.Load(GameDefine.Camera2D), new Vector3(point.x,600,point.z), transform.rotation);
            instance.name = "Camera2D";
            SecurityCamera.ChangeCamera(instance.name);
            
            GameObject inn = (GameObject)Instantiate(Resources.Load(GameDefine.CellLine));
            inn.name = GameDefine.CellLineName;
        }

        GetDirection();
        // 检测是否离不可穿透表面过近
        RaycastHit hit;
        while (Physics.Raycast(tourCamera.position, direction, out hit, minDistance))
        {
            // 消去垂直于不可穿透表面的运动速度分量
            float angel = Vector3.Angle(direction, hit.normal);
            float magnitude = Vector3.Magnitude(direction) * Mathf.Cos(Mathf.Deg2Rad * (180 - angel));
            direction += hit.normal * magnitude;
        }
        tourCamera.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }
}

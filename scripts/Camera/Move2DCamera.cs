using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Move2DCamera : MonoBehaviour
{
    #region 定义参数
    private Camera tourCamera;
    private float speed = 500f;
    private Vector3 m_lastMousePosition;
    private Vector3 curr_lastMousePosition;
    private float PanSensitivity = 3000f;
    private bool canPan = false;
    GameObject go;
    #endregion
    // Use this for initialization
    void Start()
    {
        tourCamera = GameObject.Find("Camera2D").GetComponent<Camera>();
        go = new GameObject("Point");
        go.tag = "control";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameDefine.middlepoint = GameManger.Instance.MiddlePoint();
        #region 获取按键输入
        // 获取按键输入
        if (Input.mousePosition.y <= 10)
            transform.Translate(-Vector3.up * speed * Time.deltaTime);
        if (Input.mousePosition.y > Screen.height - 10) transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (Input.mousePosition.x <= 30) transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (Input.mousePosition.x > Screen.width - 30) transform.Translate(-Vector3.left * speed * Time.deltaTime);
        float wheel = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;
        tourCamera.orthographicSize -= 3000f * wheel;

        if (tourCamera.orthographicSize < 800 && (tourCamera.orthographicSize > 500))
        {
            GameDefine.GridScale = 1;
        }

        if (tourCamera.orthographicSize > 800 && (tourCamera.orthographicSize < 1300))
        {
            GameDefine.GridScale = 2;
        }

 if (tourCamera.orthographicSize > 1300 && (tourCamera.orthographicSize < 2500))
        {
            GameDefine.GridScale = 3;
        }

        if (tourCamera.orthographicSize > 2500)
        {
            GameDefine.GridScale = 4;
        }


        if (tourCamera.orthographicSize < 500)
        {
            Destroy(GameObject.Find(GameDefine.CellLineName));
            Destroy(gameObject);
            SecurityCamera.ChangeCamera("CameraFree");
            GameDefine.ganglai = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            canPan = true;
            m_lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            canPan = false;
        }

        if (canPan)
        {
            Pan();
        }
        #endregion
    }

    #region 漫游场景
    public void Pan()
    {
        Vector3 delta = m_lastMousePosition - Input.mousePosition;
        delta = delta / Mathf.Sqrt(tourCamera.pixelHeight * tourCamera.pixelHeight + tourCamera.pixelWidth * tourCamera.pixelWidth);

        delta *= PanSensitivity;
        delta = tourCamera.cameraToWorldMatrix.MultiplyVector(delta);
        tourCamera.transform.position += delta;
        go.transform.position += delta;
        m_lastMousePosition = Input.mousePosition;
    }
    #endregion
}


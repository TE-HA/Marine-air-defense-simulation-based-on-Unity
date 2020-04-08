using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region 获取键位：F K 1-9 P E N M 然后切换相机
        if (Input.GetKeyDown(KeyCode.F))
        {
            SecurityCamera.ChangeCamera("CameraFree");

            if (GameObject.Find("CameraFree").GetComponent<free>().enabled == false)
            {
                GameObject.Find("CameraFree").GetComponent<free>().enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SecurityCamera.ChangeCamera("Camera2D");
            if (GameObject.Find("CameraFree").GetComponent<free>().enabled == false)
            {
                GameObject.Find("CameraFree").GetComponent<free>().enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SecurityCamera.ChangeCamera("Camera_1");
            if (GameObject.Find("CameraFree").GetComponent<free>().enabled == true)
            {
                GameObject.Find("CameraFree").GetComponent<free>().enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SecurityCamera.ChangeCamera("Camera_2");
            if (GameObject.Find("CameraFree").GetComponent<free>().enabled == true)
            {
                GameObject.Find("CameraFree").GetComponent<free>().enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SecurityCamera.ChangeCamera("Camera_3");
            if (GameObject.Find("CameraFree").GetComponent<free>().enabled == true)
            {
                GameObject.Find("CameraFree").GetComponent<free>().enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SecurityCamera.ChangeCamera("Camera_4");
            if (GameObject.Find("CameraFree").GetComponent<free>().enabled == true)
            {
                GameObject.Find("CameraFree").GetComponent<free>().enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SecurityCamera.ChangeCamera("Camera_5");
            if (GameObject.Find("CameraFree").GetComponent<free>().enabled == true)
            {
                GameObject.Find("CameraFree").GetComponent<free>().enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SecurityCamera.ChangeCamera("Camera_6");
            if (GameObject.Find("CameraFree").GetComponent<free>().enabled == true)
            {
                GameObject.Find("CameraFree").GetComponent<free>().enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SecurityCamera.ChangeCamera("Camera_7");
            if (GameObject.Find("CameraFree").GetComponent<free>().enabled == true)
            {
                GameObject.Find("CameraFree").GetComponent<free>().enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SecurityCamera.ChangeCamera("Camera_main");
            if (GameObject.Find("CameraFree").GetComponent<free>().enabled == true)
            {
                GameObject.Find("CameraFree").GetComponent<free>().enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SecurityCamera.ChangeCamera("Camera_Plane");
            if (GameObject.Find("CameraFree").GetComponent<free>().enabled == true)
            {
                GameObject.Find("CameraFree").GetComponent<free>().enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SecurityCamera.ChangeCamera("Camera_Enemy");
            if (GameObject.Find("CameraFree").GetComponent<free>().enabled == true)
            {
                GameObject.Find("CameraFree").GetComponent<free>().enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            SecurityCamera.ChangeCamera("Camera_N");
            if (GameObject.Find("CameraFree").GetComponent<free>().enabled == true)
            {
                GameObject.Find("CameraFree").GetComponent<free>().enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SecurityCamera.ChangeCamera("Camera_M");
            if (GameObject.Find("CameraFree").GetComponent<free>().enabled == true)
            {
                GameObject.Find("CameraFree").GetComponent<free>().enabled = false;
            }
        }
        #endregion
    }
}

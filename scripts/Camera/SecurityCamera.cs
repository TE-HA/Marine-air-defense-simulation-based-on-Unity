using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    private static GameObject CurrentCamera;
    void Start()
    {

    }

    public static void ChangeCamera(string newCamera)
    {
        //差错控制
        try
        {
            #region 切换相机
            CurrentCamera = GameObject.Find(newCamera);
            if (newCamera == "Camera2D")
            {
                //CurrentCamera.GetComponent<Camera>().orthographic = true;
            }
            CurrentCamera.GetComponent<Camera>().depth = 999999;

            GameDefine.ChangeView(CurrentCamera.GetComponent<Camera>());
            #endregion
        }
        catch {

        }
    }
}

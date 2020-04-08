using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderFacetoCamera : MonoBehaviour
{

    #region 血条始终朝向MainCamera
    private Camera refCamera;
    //public bool reverseFace = false;
    private Transform mRoot;
    // Use this for initialization
    void Awake()
    {
        mRoot = transform;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        refCamera = GameDefine.CurrentCamera;
        try
        {
            if (!refCamera.name.Equals("Camera2D"))
            {
                //判断距离
                float distanceToCurrCamera = GameManger.Instance.DistanceBetweenTwoGameObject(gameObject.transform, refCamera.transform);
                //缩放Slider
                float x = 0.0015f * distanceToCurrCamera;
                Vector3 scale = new Vector3(x, x, x);
                gameObject.transform.localScale = scale;
            }
            else {
                gameObject.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
            }

            Vector3 targetPos = mRoot.position + refCamera.transform.rotation * Vector3.back;
            Vector3 targetOri = refCamera.transform.rotation * Vector3.up;
            mRoot.LookAt(targetPos, targetOri);
        }
        catch
        {
            refCamera = Camera.main;
            GameDefine.CurrentCamera = Camera.main;
        }
    }
    #endregion
}

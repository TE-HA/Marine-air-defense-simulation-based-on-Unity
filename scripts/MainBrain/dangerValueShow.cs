using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dangerValueShow : MonoBehaviour
{
    private Camera refCamera;
    private Transform mRoot;
    // Use this for initialization
    void Start()
    {
        mRoot = gameObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        refCamera = GameDefine.CurrentCamera;

        Vector3 targetPos = mRoot.position + refCamera.transform.rotation * Vector3.forward;
        Vector3 targetOri = refCamera.transform.rotation * Vector3.up;
        mRoot.LookAt(targetPos, targetOri);

        gameObject.GetComponent<Text>().text = gameObject.transform.parent.GetComponent<dangerValue>().DangerValue.ToString();
        //Vector3 scale = new Vector3(3f, 1, 1);
        //gameObject.transform.localScale = scale;
    }
}

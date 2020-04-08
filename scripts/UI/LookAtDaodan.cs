using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtDaodan : MonoBehaviour
{
    #region 跟随导弹相机，跟SmoothFollow脚本类似
    // Use this for initialization
    /// <summary>
    /// 跟随的物体
    /// </summary>
    private GameObject target;
    /// <summary>
    /// 跟随保持的高度
    /// </summary>
    private float height = 10;
    /// <summary>
    /// 跟随保持的距离
    /// </summary>
    /// 
	private float damping = 5.0f;
    private float distance = 10;
    Vector3 _pos;
    // Use this for initialization
    void Start()
    {
        target = gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LateUpdate();
    }
    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
        Vector3 wantedPosition = target.transform.TransformPoint(0, height, -distance);
        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);
        transform.LookAt(target.transform, target.transform.up);
    }
    #endregion
}

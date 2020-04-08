using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMove : MonoBehaviour
{
    private Vector3 target;
    private float RocSpeed = 10f;
    private float MoveSpeed = 10f;

    public float move_task_x;
    public float move_task_z;
    void Start()
    {
        ////////move
        Debug.DrawLine(Vector3.zero,target,Color.red);
        target.x = transform.position.x + move_task_x;
        target.z = transform.position.z + move_task_z;
        target.y = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 _target = target - transform.position;
        if (Mathf.Pow(_target.x, 2) + Mathf.Pow(_target.y, 2) + Mathf.Pow(_target.z, 2) < 10)
        {
            Destroy(gameObject.GetComponent<BoatMove>());
            return;
        }
        float a = Vector3.Angle(transform.forward, _target) / RocSpeed;
        if (a > -0.1f || a < 0.1f)
        {
            transform.forward = Vector3.Slerp(transform.forward, _target, Time.deltaTime / a).normalized;
        }
        else
        {
            transform.forward = Vector3.Slerp(transform.forward, _target, 1).normalized;
        }
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
    }
}

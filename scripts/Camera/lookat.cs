using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour {
    public Transform target;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }
        transform.LookAt(target);
        //transform.RotateAround(target.transform.position, Vector3.up, 200 * Time.deltaTime);
    }
}

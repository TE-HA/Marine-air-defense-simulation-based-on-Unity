using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatright : MonoBehaviour {

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
        
        //环绕战场
        transform.RotateAround(target.transform.position, Vector3.up, 5f * Time.deltaTime);
    }
}

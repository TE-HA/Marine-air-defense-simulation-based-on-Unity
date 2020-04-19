using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dangerValue : MonoBehaviour {
    public float DangerValue;
    private GameObject text;
    // Use this for initialization
    void Start()
    {
        if (gameObject.tag != "plane")
        {


            text = gameObject.transform.Find("Text").gameObject;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (gameObject.tag != "plane")
        {
            if (GameDefine.CurrentCamera.name == "Camera2D")
            {
                text.SetActive(true);
            }
            else
            {
                text.SetActive(false);
            }
        }
	}
}

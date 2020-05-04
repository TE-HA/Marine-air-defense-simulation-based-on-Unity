using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dangerValue : MonoBehaviour
{
    public float DangerValue;
    public bool isAdd=false;
    private GameObject text;
    private int jiange = 60;
    // Use this for initialization
    void Start()
    {
        text = gameObject.transform.Find("Text").gameObject;
    }

    void FixedUpdate()
    {      
        if (!isAdd) {
            if (jiange < 0)
            {
                if (GameManger.Instance.DistanceBetweenTwoVector3(Vector3.zero, gameObject.transform.position) < 5000)
                {
                    DangerValue += 20;
                }
                else {
                    DangerValue += 10;
                }
                jiange = 60;
            }
            else
            {
                jiange--;
            }
        }
        else { }


        //add to heap
        if (!isAdd && DangerValue > GameDefine.canFireValue)
        {
            if (taskHeap.Instance.Insert(new TaskNode(gameObject.name, DangerValue)))
            {
                isAdd = true;
            }
            else
            {
                isAdd = false;
            }
        }

        /*
         * */
     /*   if (DangerValue < GameDefine.canFireValue)
        {
            isAdd = false;
        }*/

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

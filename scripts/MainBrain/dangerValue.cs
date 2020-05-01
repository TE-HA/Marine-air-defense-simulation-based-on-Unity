using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dangerValue : MonoBehaviour
{
    public float DangerValue;
    private GameObject text;
    private int jiange = 60;
    // Use this for initialization
    void Start()
    {
        text = gameObject.transform.Find("Text").gameObject;
    }

    void FixedUpdate()
    {
        if (!GameData.Instance.isAdded[gameObject]) {
            if (jiange < 0)
            {
                if (GameManger.Instance.DistanceBetweenTwoVector3(Vector3.zero, gameObject.transform.position) < 5000)
                {
                    DangerValue += 100;
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
        if (!GameData.Instance.isAdded[gameObject] && DangerValue > GameDefine.canFireValue)
        {
            taskHeap.Instance.Insert(new TaskNode(gameObject.name, DangerValue));
            GameData.Instance.isAdded[gameObject] = true;
        }


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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyRay : MonoBehaviour
{
    private string type;
    void Start()
    {
        type = gameObject.GetComponent<LineRenderer>().GetComponent<RayShot>().RayType;
    }
    void FixedUpdate()
    {
        switch (type)
        {
            case "FireRay":
                if (GameDefine.MuteFireRay)
                {
                    gameObject.GetComponent<LineRenderer>().startWidth = 0;
                    gameObject.GetComponent<LineRenderer>().endWidth = 0;
                }
                else
                {
                    gameObject.GetComponent<LineRenderer>().startWidth = 4;
                    gameObject.GetComponent<LineRenderer>().endWidth = 4;
                }

                break;
            case "WarningRay":
                if (GameDefine.MuteWarningRay)
                {
                    gameObject.GetComponent<LineRenderer>().startWidth = 0;
                    gameObject.GetComponent<LineRenderer>().endWidth = 0;
                }
                else
                {
                    gameObject.GetComponent<LineRenderer>().startWidth = 4;
                    gameObject.GetComponent<LineRenderer>().endWidth = 4;
                }

                break;
            case "WatchRay":
                if (GameDefine.MuteWatchRay)
                {
                    gameObject.GetComponent<LineRenderer>().startWidth = 0;
                    gameObject.GetComponent<LineRenderer>().endWidth = 0;
                }
                else
                {
                    gameObject.GetComponent<LineRenderer>().startWidth = 4;
                    gameObject.GetComponent<LineRenderer>().endWidth = 4;
                }
                break;
        }
    }
}

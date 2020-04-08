using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class travelline : MonoBehaviour
{
    public List<Vector3> points;
    private LineRenderer line;
    private Camera _2dcamera;

    private void Start()
    {
        //_2dcamera = GameObject.Find("Camera2D").GetComponent<Camera>();
        points = new List<Vector3>();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.M)) {
            points.Clear();
        }

        if (Input.GetMouseButtonDown(0)&&Input.GetKey(KeyCode.M))
        {
            if (_2dcamera==null) { return; }
            Vector3 point = _2dcamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 500));
            point.y = 500;
            if (points.Count == 0)
            {
                GameObject instance = (GameObject)Instantiate(Resources.Load(GameDefine.TravelLine), point, transform.rotation);
                line = instance.GetComponent<LineRenderer>();
                points.Add(point);
            }
            else
            {
                points.Add(point);
            }
            //Debug.Log(point);
        }
        for (int i = 0; i < points.Count; i++)
        {
            line.SetPosition(i, points[i]);
        }
    }
}

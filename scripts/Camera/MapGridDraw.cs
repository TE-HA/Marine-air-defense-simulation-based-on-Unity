
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGridDraw : MonoBehaviour
{
    private LineRenderer lineRender;
    public Vector3 from;
    public Vector3 to;
    
    void Start()
    {
        lineRender = gameObject.GetComponent<LineRenderer>();
        List<Vector3> points = new List<Vector3>();
        points.Add(from);
        points.Add(to);

        lineRender.positionCount = points.Count;
        lineRender.SetPositions(points.ToArray());

        lineRender.startWidth = 3f;
        lineRender.endWidth = 3f;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShot : MonoBehaviour
{
    public GameObject from;
    public GameObject to;
    public string RayType;
    private float width = 0.5f;
    private LineRenderer lineRender;
    private int shexianchixushijian = 120;

    private Material material_fire;
    private Material material_warning;
    private Material material_watch;
    void Start()
    {
        material_fire = Resources.Load<Material>(GameDefine.ShotRay);
        material_warning = Resources.Load<Material>(GameDefine.WarningRay);
        material_watch = Resources.Load<Material>(GameDefine.WatchRay);
    }


     void FixedUpdate()
    {
        if (to == null || from == null)
        {
            Destroy(gameObject);
        }
      
        lineRender = gameObject.GetComponent<LineRenderer>();
        List<Vector3> points = new List<Vector3>();
        try
        {
            points.Add(from.transform.Find("FirePoint").transform.position);
            points.Add(to.transform.position);
        }
        catch { }
        lineRender.positionCount = points.Count;
        lineRender.SetPositions(points.ToArray());

        lineRender.startWidth = width * GameDefine.GridScale * 4;
        lineRender.endWidth = width * GameDefine.GridScale * 4;

        switch (RayType) {
            case "FireRay":
                lineRender.material = material_fire;
                if (shexianchixushijian < 0)
                {
                    Destroy(gameObject);
                }
                else {
                    shexianchixushijian--;
                }
                break;
            case "WarningRay":
                lineRender.material = material_warning;
                break;
            case "WatchRay":
                lineRender.material = material_watch;
                break;
        }
    }
}

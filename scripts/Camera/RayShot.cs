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

    private Material material_fire;
    private Material material_warning;
    private Material material_watch;
    void Start()
    {
        material_fire = Resources.Load<Material>(GameDefine.ShotRay);
        material_warning = Resources.Load<Material>(GameDefine.WarningRay);
        material_watch = Resources.Load<Material>(GameDefine.WatchRay);
    }

    /*
      #region 射线信息
        GameObject fireRay = (GameObject)Instantiate(Resources.Load(GameDefine.Ray));
        fireRay.GetComponent<RayShot>().from = _from;
        fireRay.GetComponent<RayShot>().to = _target;
        fireRay.GetComponent<RayShot>().material = Resources.Load<Material>(GameDefine.ShotRay);

        GameObject warningRay = (GameObject)Instantiate(Resources.Load(GameDefine.Ray));
        warningRay.GetComponent<RayShot>().from = GameObject.Find("km_3");
        warningRay.GetComponent<RayShot>().to = _target;
        warningRay.GetComponent<RayShot>().material = Resources.Load<Material>(GameDefine.WarningRay);

        GameObject watchRay = (GameObject)Instantiate(Resources.Load(GameDefine.Ray));
        watchRay.GetComponent<RayShot>().from = GameObject.Find("km_6");
        watchRay.GetComponent<RayShot>().to = _target;
        watchRay.GetComponent<RayShot>().material = Resources.Load<Material>(GameDefine.WatchRay);
        #endregion

         */

    // Update is called once per frame
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

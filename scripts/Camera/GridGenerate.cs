using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerate : MonoBehaviour
{
    private Transform root;
    // Use this for initialization
    void Start()
    {
        Vector3 point = GameManger.Instance.BoatMiddlePoint();

        
        root = GameObject.Find(GameDefine.CellLineName).transform;
        for (int i = 0; i < 100; i++)
        {
            Vector3 from = new Vector3(i * 300*GameDefine.GridScale, 100, point.x-10000);
            Vector3 to = new Vector3(i * 300*GameDefine.GridScale, 100, point.x+10000);
            GameObject instance = (GameObject)Instantiate(Resources.Load(GameDefine.Grid));
            instance.name = GameDefine.Grid_Name + GameDefine.GridScale;
            instance.transform.SetParent(root);
            instance.GetComponent<MapGridDraw>().from = from;
            instance.GetComponent<MapGridDraw>().to = to;
        }

        for (int i = 0; i < 100; i++)
        {

            Vector3 from = new Vector3(point.x-10000, 100, 300 * i*GameDefine.GridScale);
            Vector3 to = new Vector3(point.x+10000, 100, 300 * i*GameDefine.GridScale);
            GameObject instance = (GameObject)Instantiate(Resources.Load(GameDefine.Grid));
            instance.name = GameDefine.Grid_Name + GameDefine.GridScale;
            instance.transform.SetParent(root);
            instance.GetComponent<MapGridDraw>().from = from;
            instance.GetComponent<MapGridDraw>().to = to;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

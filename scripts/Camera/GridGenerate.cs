using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerate : MonoBehaviour
{
    private Transform root;
    // Use this for initialization
    void Start()
    {
        root = GameObject.Find(GameDefine.CellLineName).transform;
        for (int i = 0; i < 100; i++)
        {
            Vector3 from = new Vector3(i * 200*GameDefine.GridScale, 100, 0);
            Vector3 to = new Vector3(i * 200*GameDefine.GridScale, 100, 20000);
            GameObject instance = (GameObject)Instantiate(Resources.Load(GameDefine.Grid));
            instance.name = GameDefine.Grid_Name + GameDefine.GridScale;
            instance.transform.SetParent(root);
            instance.GetComponent<MapGridDraw>().from = from;
            instance.GetComponent<MapGridDraw>().to = to;
        }

        for (int i = 0; i < 100; i++)
        {
            Vector3 from = new Vector3(0, 100, 200 * i*GameDefine.GridScale);
            Vector3 to = new Vector3(20000, 100, 200 * i*GameDefine.GridScale);
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

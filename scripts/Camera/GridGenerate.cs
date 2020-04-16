using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerate : MonoBehaviour
{
    private GameObject root;
    private GameObject currRoot;
    private GameObject root1;
    private GameObject root2;
    private GameObject root3;
    private GameObject root4;
    // Use this for initialization
    void Start()
    {
        root = gameObject;
        root1 = (GameObject)Instantiate(Resources.Load(GameDefine.GridObj));
        root1.name = "root1";
        root1.transform.SetParent(gameObject.transform);

        root2 = (GameObject)Instantiate(Resources.Load(GameDefine.GridObj));
        root2.name = "root2";
        root2.transform.SetParent(gameObject.transform);

        root3 = (GameObject)Instantiate(Resources.Load(GameDefine.GridObj));
        root3.name = "root3";
        root3.transform.SetParent(gameObject.transform);

        root4 = (GameObject)Instantiate(Resources.Load(GameDefine.GridObj));
        root4.name = "root4";
        root4.transform.SetParent(gameObject.transform);


        for (int scale = 1; scale < 5; scale++)
        {
            GameDefine.GridScale = scale;
            currRoot = GameObject.Find("root" + scale.ToString());
            Vector3 point = GameManger.Instance.BoatMiddlePoint();
            for (int i = -50; i < 50; i++)
            {
                Vector3 from = new Vector3(i * 300 * GameDefine.GridScale, 100, point.z - 10000);
                Vector3 to = new Vector3(i * 300 * GameDefine.GridScale, 100, point.z + 10000);
                GameObject instance = (GameObject)Instantiate(Resources.Load(GameDefine.Grid));
                instance.transform.SetParent(currRoot.transform);
                instance.GetComponent<MapGridDraw>().from = from;
                instance.GetComponent<MapGridDraw>().to = to;
            }

            for (int i = -50; i < 50; i++)
            {
                Vector3 from = new Vector3(point.x - 10000, 100, 300 * i * GameDefine.GridScale);
                Vector3 to = new Vector3(point.x + 10000, 100, 300 * i * GameDefine.GridScale);
                GameObject instance = (GameObject)Instantiate(Resources.Load(GameDefine.Grid));
                instance.transform.SetParent(currRoot.transform);
                instance.GetComponent<MapGridDraw>().from = from;
                instance.GetComponent<MapGridDraw>().to = to;
            }

        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameDefine.GridScale == 1)
        {
            root4.SetActive(false);
            root2.SetActive(false);
            root3.SetActive(false);
            root1.SetActive(true);
        }
        else if (GameDefine.GridScale == 2)
        {

            root1.SetActive(false);
            root4.SetActive(false);
            root3.SetActive(false);
            root2.SetActive(true);
        }
        else if (GameDefine.GridScale == 3)
        {
            root4.SetActive(false);
            root1.SetActive(false);
            root2.SetActive(false);
            root3.SetActive(true);
        }
        else
        {
            root2.SetActive(false);
            root1.SetActive(false);
            root3.SetActive(false);
            root4.SetActive(true);
        }
    }
}

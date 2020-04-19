using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class init_assets : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
        for (int i = 0; i < GameDefine.fireAssets; i++)
        {
            gameObject.AddComponent<fire>();
        }
        for (int i = 0; i < GameDefine.warningAssets; i++)
        {
            gameObject.AddComponent<warning>();
        }
        for (int i = 0; i < GameDefine.watchingAssets*6; i++)
        {
            GameData.Instance.watchAssets[i] = new watching();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class init_assets : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {

      /*  GameData.dandaoCount.Add(20);
        GameData.dandaoCount.Add(30);
        GameData.dandaoCount.Add(40);
        GameData.dandaoCount.Add(15);
        GameData.dandaoCount.Add(34);
        GameData.dandaoCount.Add(29);
*/

        for (int i = 0; i < GameDefine.fireAssets; i++)
        {
            gameObject.AddComponent<fire>();
        }
        for (int i = 0; i < GameDefine.warningAssets; i++)
        {
            gameObject.AddComponent<warning>();
        }
        for (int i = 0; i < GameDefine.watchingAssets; i++)
        {
            gameObject.AddComponent<watching>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

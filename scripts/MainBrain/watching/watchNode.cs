using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watchNode : MonoBehaviour
{
    private watching km_1_1;
    private watching km_1_2;
    private watching km_1_3;

    private watching km_2_1;
    private watching km_2_2;
    private watching km_2_3;

    private watching km_3_1;
    private watching km_3_2;
    private watching km_3_3;

    private watching km_4_1;
    private watching km_4_2;
    private watching km_4_3;

    private watching km_5_1;
    private watching km_5_2;
    private watching km_5_3;

    private watching km_6_1;
    private watching km_6_2;
    private watching km_6_3;

    // Use this for initialization
    void Start()
    {
        /*km_1_1 = GameObject.Find("km_1").GetComponent<watching>();
        km_1_2 = GameObject.Find("km_1").GetComponent<watching>();
        km_1_3 = GameObject.Find("km_1").GetComponent<watching>();

        km_2_1 = GameObject.Find("km_2").GetComponent<watching>();
        km_2_2 = GameObject.Find("km_2").GetComponent<watching>();
        km_2_3 = GameObject.Find("km_2").GetComponent<watching>();

        km_3_1 = GameObject.Find("km_3").GetComponent<watching>();
        km_3_2 = GameObject.Find("km_3").GetComponent<watching>();
        km_3_3 = GameObject.Find("km_3").GetComponent<watching>();

        km_4_1 = GameObject.Find("km_4").GetComponent<watching>();
        km_4_2 = GameObject.Find("km_4").GetComponent<watching>();
        km_4_3 = GameObject.Find("km_4").GetComponent<watching>();

        km_5_1 = GameObject.Find("km_5").GetComponent<watching>();
        km_5_2 = GameObject.Find("km_5").GetComponent<watching>();
        km_5_3 = GameObject.Find("km_5").GetComponent<watching>();

        km_6_1 = GameObject.Find("km_6").GetComponent<watching>();
        km_6_2 = GameObject.Find("km_6").GetComponent<watching>();
        km_6_3 = GameObject.Find("km_6").GetComponent<watching>();*/
        for (int j = 0; j < GameDefine.watchingAssets; j++)
        {
            for (int i = 0; i < 6; i++)
            {
                GameData.Instance.watchAssets[j*6 + i] = GameObject.Find("km_" + (i + 1).ToString()).GetComponent<watching>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

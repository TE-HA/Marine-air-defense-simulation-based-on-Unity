using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetsRefresh : MonoBehaviour
{
    #region 定义参数
    string warning;
    string watch;
    string fire;
    string danyao;
    string xueliang;
    string danyaoname;
    string xueliangname;
    float xueliangpersent;
    float mainpersent;

    #endregion

    private void Init_assets()
    {
        for (int i = 0; i < 6; i++)
        {
            GameData.Instance.warning[i] = 3;
            GameData.Instance.watch[i] = 3;
            GameData.Instance.fire[i] = 1;
        }
    }
    // Use this for initialization
    void Start()
    {
        Init_assets();
        #region 定义
        #endregion
    }

    void fresh()
    {
        mainpersent = (float)PlayerPrefs.GetInt("km_main_info_slider") / 30000;
        GameObject.Find("Text7_6").GetComponent<Text>().text = mainpersent.ToString("0.00");

        for (int i = 1; i <= 6; i++)
        {
            warning = "Text" + i + "_4";
            GameObject.Find(warning).GetComponent<Text>().text = GameData.Instance.warning[i - 1].ToString();

            watch = "Text" + i + "_5";
            GameObject.Find(watch).GetComponent<Text>().text = GameData.Instance.watch[i - 1].ToString();

            fire = "Text" + i + "_3";
            GameObject.Find(fire).GetComponent<Text>().text = GameData.Instance.fire[i - 1].ToString();

            danyao = "Text" + i + "_2";
            danyaoname = "km_" + i + "_fireAssets";
            GameObject.Find(danyao).GetComponent<Text>().text = PlayerPrefs.GetInt(danyaoname).ToString();

            xueliang = "Text" + i + "_6";
            xueliangname = "km_" + i + "_info_slider";
            xueliangpersent = (float)PlayerPrefs.GetInt(xueliangname) / 20000;
            GameObject.Find(xueliang).GetComponent<Text>().text = xueliangpersent.ToString("0.00");
        }

    }

    int jiange = 60;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (jiange < 0)
        {
            fresh();
            jiange = 60;
        }
        else {
            jiange--;
        }
    }
}

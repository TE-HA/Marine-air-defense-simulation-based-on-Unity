using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetsRefresh : MonoBehaviour
{
    #region 定义
    /* private Text text1_2;
     private Text text2_2;
     private Text text3_2;
     private Text text4_2;
     private Text text5_2;
     private Text text6_2;
     private Text text7_2;
     private Text text1_3;
     private Text text2_3;
     private Text text3_3;
     private Text text4_3;
     private Text text5_3;
     private Text text6_3;
     private Text text7_3;
     private Text text1_4;
     private Text text2_4;
     private Text text3_4;
     private Text text4_4;
     private Text text5_4;
     private Text text6_4;
     private Text text7_4;
     private Text text1_5;
     private Text text2_5;
     private Text text3_5;
     private Text text4_5;
     private Text text5_5;
     private Text text6_5;
     private Text text7_5;*/
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
        /*text1_2 = GameObject.Find("Text1_2").GetComponent<Text>();
        text2_2 = GameObject.Find("Text2_2").GetComponent<Text>();
        text3_2 = GameObject.Find("Text3_2").GetComponent<Text>();
        text4_2 = GameObject.Find("Text4_2").GetComponent<Text>();
        text5_2 = GameObject.Find("Text5_2").GetComponent<Text>();
        text6_2 = GameObject.Find("Text6_2").GetComponent<Text>();
        text7_2 = GameObject.Find("Text7_2").GetComponent<Text>();

        text1_3 = GameObject.Find("Text1_3").GetComponent<Text>();
        text2_3 = GameObject.Find("Text2_3").GetComponent<Text>();
        text3_3 = GameObject.Find("Text3_3").GetComponent<Text>();
        text4_3 = GameObject.Find("Text4_3").GetComponent<Text>();
        text5_3 = GameObject.Find("Text5_3").GetComponent<Text>();
        text6_3 = GameObject.Find("Text6_3").GetComponent<Text>();
        text7_3 = GameObject.Find("Text7_3").GetComponent<Text>();

        text1_4 = GameObject.Find("Text1_4").GetComponent<Text>();
        text2_4 = GameObject.Find("Text2_4").GetComponent<Text>();
        text3_4 = GameObject.Find("Text3_4").GetComponent<Text>();
        text4_4 = GameObject.Find("Text4_4").GetComponent<Text>();
        text5_4 = GameObject.Find("Text5_4").GetComponent<Text>();
        text6_4 = GameObject.Find("Text6_4").GetComponent<Text>();
        text7_4 = GameObject.Find("Text7_4").GetComponent<Text>();

        text1_5 = GameObject.Find("Text1_5").GetComponent<Text>();
        text2_5 = GameObject.Find("Text2_5").GetComponent<Text>();
        text3_5 = GameObject.Find("Text3_5").GetComponent<Text>();
        text4_5 = GameObject.Find("Text4_5").GetComponent<Text>();
        text5_5 = GameObject.Find("Text5_5").GetComponent<Text>();
        text6_5 = GameObject.Find("Text6_5").GetComponent<Text>();
        text7_5 = GameObject.Find("Text7_5").GetComponent<Text>();*/
        #endregion
    }
    // Update is called once per frame
    string warning;
    string watch;
    string fire;
    string danyao;
    string xueliang;
    string danyaoname;
    string xueliangname;
    float xueliangpersent;
    float mainpersent;
    void FixedUpdate()
    {
        mainpersent = (float)PlayerPrefs.GetInt("km_main_info_slider") / 30000;
        GameObject.Find("Text7_6").GetComponent<Text>().text =  mainpersent.ToString("0.00");
        for (int i = 1; i <= 6; i++)
        {
            warning = "Text" + i + "_4";
            GameObject.Find(warning).GetComponent<Text>().text = GameData.Instance.warning[i - 1].ToString();
            watch = "Text" + i + "_5";
            GameObject.Find(watch).GetComponent<Text>().text = GameData.Instance.watch[i - 1].ToString();
            fire = "Text" + i + "_3";
            GameObject.Find(fire).GetComponent<Text>().text = GameData.Instance.fire[i - 1].ToString();
            danyao = "Text" + i + "_2";
            danyaoname = "km_" + i+"_fireAssets";
            GameObject.Find(danyao).GetComponent<Text>().text = PlayerPrefs.GetInt(danyaoname).ToString();
            xueliang = "Text" + i + "_6";
            xueliangname = "km_" + i+"_info_slider";
            xueliangpersent = (float)PlayerPrefs.GetInt(xueliangname) / 20000;
            GameObject.Find(xueliang).GetComponent<Text>().text =  xueliangpersent.ToString("0.00");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    #region 菜单参数定义
    public IPanel menu;
    public Text time;
    public GameObject plane;
    public GameObject PausePanel;
    public int ClearTime = 0;
    public int jiange = 0;
    public List<string> tagList;
    #endregion


    // 数据定义
    void Awake()
    {
        #region 数据库清空
        string sql_delete_all_task = "DELETE FROM graduate.all_task";
        string sql_delete_all_weapon_task = "DELETE FROM graduate.weapon_task";
        string sql_delete_all_move_task = "DELETE FROM graduate.move_task";
        MySqlT.Instance.DealSqlToSet(sql_delete_all_task);
        MySqlT.Instance.DealSqlToSet(sql_delete_all_move_task);
        MySqlT.Instance.DealSqlToSet(sql_delete_all_weapon_task);
        #endregion

        #region 游戏存档数据
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("TaskID", 1);
        PlayerPrefs.SetString("航空母舰", "km_main");
        PlayerPrefs.SetString("护卫舰1", "km_5");
        PlayerPrefs.SetString("护卫舰2", "km_6");
        PlayerPrefs.SetString("驱逐舰1", "km_1");
        PlayerPrefs.SetString("驱逐舰2", "km_3");
        PlayerPrefs.SetString("巡洋舰1", "km_2");
        PlayerPrefs.SetString("巡洋舰2", "km_4");

        PlayerPrefs.SetString("km_main", "航空母舰");
        PlayerPrefs.SetString("km_5", "护卫舰1");
        PlayerPrefs.SetString("km_6", "护卫舰2");
        PlayerPrefs.SetString("km_1", "驱逐舰1");
        PlayerPrefs.SetString("km_3", "驱逐舰2");
        PlayerPrefs.SetString("km_2", "巡洋舰1");
        PlayerPrefs.SetString("km_4", "巡洋舰2");
        PlayerPrefs.SetString("plane_warning", "预警机");
    
        PlayerPrefs.SetString("km_main_info_name", "航空母舰");
        PlayerPrefs.SetString("km_5_info_name", "护卫舰1");
        PlayerPrefs.SetString("km_6_info_name", "护卫舰2");
        PlayerPrefs.SetString("km_1_info_name", "驱逐舰1");
        PlayerPrefs.SetString("km_3_info_name", "驱逐舰2");
        PlayerPrefs.SetString("km_2_info_name", "巡洋舰1");
        PlayerPrefs.SetString("km_4_info_name", "巡洋舰2");
        PlayerPrefs.SetString("plane_warning_info_name", "预警机");

        PlayerPrefs.SetInt("CurrTime", 0);
        PlayerPrefs.SetInt("EnemyPlaneDaodanCount", 0);
        PlayerPrefs.SetInt("EnemyPlaneCount", 0);
        PlayerPrefs.SetInt("EnemyTaskCount", 0);
        PlayerPrefs.SetInt("TaskID", 1);
        PlayerPrefs.SetInt("FireTaskID", 1);


        PlayerPrefs.SetInt("km_main_info_slider", 3000);
        PlayerPrefs.SetInt("km_1_info_slider", 20000);
        PlayerPrefs.SetInt("km_2_info_slider", 20000);
        PlayerPrefs.SetInt("km_3_info_slider", 20000);
        PlayerPrefs.SetInt("km_4_info_slider", 20000);
        PlayerPrefs.SetInt("km_5_info_slider", 20000);
        PlayerPrefs.SetInt("km_6_info_slider", 20000);
        PlayerPrefs.SetInt("plane_warning_info_slider", 20000);
        #endregion

        #region judge by tag 的链表
        tagList = new List<string>();
        tagList.Add("MainCamera");
        tagList.Add("Ocean");
        tagList.Add("daodan");
        tagList.Add("zhanjian");
        tagList.Add("plane");
        tagList.Add("control");
        tagList.Add("sun");
        tagList.Add("mycamera");
        tagList.Add("other");
        #endregion


        #region 初始化参数输入界面,作战想定参数输入
        GameObject panel = (GameObject)Instantiate(Resources.Load(GameDefine.InputMenu));
        panel.name = "InputMenu";
        panel.SetActive(true);

        #endregion



        #region 初始化血条位置
        InitBloodSlider("km_main");
        InitBloodSlider("km_1");
        InitBloodSlider("km_2");
        InitBloodSlider("km_3");
        InitBloodSlider("km_4");
        InitBloodSlider("km_5");
        InitBloodSlider("km_6");
        InitBloodSlider("Plane_Warning");
        /*GameObject km_main = GameObject.Find("km_main");
        GameObject km_1 = GameObject.Find("km_1");
        GameObject km_2 = GameObject.Find("km_2");
        GameObject km_3 = GameObject.Find("km_3");
        GameObject km_4 = GameObject.Find("km_4");
        GameObject km_5 = GameObject.Find("km_5");
        GameObject km_6 = GameObject.Find("km_6");
        GameObject Plane_Warning = GameObject.Find("Plane_Warning");

        Transform BloodPoint_main = km_main.transform.Find("BloodPoint");
        GameObject objmain = (GameObject)Instantiate(Resources.Load(GameDefine.BloodSlider), BloodPoint_main.position, BloodPoint_main.rotation);
        objmain.transform.parent = km_main.transform;
        objmain.name = GameDefine.BloodSlider_main;

        Transform BloodPoint_1 = km_1.transform.Find("BloodPoint");
        GameObject obj1 = (GameObject)Instantiate(Resources.Load(GameDefine.BloodSlider), BloodPoint_1.position, BloodPoint_1.rotation);
        obj1.transform.parent = km_1.transform;
        obj1.name = GameDefine.BloodSlider_1;

        Transform BloodPoint_2 = km_2.transform.Find("BloodPoint");
        GameObject obj2 = (GameObject)Instantiate(Resources.Load(GameDefine.BloodSlider), BloodPoint_2.position, BloodPoint_2.rotation);
        obj2.transform.parent = km_2.transform;
        obj2.name = GameDefine.BloodSlider_2;

        Transform BloodPoint_3 = km_3.transform.Find("BloodPoint");
        GameObject obj3 = (GameObject)Instantiate(Resources.Load(GameDefine.BloodSlider), BloodPoint_3.position, BloodPoint_3.rotation);
        obj3.transform.parent = km_3.transform;
        obj3.name = GameDefine.BloodSlider_3;

        Transform BloodPoint_4 = km_4.transform.Find("BloodPoint");
        GameObject obj4 = (GameObject)Instantiate(Resources.Load(GameDefine.BloodSlider), BloodPoint_4.position, BloodPoint_4.rotation);
        obj4.transform.parent = km_4.transform;
        obj4.name = GameDefine.BloodSlider_4;

        Transform BloodPoint_5 = km_5.transform.Find("BloodPoint");
        GameObject obj5 = (GameObject)Instantiate(Resources.Load(GameDefine.BloodSlider), BloodPoint_5.position, BloodPoint_5.rotation);
        obj5.transform.parent = km_5.transform;
        obj5.name = GameDefine.BloodSlider_5;

        Transform BloodPoint_6 = km_6.transform.Find("BloodPoint");
        GameObject obj6 = (GameObject)Instantiate(Resources.Load(GameDefine.BloodSlider), BloodPoint_6.position, BloodPoint_6.rotation);
        obj6.transform.parent = km_6.transform;
        obj6.name = GameDefine.BloodSlider_6;

        Transform BloodPoint_Plane_Warning = Plane_Warning.transform.Find("BloodPoint");
        GameObject obj_Plane_Warning = (GameObject)Instantiate(Resources.Load(GameDefine.BloodSlider), BloodPoint_Plane_Warning.position, BloodPoint_Plane_Warning.rotation);
        obj_Plane_Warning.transform.parent = Plane_Warning.transform;
        obj_Plane_Warning.name = GameDefine.BloodSlider_Plane_Warning;*/

        #endregion



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region 每隔一段时间自动清理特效，弃用
        /* if (jiange < 0)
         {
             ClearEffect();
             jiange = 1200;
         }
         else
         {
             jiange--;
         }*/
        #endregion


        #region 更新游戏时间
        time = GameObject.Find("time").GetComponent<Text>();
        if (jiange <= 0)
        {
            time.text = PlayerPrefs.GetInt("CurrTime").ToString() + " 秒";
            PlayerPrefs.SetInt("CurrTime", PlayerPrefs.GetInt("CurrTime") + 1);
            jiange = 60;
        }
        else
        {
            jiange--;
        }
        #endregion
    }


    #region 清除特效
    public void ClearEffect()
    {
        ClearTime++;
        Debug.Log("清除特效Round " + ClearTime);
        foreach (GameObject objj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            Debug.Log(objj.name);
            if (!inSence(objj.tag) && !NameConLod(objj.name))
            {
                Debug.Log("清除：" + objj.name);
                Destroy(objj);
            }
        }
    }
    #endregion


    #region 海洋中的GameObject不清除 很烦，应该找个更好的海洋
    bool NameConLod(string name)
    {
        char[] ss = name.ToCharArray();
        if (ss[0] != 'L' || ss[1] != 'o' || ss[2] != 'd' || ss[3] != '_')
        {
            return false;
        }
        return true;
    }
    #endregion


    #region 清除特效，根据GameObject的tag清除
    bool inSence(string tag)
    {
        if (tagList.Contains(tag))
        {
            return true;
        }
        return false;
    }
    #endregion


    #region 初始化血条信息
    public void InitBloodSlider(string objname)
    {
        GameObject obj = GameObject.Find(objname);
        if (obj == null)
        {
            return;
        }
        Transform BloodPoint = obj.transform.Find("BloodPoint");
        GameObject objslider = (GameObject)Instantiate(Resources.Load(GameDefine.BloodSlider), BloodPoint.position, BloodPoint.rotation);
        objslider.transform.parent = obj.transform;
        objslider.name = objname.ToLower() + "_info";
    }
    #endregion

    public void OnGUI()
    {
        #region GUI中的暂停Pause键
        if (GUI.Button(new Rect(10, 50, 80, 20), GameDefine.GUIPause))
        {
            if (Time.timeScale == 0)
            {
                return;
            }

            PausePanel = (GameObject)Instantiate(Resources.Load(GameDefine.InputMenu));
            PausePanel.name = GameDefine.PausePanelName;
        }
        #endregion

        #region GUI中的继续Resume键
        if (GUI.Button(new Rect(10, 80, 80, 20), GameDefine.GUIResume))
        {
            Time.timeScale = 1;
            GameManger.Instance.UnMuteAll();

            if (PausePanel == null)
            {
                return;
            }
            else
            {
                PausePanel.SetActive(false);
            }
        }
        #endregion

        #region GUI禁用特效MuteEffect键
        if (GUI.Button(new Rect(10, 110, 80, 20), GameDefine.GUIMuteEffect))
        {
            if (GameDefine.MuteEffect == true)
            {
                GameDefine.MuteEffect = false;
            }
            else
            {
                GameDefine.MuteEffect = true;
            }
        }
        #endregion

        #region GUI中清除特效ClearEffect键
        if (GUI.Button(new Rect(10, 140, 80, 20), GameDefine.GUIClearEffect))
        {
            ClearEffect();
        }
        #endregion

        #region GUI中更新数据库键位（测试用）
        if (GUI.Button(new Rect(10, 170, 80, 20), GameDefine.GUIUpdate))
        {
        }
        #endregion
    }
}


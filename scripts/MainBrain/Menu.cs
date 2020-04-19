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
    public Text locayion_xx;
    public GameObject plane;
    public GameObject PausePanel;
    public GameObject ShowLogPanel;
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
        string sql_delete_all_addobj_task = "DELETE FROM graduate.addobj_task";
        MySqlT.Instance.DealSqlToSet(sql_delete_all_task);
        MySqlT.Instance.DealSqlToSet(sql_delete_all_move_task);
        MySqlT.Instance.DealSqlToSet(sql_delete_all_weapon_task);
        MySqlT.Instance.DealSqlToSet(sql_delete_all_addobj_task);
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

        /*
                PlayerPrefs.SetString("航空母舰", "hangmu");
                PlayerPrefs.SetString("驱逐舰", "quzhu");
                PlayerPrefs.SetString("巡洋舰", "xunyang");
                PlayerPrefs.SetString("护卫舰", "huwei");*/

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


        PlayerPrefs.SetInt("km_main_info_slider", 30000);
        PlayerPrefs.SetInt("km_1_info_slider", 20000);
        PlayerPrefs.SetInt("km_2_info_slider", 20000);
        PlayerPrefs.SetInt("km_3_info_slider", 20000);
        PlayerPrefs.SetInt("km_4_info_slider", 20000);
        PlayerPrefs.SetInt("km_5_info_slider", 20000);
        PlayerPrefs.SetInt("km_6_info_slider", 20000);
        PlayerPrefs.SetInt("plane_warning_info_slider", 20000);


        PlayerPrefs.SetInt("km_1_fireAssets", 20);
        PlayerPrefs.SetInt("km_2_fireAssets", 40);
        PlayerPrefs.SetInt("km_3_fireAssets", 25);
        PlayerPrefs.SetInt("km_4_fireAssets", 20);
        PlayerPrefs.SetInt("km_5_fireAssets", 35);
        PlayerPrefs.SetInt("km_6_fireAssets", 45);



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

        #region 初始化

        #region 初始化游戏日志
        ShowLogPanel = GameObject.Find(GameDefine.ShowLogPanelName);
        UnShowLog();
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

        

        #endregion
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

        #region 更新系统时间
        UpdateTime();
        #endregion

        #region 更新中心坐标
        UpdateLocation();
        #endregion

        #region 更新系统左下角坐标


        #endregion
    }

    #region 更新游戏时间
    public void UpdateTime()
    {

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
    }
    #endregion

    #region 更新中心界面坐标
    public void UpdateLocation()
    {

        locayion_xx = GameObject.Find("location").GetComponent<Text>();
        Vector3 middle = GameManger.Instance.MiddlePoint();
        locayion_xx.text = "(" + (int)middle.x + " , " + (int)middle.z + ")";
    }
    #endregion

    #region 游戏日志打开与关闭方法
    public void ShowLog()
    {
        ShowLogPanel.GetComponent<CanvasGroup>().alpha = 1;
        ShowLogPanel.GetComponent<CanvasGroup>().interactable = true;
        ShowLogPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameDefine.ShowStatus = true;
    }
    public void UnShowLog()
    {
        ShowLogPanel.GetComponent<CanvasGroup>().alpha = 0;
        ShowLogPanel.GetComponent<CanvasGroup>().interactable = false;
        ShowLogPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameDefine.ShowStatus = false;
    }
    #endregion

    #region 清除特效
    public void ClearEffect()
    {
        ClearTime++;
        Debug.Log("清除特效Round " + ClearTime);
        foreach (GameObject objj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            Debug.Log(objj.name);
            if (!inSence(objj.tag))
            {
                Debug.Log("清除：" + objj.name);
                Destroy(objj);
            }
        }
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
        if (GUI.Button(new Rect(1400, 80, 80, 20), GameDefine.GUIPause))
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
        if (GUI.Button(new Rect(1400, 110, 80, 20), GameDefine.GUIResume))
        {
            Time.timeScale = 1;
            GameManger.Instance.UnMuteAll();

            if (PausePanel == null)
            {
                return;
            }
            else
            {
                Destroy(PausePanel);
            }
        }
        #endregion

        #region GUI禁用特效MuteEffect键
        if (GUI.Button(new Rect(1400, 140, 80, 20), GameDefine.GUIMuteEffect))
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
        if (GUI.Button(new Rect(1400, 170, 80, 20), GameDefine.GUIClearEffect))
        {
            ClearEffect();
        }
        #endregion

        #region GUI中显示游戏日志
        if (GUI.Button(new Rect(1400, 200, 80, 20), GameDefine.GUIShowLog))
        {
            if (GameDefine.ShowStatus)
            {
                UnShowLog();
            }
            else
            {
                ShowLog();
            }
        }
        #endregion

        string ziyuan = "";
        string ziyuan2 = "";
        #region GUI中显示资源
        if (GUI.Button(new Rect(1400, 230, 80, 20), "FireAssets"))
        {
            for (int i = 1; i <= 6; i++)
            {
                ziyuan += PlayerPrefs.GetInt("km_" + i + "_fireAssets").ToString() + "_";
            }
            Debug.Log(ziyuan);
            Debug.Log(PlayerPrefs.GetInt("km_main_info_slider"));


          
        }
        #endregion
    }
}


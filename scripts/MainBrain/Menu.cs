using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    public GameObject VictoryPanel;
    public GameObject ShowLogPanel;
    public GameObject ShowAssets;
    public GameObject Heap;
    public GameObject GameAnalyse;
    public int ClearTime = 0;
    public int jiange = 0;
    public List<string> tagList;
    public List<string> fire_target = new List<string>();
    public List<int> fire_count = new List<int>();
    //Dictionary<string, int> fireCount;
    #endregion

    // 数据定义
    void Awake()
    {
        #region 数据库清空
        string sql_delete_all_task = "DELETE FROM graduate.all_task";
        string sql_delete_all_fire_task = "DELETE FROM graduate.fire_task";
        string sql_delete_all_attack_task = "DELETE FROM graduate.attack_task";
        string sql_delete_all_move_task = "DELETE FROM graduate.move_task";
        string sql_delete_all_addobj_task = "DELETE FROM graduate.addobj_task";
        MySqlT.Instance.DealSqlToSet(sql_delete_all_task);
        MySqlT.Instance.DealSqlToSet(sql_delete_all_move_task);
        MySqlT.Instance.DealSqlToSet(sql_delete_all_fire_task);
        MySqlT.Instance.DealSqlToSet(sql_delete_all_attack_task);
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
        PlayerPrefs.SetInt("WasteTime", 0);
        PlayerPrefs.SetInt("EnemyPlaneDaodanCount", 0);
        PlayerPrefs.SetInt("EnemyPlaneCount", 0);
        PlayerPrefs.SetInt("EnemyTaskCount", 0);
        PlayerPrefs.SetInt("LanjieCount", 0);
        PlayerPrefs.SetInt("AttackP", 0);
        PlayerPrefs.SetInt("TaskID", 1);
        PlayerPrefs.SetInt("FireTaskID", 1);


        PlayerPrefs.SetInt("km_main_info_slider", 30000);
        PlayerPrefs.SetInt("km_1_info_slider", 20000);
        PlayerPrefs.SetInt("km_2_info_slider", 20000);
        PlayerPrefs.SetInt("km_3_info_slider", 20000);
        PlayerPrefs.SetInt("km_4_info_slider", 20000);
        PlayerPrefs.SetInt("km_5_info_slider", 20000);
        PlayerPrefs.SetInt("km_6_info_slider", 20000);


        PlayerPrefs.SetInt("km_1_fireAssets", 80);
        PlayerPrefs.SetInt("km_2_fireAssets", 80);
        PlayerPrefs.SetInt("km_3_fireAssets", 90);
        PlayerPrefs.SetInt("km_4_fireAssets", 70);
        PlayerPrefs.SetInt("km_5_fireAssets", 85);
        PlayerPrefs.SetInt("km_6_fireAssets", 75);



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
        ShowAssets = GameObject.Find(GameDefine.ShowAssets);
        UnShowAssetsPanel();
        /*GameAnalyse = GameObject.Find(GameDefine.ShowGameAnalyse);
        UnShowGameAnalyse();*/
        Heap = GameObject.Find(GameDefine.ShowHeap);
        UnShowHeap();
        #endregion

        #region 初始化游戏分析结果
        //fireCount = new Dictionary<string, int>();
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
            //Debug.Log(GameData.Instance.isAdded.Count+"长度");
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


    #region 游戏分析界面打开与关闭方法
    public void ShowGameAnalyse()
    {
        GameAnalyse.GetComponent<CanvasGroup>().alpha = 1;
        GameAnalyse.GetComponent<CanvasGroup>().interactable = true;
        GameAnalyse.GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameDefine.canShowGameAnalyse = true;
    }
    public void UnShowGameAnalyse()
    {
        GameAnalyse.GetComponent<CanvasGroup>().alpha = 0;
        GameAnalyse.GetComponent<CanvasGroup>().interactable = false;
        GameAnalyse.GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameDefine.canShowGameAnalyse = false;
    }
    #endregion

    #region 字典排序
    private void DictonarySort()
    {
        //fireCount = fireCount.OrderBy(p => p.Value).ToDictionary(p => p.Key, p => p.Value);
    }
    #endregion

    #region 任务堆显示
    public void ShowHeap()
    {
        Heap.GetComponent<CanvasGroup>().alpha = 1;
        Heap.GetComponent<CanvasGroup>().interactable = true;
        Heap.GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameDefine.canShowHeap = true;
    }
    public void UnShowHeap()
    {
        Heap.GetComponent<CanvasGroup>().alpha = 0;
        Heap.GetComponent<CanvasGroup>().interactable = false;
        Heap.GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameDefine.canShowHeap = false;
    }
    #endregion

    #region 游戏资源打开与关闭方法
    public void ShowAssetsPanel()
    {
        ShowAssets.GetComponent<CanvasGroup>().alpha = 1;
        ShowAssets.GetComponent<CanvasGroup>().interactable = true;
        ShowAssets.GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameDefine.ShowAssetsLog = true;
    }
    public void UnShowAssetsPanel()
    {
        ShowAssets.GetComponent<CanvasGroup>().alpha = 0;
        ShowAssets.GetComponent<CanvasGroup>().interactable = false;
        ShowAssets.GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameDefine.ShowAssetsLog = false;
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
        #region GUI中清除特效暂停键
        if (GUI.Button(new Rect(220, 660, 80, 20), GameDefine.GUIPauseShow))
        {
            if (Time.timeScale == 0.02f)
            {
                Time.timeScale = 1;
                GameManger.Instance.UnMuteAll();
            }
            else
            {
                Time.timeScale = 0.02f;
                GameManger.Instance.MuteAll();
            }
        }
        #endregion



        #region GUI中的暂停Pause键
        if (GUI.Button(new Rect(310, 660, 80, 20), GameDefine.GUIPause))
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
        if (GUI.Button(new Rect(400, 660, 80, 20), GameDefine.GUIResume))
        {
            /* Time.timeScale = 1;
             GameManger.Instance.UnMuteAll();

             if (PausePanel == null)
             {
                 return;
             }
             else
             {
                 Destroy(PausePanel);
             }*/
            Time.timeScale = 3;
        }
        #endregion

        #region GUI禁用特效MuteEffect键
        if (GUI.Button(new Rect(490, 660, 80, 20), GameDefine.GUIMuteEffect))
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


        #region GUI中显示游戏日志
        if (GUI.Button(new Rect(580, 660, 80, 20), GameDefine.GUIShowLog))
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

        #region GUI中显示资源
        if (GUI.Button(new Rect(670, 660, 80, 20), GameDefine.GUIFireAssets))
        {
            if (GameDefine.ShowAssetsLog)
            {
                UnShowAssetsPanel();
            }
            else
            {
                ShowAssetsPanel();
            }
        }
        #endregion

        #region GUI中显示任务堆
        if (GUI.Button(new Rect(760, 660, 80, 20), GameDefine.GUIHeapStatus))
        {
            if (GameDefine.canShowHeap)
            {
                UnShowHeap();
            }
            else
            {
                ShowHeap();
            }
        }
        #endregion

        #region GUI中战后分析
        if (GUI.Button(new Rect(850, 660, 80, 20), GameDefine.GUIAnalyse))
        {
            if (!GameDefine.canShowGameAnalyse)
            {
                if (PlayerPrefs.GetInt("EnemyCount") == 0)
                {
                    return;
                }

                VictoryPanel = (GameObject)Instantiate(Resources.Load(GameDefine.AnalyseMenu));
                VictoryPanel.name = GameDefine.AnalysePanelName;
                GameDefine.canShowGameAnalyse = true;

                DataSet ds = MySqlT.Instance.DealSqlToSet(MySqlT._count_every_daodan);
                DataTable dt = ds.Tables[0];
                //fireCount.Clear();
                fire_target.Clear();
                fire_count.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //fireCount.Add(dt.Rows[i][0].ToString(), int.Parse(dt.Rows[i][1].ToString()));
                    fire_target.Add(dt.Rows[i][0].ToString());
                    fire_count.Add(int.Parse(dt.Rows[i][1].ToString()));
                }

                //DictonarySort();

                int goal_value = 0;//未击中
                int not_goal_value = 0;//击中
                int not_goal_count = 0;
                int[] behit = new int[7];
                int behit_boat_count = 0;
                int behit_value = 0;

                behit[0] = 30000 - PlayerPrefs.GetInt("km_main_info_slider");
                for (int i = 1; i < 7; i++)
                {
                    behit[i] = 20000 - PlayerPrefs.GetInt("km_" + i + "_info_slider");
                }

                for (int i = 0; i < 7; i++)
                {
                    if (behit[i] != 0)
                    {
                        behit_boat_count++;
                    }
                    behit_value += behit[i];
                }

                /* foreach (KeyValuePair<string, int> kvp in fireCount)
                 {
                     goal_value += kvp.Value;
                     //Debug.Log(string.Format("{0} {1}", kvp.Key, kvp.Value));
                 }*/

                for (int i = 0; i < fire_count.Count; i++)
                {
                    goal_value += fire_count[i];
                }
                /*foreach (KeyValuePair<string, string> kvp in GameData.Instance.behitInfo)
                {
                    Debug.Log("*****");
                    not_goal_count++;
                    not_goal_value += fireCount[kvp.Key];
                    //Debug.Log(string.Format("{0} {1} {2}", kvp.Key, kvp.Value, fireCount[kvp.Key]));
                    Debug.Log("kvp.key"+kvp.Key);
                    Debug.Log("kvp.value"+kvp.Value);
                    Debug.Log("first[kvp.key]"+fireCount[kvp.Key]);
                }

                //Debug.Log("count:"+GameData.Instance.behit_key.Count);

                for (int i = 0; i < fire_target.Count; i++)
                {
                    Debug.Log("各目标:" + fire_target[i] + "击中次数：" + fire_count[i]);
                }*/

                for (int i = 0; i < GameData.Instance.behit_key.Count; i++)
                {
                    try
                    {
                        not_goal_count++;
                        not_goal_value += fire_count[fire_target.IndexOf(GameData.Instance.behit_key[i])];
                    }
                    catch
                    {
                        not_goal_count++;
                        not_goal_value += 0;
                        //Debug.Log("这个bug真变态啊，把我找了一下午！！！！");
                    }
                }

                /*
                    Debug.Log("持续时间：" + (PlayerPrefs.GetInt("CurrTime") - PlayerPrefs.GetInt("WasteTime")).ToString());
                    Debug.Log("敌机数量：" + PlayerPrefs.GetInt("EnemyCount").ToString());
                    Debug.Log("任务数量：" + PlayerPrefs.GetInt("LanjieCount").ToString());
                    Debug.Log("空袭密度：" + PlayerPrefs.GetInt("AttackP").ToString());
                    Debug.Log("单发概率：" + GameDefine.percent.ToString());
                    Debug.Log("单目标拦截：" + GameDefine.percent.ToString());
                    Debug.Log("拦截失败的次数：" + ((float)not_goal_value / not_goal_count).ToString("0.00"));
                    Debug.Log("友方被打击的次数：" + behit_boat_count.ToString());
                    Debug.Log("友方被打击的血量：" + (behit_value / 7).ToString());
                    Debug.Log("友方被打击的血量：" + (behit_value / 7).ToString());
                    float k = (float)GameData.Instance.behit_key.Count / PlayerPrefs.GetInt("EnemyCount");
                    Debug.Log("拦截百分比：" + (1 - k).ToString("0.00"));
    */


                GameObject.Find("wastetime").GetComponent<Text>().text = (PlayerPrefs.GetInt("CurrTime") - PlayerPrefs.GetInt("WasteTime")).ToString();
                GameObject.Find("planecount").GetComponent<Text>().text = PlayerPrefs.GetInt("EnemyCount").ToString();
                GameObject.Find("taskcount").GetComponent<Text>().text = PlayerPrefs.GetInt("LanjieCount").ToString();
                GameObject.Find("attackp").GetComponent<Text>().text = PlayerPrefs.GetInt("AttackP").ToString();
                GameObject.Find("persent").GetComponent<Text>().text = GameDefine.percent.ToString();

                if (PlayerPrefs.GetInt("LanjieCount") == 0)
                {
                    GameObject.Find("sucesscount").GetComponent<Text>().text = "0";
                }
                else
                {
                    float x = (float)PlayerPrefs.GetInt("LanjieCount") / PlayerPrefs.GetInt("EnemyCount");
                    x = x / 2;
                    GameObject.Find("sucesscount").GetComponent<Text>().text = x.ToString("0.00");
                }

                if (not_goal_count == 0)
                {
                    GameObject.Find("failcount").GetComponent<Text>().text = 0.ToString();
                }
                else
                {
                    GameObject.Find("failcount").GetComponent<Text>().text = ((float)not_goal_value / not_goal_count).ToString("0.00");
                }

                GameObject.Find("behitcount").GetComponent<Text>().text = behit_boat_count.ToString();
                GameObject.Find("bihitxueliang").GetComponent<Text>().text = (behit_value / 7).ToString();
                GameObject.Find("behitplanecount").GetComponent<Text>().text = PlayerPrefs.GetInt("EnemyCount").ToString();
                float l = (float)GameData.Instance.behit_key.Count / PlayerPrefs.GetInt("EnemyCount");
                GameObject.Find("successpersent").GetComponent<Text>().text = (1 - l).ToString("0.00");
            }
            else
            {
                GameDefine.canShowGameAnalyse = false;
                Destroy(VictoryPanel);
            }
        }

        #endregion

        #region GUI中清除特效MuteRay键
        if (GUI.Button(new Rect(940, 660, 80, 20), GameDefine.GUIMuteWarningRay))
        {
            if (GameDefine.MuteWarningRay)
            {
                GameDefine.MuteWarningRay = false;
            }
            else
            {
                GameDefine.MuteWarningRay = true;
            }
        }
        #endregion

        #region GUI中清除特效MuteRay键
        if (GUI.Button(new Rect(1030, 660, 80, 20), GameDefine.GUIMuteWatchRay))
        {
            if (GameDefine.MuteWatchRay)
            {
                GameDefine.MuteWatchRay = false;
            }
            else
            {
                GameDefine.MuteWatchRay = true;
            }
        }
        #endregion

        #region GUI中清除特效MuteRay键
        if (GUI.Button(new Rect(1120, 660, 80, 20), GameDefine.GUIMuteFireRay))
        {
            if (GameDefine.MuteFireRay)
            {
                GameDefine.MuteFireRay = false;
            }
            else
            {
                GameDefine.MuteFireRay = true;
            }
        }
        #endregion
    }
}

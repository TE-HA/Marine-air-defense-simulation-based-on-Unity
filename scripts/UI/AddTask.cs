using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTask : MonoBehaviour
{
    #region 添加任务Panel参数定义
    private Button ButtonAddTask;
    private Button ButtonBegin;
    private Button ButtonRandom;
    private Text EnemyTaskCount;
    private Text EnemyCount;
    private GameObject InputField;

    private int id;
    private string target;
    private string from;
    private int toward;
    private double time;

    private string type = "attack";
    private float queue = -1;
    #endregion

    #region 根据id寻找对应的控件
    public InputField FindInputField(int rows)
    {
        string id = "InputField" + rows;
        InputField result = GameObject.Find(id).GetComponent<InputField>();
        return result;
    }

    public Dropdown FindDropdown(int rows)
    {
        string id = "Dropdown" + rows;
        Dropdown result = GameObject.Find(id).GetComponent<Dropdown>();
        return result;
    }
    #endregion

    // Use this for initialization
    void Start()
    {
        #region 初始化暂停、静音以供输入
        if (Time.timeScale == 0)
        {
            return;
        }
        Time.timeScale = 0;
        GameManger.Instance.MuteAll();
        #endregion

        InputField = GameObject.Find("InputMenu");
      
        #region 按钮监听
        EnemyCount = GameObject.Find("EnemyCount").GetComponent<Text>();
        EnemyTaskCount = GameObject.Find("EnemyTaskCount").GetComponent<Text>();

        ButtonAddTask = GameObject.Find("ButtonAddTask").GetComponent<Button>();
        ButtonAddTask.onClick.AddListener(AddTaskClick);

        ButtonBegin = GameObject.Find("ButtonBegin").GetComponent<Button>();
        ButtonBegin.onClick.AddListener(Begin);

        ButtonRandom = GameObject.Find("ButtonRandom").GetComponent<Button>();
        ButtonRandom.onClick.AddListener(RandomTask);


        Count();
        #endregion
    }

    #region 敌军数量计算
    void Count() {
        EnemyCount.text = PlayerPrefs.GetInt("EnemyCount").ToString();
        EnemyTaskCount.text = PlayerPrefs.GetInt("EnemyTaskCount").ToString();
    }
    #endregion

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    #region 数据库操作方法
    public void SqlTask(string sql_task)
    {
        MySqlT.Instance.DealSqlToSet(sql_task);
    }
    #endregion


    #region 同时更改all_task and weapon_task数据库
    public void AddToDateBase(int id, string target, string form, int toward, double time)
    {
        string sql_all_task = "INSERT INTO `graduate`.`all_task` (`all_task_type`, `all_task_id`, `all_task_status`,`all_task_get`) VALUES('attack', '" + id + "', 'pending','no')";
        try
        {
            SqlTask(sql_all_task);
        }
        catch
        {
            Debug.LogError("检查添加数据");
        }


        string sql_weapon_task = "INSERT INTO `graduate`.`weapon_task` (`Tid`, `TTarget`, `TFrom`, `TType`, `TQueue`, `TTime`, `TToward`) VALUES ('" + id + "', '" + target + "', '" + from + "', 'attack', '-1', '" + time + "', '" + toward + "')";
        try
        {
            SqlTask(sql_weapon_task);
        }
        catch
        {
            Debug.LogError("检查添加数据");
        }
    }
    #endregion


    #region 初始化输入框，重置为空
    public void initInput()
    {
        for (int i = 1; i <= 15; i++)
        {
            FindInputField(i).text = string.Empty;
        }
    }
    #endregion


    #region 添加随机敌机任务
    public void RandomTask()
    {
        for (int i = 1; i <= 10; i++)
        {
            id = PlayerPrefs.GetInt("TaskID");
            PlayerPrefs.SetInt("TaskID", id + 1);

            target = "km_main";

            from = "zhanji_" + PlayerPrefs.GetInt("EnemyPlaneCount");
            PlayerPrefs.SetInt("EnemyPlaneCount", 1 + PlayerPrefs.GetInt("EnemyPlaneCount"));
            toward = Random.Range(0,360);
            time = PlayerPrefs.GetInt("CurrTime")+10*i-5;

            PlayerPrefs.SetInt("EnemyCount", PlayerPrefs.GetInt("EnemyCount") + 1);
            PlayerPrefs.SetInt("EnemyTaskCount", PlayerPrefs.GetInt("EnemyTaskCount") + 1);
            Count();            
            AddToDateBase(id, target, from, toward, time);
        }
        Debug.Log("添加成功");
    }
    #endregion


    #region 按钮监听事件
    public void Begin()
    {
        InputField.SetActive(false);
        Time.timeScale = 1;
        GameManger.Instance.UnMuteAll();
        GameDefine.CanGetTask=true;
    }

    public void AddTaskClick()
    {
        for (int i = 1; i <= 5; i++)
        {
            if (FindInputField(i).text == string.Empty || FindInputField(i + 5).text == string.Empty || FindInputField(i + 10).text == string.Empty)
            {
                return;
            }
            else
            {
                #region 参数添加设置
                id = PlayerPrefs.GetInt("TaskID");
                PlayerPrefs.SetInt("TaskID", id + 1);

                Dropdown d = FindDropdown(i);
                target = d.options[d.value].text;

                from = "zhanji_" + FindInputField(i + 10).text;
                toward = int.Parse(FindInputField(i).text);
                time = double.Parse(FindInputField(i + 5).text);

                PlayerPrefs.SetInt("EnemyCount", PlayerPrefs.GetInt("EnemyCount") + 1);
                PlayerPrefs.SetInt("EnemyTaskCount", PlayerPrefs.GetInt("EnemyTaskCount") + 1);
                Count();

                AddToDateBase(id, PlayerPrefs.GetString(target), from, toward, time);
                #endregion
            }
        }
        initInput();
        Debug.Log("添加成功");
    }

    #endregion
}

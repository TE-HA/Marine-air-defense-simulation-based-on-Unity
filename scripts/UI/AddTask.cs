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
    private Button ButtonAddBoat;
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

        ButtonAddBoat = GameObject.Find("ButtonAddBoat").GetComponent<Button>();
        ButtonAddBoat.onClick.AddListener(AddBoat);

        Count();
        #endregion
    }

    #region 敌军数量计算
    void Count()
    {
        EnemyCount.text = PlayerPrefs.GetInt("EnemyCount").ToString();
        EnemyTaskCount.text = PlayerPrefs.GetInt("EnemyTaskCount").ToString();
    }
    #endregion

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

    #region 同时更改all_task and addobj_task数据库
    public void AddObjDataBase(int id, string target, float x, float y)
    {
        string sql_all_task = "INSERT INTO `graduate`.`all_task` (`all_task_type`, `all_task_id`, `all_task_status`,`all_task_get`) VALUES('addobj', '" + id + "', 'pending','no')";
        try
        {
            SqlTask(sql_all_task);
        }
        catch
        {
            Debug.LogError("检查添加数据");
        }


        string sql_addobj_task = "INSERT INTO `graduate`.`addobj_task` (`Aid`, `Atarget`, `Ax`, `Ay`) VALUES ('" + id + "', '" + target + "', '" + x + "','" + y + "')";
        try
        {
            SqlTask(sql_addobj_task);
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
        for (int i = 1; i <= 25; i++)
        {
            FindInputField(i).text = "";
        }
    }
    #endregion
    int lunci = 1;
    #region 添加随机敌机任务
    public void RandomTask()
    {
        
        for (int i = 1; i <= 10; i++)
        {
           
            target = "km_main";

            //from = "zhanji_" + PlayerPrefs.GetInt("EnemyPlaneCount");
            //PlayerPrefs.SetInt("EnemyPlaneCount", 1 + PlayerPrefs.GetInt("EnemyPlaneCount"));
            toward = Random.Range(0, 360);
            from = "zhanji_" + (i+(lunci-1)*10).ToString();
            for (int j = 0; j < 3; j++)
            {
         id = PlayerPrefs.GetInt("TaskID");
            PlayerPrefs.SetInt("TaskID", id + 1);

                time = PlayerPrefs.GetInt("CurrTime") + 70 * i + 20 * j - 60;
                AddToDateBase(id, target, from, toward, time);

                PlayerPrefs.SetInt("EnemyCount", PlayerPrefs.GetInt("EnemyCount") + 1);
                PlayerPrefs.SetInt("EnemyTaskCount", PlayerPrefs.GetInt("EnemyTaskCount") + 1);
            }
        }
        Count();
        Debug.Log("添加成功");
        lunci++;
    }
    #endregion


    #region 按钮监听事件

    #region 开始/继续仿真
    public void Begin()
    {
        InputField.SetActive(false);
        Time.timeScale = 1;
        GameManger.Instance.UnMuteAll();
        GameDefine.CanGetTask = true;
    }
    #endregion

    #region 添加敌机攻击任务
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

    #region 添加舰船，位置是基于航母的偏移量
    public void AddBoat()
    {
        for (int i = 16; i <= 20; i++)
        {
            if (FindInputField(i).text == string.Empty || FindInputField(i + 5).text == string.Empty)
            {
                return;
            }
            else
            {
                id = PlayerPrefs.GetInt("TaskID");
                PlayerPrefs.SetInt("TaskID", id + 1);

                Dropdown d = FindDropdown(i - 10);
                target = PlayerPrefs.GetString(d.options[d.value].text);
                float offset_x = float.Parse(FindInputField(i).text);
                float offset_y = float.Parse(FindInputField(i + 5).text);
                AddObjDataBase(id, target, offset_x, offset_y);
            }
        }
        initInput();
    }
    #endregion
    #endregion
}

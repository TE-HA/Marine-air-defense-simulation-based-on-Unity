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
    private float kill;
    private int toward;
    private double time;

    private string type = "attack";
    private float queue = -1;

    public static int lunci = 5;
    public static int every_lunci_plane_count = 20;

    public static int per_plane_split_time = 2;
    public static int every_lunci_time = every_lunci_plane_count * per_plane_split_time + 10;

    #endregion

    public string[] ZhanjianName = new string[7];

    public void initZhanjian()
    {
        ZhanjianName[0] = "km_main";
        for (int i = 1; i <= 6; i++)
        {
            ZhanjianName[i] = "km_" + i;
        }
    }

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
        #region init zhanjian name
        initZhanjian();
        #endregion

        #region 初始化暂停、静音以供输入
        if (Time.timeScale == 0)
        {
            return;
        }
        Time.timeScale = 0;
        GameManger.Instance.MuteAll();
        #endregion

        InputField = GameObject.Find("InputMenu");

        PlayerPrefs.SetInt("AttackP",60/per_plane_split_time);
        PlayerPrefs.SetInt("BeginTime", PlayerPrefs.GetInt("CurrTime"));

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
        EnemyTaskCount.text = (PlayerPrefs.GetInt("EnemyTaskCount") * 2).ToString();
    }
    #endregion

    void FixedUpdate()
    {
    }
    public string RandomEnemyName()
    {
        return ZhanjianName[Random.Range(0, 7)];
    }

    #region 初始化输入框，重置为空
    public void initInput()
    {
        for (int i = 1; i <= 25; i++)
        {
            FindInputField(i).text = "";
        }
    }
    #endregion


    #region 添加随机敌机任务
    public void RandomTask()
    {
        for (int i = 1; i <= lunci; i++)
        {
            for (int j = 1; j <= every_lunci_plane_count; j++)
            {
                target = RandomEnemyName();
                toward = Random.Range(0, 360);
                from = "zhanji_" + PlayerPrefs.GetInt("EnemyPlaneCount");
                PlayerPrefs.SetInt("EnemyPlaneCount", 1 + PlayerPrefs.GetInt("EnemyPlaneCount"));

                id = PlayerPrefs.GetInt("TaskID");
                PlayerPrefs.SetInt("TaskID", id + 1);
                kill = Random.Range(10,500);

                time = PlayerPrefs.GetInt("CurrTime") + i * every_lunci_time + j * per_plane_split_time - every_lunci_time;
                MySqlT.Instance.AddToAttackDataBase(id, target, from, kill, toward, time);

                PlayerPrefs.SetInt("EnemyCount", PlayerPrefs.GetInt("EnemyCount") + 1);
                PlayerPrefs.SetInt("EnemyTaskCount", PlayerPrefs.GetInt("EnemyTaskCount") + 1);
            }
        }
        Count();
        //Debug.Log("添加成功");
    }
    #endregion


    #region 按钮监听事件

    #region 开始/继续仿真
    public void Begin()
    {
        //InputField.SetActive(false);
        Destroy(InputField);
        Time.timeScale = 1;
        GameManger.Instance.UnMuteAll();
        GameDefine.CanGetTask = true;
        PlayerPrefs.SetInt("WasteTime", PlayerPrefs.GetInt("CurrTime"));
        
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
                kill = Random.Range(6, 20);
                PlayerPrefs.SetInt("EnemyCount", PlayerPrefs.GetInt("EnemyCount") + 1);
                PlayerPrefs.SetInt("EnemyTaskCount", PlayerPrefs.GetInt("EnemyTaskCount") + 1);
                Count();

                MySqlT.Instance.AddToAttackDataBase(id, PlayerPrefs.GetString(target), from, kill, toward, time);
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
                MySqlT.Instance.AddObjDataBase(id, target, offset_x, offset_y);
            }
        }
        initInput();
    }
    #endregion
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    //单例可存储数据文件
    private static GameData _Instance;
    public static GameData Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new GameData();
            }
            return _Instance;
        }
    }


    //enemy
    private static bool daodan_flag = false;
    private static bool daodan_plane = false;
    public List<GameObject> EnemyDaodan = new List<GameObject>();
    public List<GameObject> EnemyPlane = new List<GameObject>();
    public watching[] watchAssets = new watching[6 * GameDefine.watchingAssets];

    public static List<int> dandaoCount = new List<int>();
    public static List<int> dandaoCountAfter = new List<int>();

    //message
    public static bool canShow = false;
    //
    //1:敌机攻击
    //2:战舰反击
    //3:战舰移动
    //4:拦截导弹成功
    //5:拦截失败
    //6:拦截飞机成功
    public static int messageType = -1;

    public static string message = string.Empty;
}

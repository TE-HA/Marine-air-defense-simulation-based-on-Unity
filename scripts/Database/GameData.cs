using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    //单例可存储数据文件
    private static GameData _Instance;
    private static readonly object InstanceLock = new object();
    public static GameData Instance
    {
        get
        {
            //利用双锁模式避免高并发下非单例模式
            if (_Instance == null)
            {
                lock (InstanceLock)
                {
                    if (_Instance == null)
                    {
                        _Instance = new GameData();
                    }
                }
            }
            return _Instance;
        }
    }
    //enemy
    private static bool daodan_flag = false;
    private static bool daodan_plane = false;
    public List<GameObject> EnemyDaodan = new List<GameObject>();
    public List<GameObject> EnemyPlane = new List<GameObject>();
    public Dictionary<GameObject, bool> isAdded = new Dictionary<GameObject, bool>();
    public Dictionary<string, int[]> target_watch = new Dictionary<string, int[]>();
    public watching[] watchAssets = new watching[6 * GameDefine.watchingAssets];
    public Dictionary<string, string> behitInfo = new Dictionary<string, string>();

    public void FreeWatchAssets(int index, string zhanjian_index)
    {
        //index=制导资源池中的下标
        //zhanjian_index=发射它的战舰的下标，是用来更新资源信息面板的
        watchAssets[index].used = false;
        int num = int.Parse(zhanjian_index.Substring(3, 1));
        GameData.Instance.watch[num - 1]++;
    }
    public float[] xueliang = new float[7];

    public int[] daodan = new int[6];
    public int[] fire = new int[6];
    public int[] warning = new int[6];
    public int[] watch = new int[6];
    
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

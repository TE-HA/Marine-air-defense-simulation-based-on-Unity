using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData{
    //enemy
    public static List<GameObject> enemyDaodan = new List<GameObject>();
    public static List<GameObject> enemyPlane = new List<GameObject>();

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

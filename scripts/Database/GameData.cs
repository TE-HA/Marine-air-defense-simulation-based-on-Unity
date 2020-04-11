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
    public static int messageType = -1;

    public static string message = string.Empty;
}

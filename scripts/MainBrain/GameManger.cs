using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region 枚举GameObject类  没用
public enum CharacterType
{
    km_1,
    km_2,
    km_3,
    km_main,
    Plane_Warning,
}
#endregion

public class GameManger
{
    //单例可存储数据文件
    private static GameManger _Instance;
    public static GameManger Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new GameManger();
            }
            return _Instance;
        }
    }
    #region 切换场景
    public void LoadSence(string senceName)
    {
        Application.LoadLevel(senceName);
    }
    #endregion

    #region 加载资源
    public T LoadResources<T>(string path) where T : Object
    {
        Object obj = Resources.Load(path);
        if (obj == null)
        {
            return null;
        }
        return (T)obj;
    }
    #endregion

    #region CharacterType没啥用应该
    public CharacterType CharacterType
    {
        get
        {
            return CharacterType.km_1;
        }
    }
    #endregion

    #region 获取屏幕中心点世界坐标
    public Vector3 MiddlePoint()
    {
        try
        {
            Vector3 center = Vector3.zero;
            Ray ray = GameDefine.CurrentCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));//射线

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))//发射射线(射线，射线碰撞信息，射线长度，射线会检测的层级)
            {
                center = new Vector3(hit.point.x, 0, hit.point.z);
            }

            //Debug.Log(center);
            return center;
        }
        catch {
            return Vector3.zero;
        }

    }
    #endregion
   
    
    #region 屏蔽海洋
    public void OceanActive() {

    }

    #endregion

    #region 获取战场中心点世界坐标（航母）
    public Vector3 BoatMiddlePoint() {
        return GameObject.Find("km_main").transform.position;
    }
    #endregion

    #region OnGUI中对声音的管理
    GameObject audio = GameObject.Find("Audio");
    public void MuteAll()
    {

        audio.SetActive(false);
    }

    public void UnMuteAll()
    {
        audio.SetActive(true);
    }
    #endregion

    #region 后续功能添加
    //
    #endregion

    #region 概率击毁
    public bool Percent() {
        int a = Random.Range(0, 100);
        if (a < GameDefine.percent * 100)
        {
            return true;
        }
        else {
            return false;
        }
    }
    #endregion

    #region 返回两GameObject的距离
    public float DistanceBetweenTwoGameObject(Transform first, Transform second)
    {
        float x = first.position.x - second.position.x;
        float y = first.position.y - second.position.y;
        float z = first.position.z - second.position.z;
        return Mathf.Sqrt(x * x + y * y + z * z);
    }
    #endregion

 #region 返回两Vector3的距离
    public float DistanceBetweenTwoVector3(Vector3 first, Vector3 second)
    {
        float x =first.x - second.x;
        float y = first.y - second.y;
        float z = first.z - second.z;
        return Mathf.Sqrt(x * x + y * y + z * z);
    }
    #endregion

   }

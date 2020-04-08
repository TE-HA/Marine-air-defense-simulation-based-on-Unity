using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
public class weapon : MonoBehaviour
{
    #region 定义weapon变量
    private bool used = false;
    private bool qingqiu = false;
    public int all_task_id;
    private GameObject weaponself;
    private GameObject plane;

    private string from;
    private string target;
    private string type;
    private float queue;
    private int toward;
    private double time;
    #endregion


    private void Start()
    {
        weaponself = gameObject;
    }

    #region 敌机攻击
    public void FirePlane(string from, string target)
    {
        GameObject _from = GameObject.Find(from);
        GameObject _target = GameObject.Find(target);
        if (_target == null)
        {
            return;
        }
        GameObject instance = null;
        if (_from == null)
        {
            return;
        }
        instance = (GameObject)Instantiate(Resources.Load(GameDefine.DaodanPlane), _from.transform.Find("FirePoint").position, plane.transform.rotation);
        PlayerPrefs.SetInt(GameDefine.EnemyPlaneDaodanCount, PlayerPrefs.GetInt(GameDefine.EnemyPlaneDaodanCount) + 1);
        instance.name = "planedaodan_" + PlayerPrefs.GetInt(GameDefine.EnemyPlaneDaodanCount);
        GameData.enemyDaodan.Add(instance);

        instance.GetComponent<fire_daodan_plane>().target = _target.transform;
        instance.GetComponent<fire_daodan_plane>().from = _from.transform;
        //Destroy(gameObject);


        if (instance == null)
        {
            return;
        }
    }
    #endregion



    #region 战舰反击
    public void FireZhanjian(string from, string target)
    {
        GameObject _from = GameObject.Find(from);
        GameObject _target = GameObject.Find(target);
        if (_target == null)
        {
            return;
        }
        // GameObject.Find(from_A).GetComponent<spaceFire>().Fire(_from,_target);
        GameObject instance = null;
        if (_from == null)
        {
            return;
        }
        instance = (GameObject)Instantiate(Resources.Load(GameDefine.DaodanZhanjian), _from.transform.Find("FirePoint").position, transform.rotation);
        instance.GetComponent<fire_daodan>().target = _target.transform;
        instance.GetComponent<fire_daodan>().from = _from.transform;
        //Debug.Log(_target+" "+_from);
        //Destroy(gameObject);

        if (instance == null)
        {
            return;
        }
    }
    #endregion


    #region 获取weapon表中的武器任务
    public DataTable GetWeaponTask(string sql_weapon_task)
    {
               DataSet ds = MySqlT.Instance.DealSqlToSet(sql_weapon_task);
        DataTable dt = ds.Tables[0];
        if (dt == null)
        {
            return null;
        }
        return dt;
    }
    #endregion


    #region 出生点设置（敌机）
    public Vector3 GetBornPoint(int toward)
    {
        Vector3 bornPoint = new Vector3();
        int lenth = 2500;
        bornPoint.x = lenth * Mathf.Sin(toward * Mathf.PI / 180);
        bornPoint.z = lenth * Mathf.Cos(toward * Mathf.PI / 180);
        bornPoint.y = 500;
        //Debug.Log(bornPoint);
        return bornPoint;
    }
    #endregion


    #region 对已经操作的武器任务进行数据库更新
    public void UpdateWeaponTask(string sql_weapon_task)
    {
        MySqlT.Instance.DealSqlToSet(sql_weapon_task);
    }
    #endregion

    private void FixedUpdate()
    {
        #region 确保只会请求一次数据
        if (!qingqiu)
        {
            DataTable dt = GetWeaponTask(MySqlT._get_weapon_task + all_task_id + ")");
            type = dt.Rows[0][3].ToString();
            from = dt.Rows[0][2].ToString();
            target = dt.Rows[0][1].ToString();
            time = double.Parse(dt.Rows[0][5].ToString());
            toward = int.Parse(dt.Rows[0][6].ToString());
            //Debug.Log(type + " " + from + " " + target);
            qingqiu = true;
        }
        #endregion

        //判断时间
        if (time == PlayerPrefs.GetInt("CurrTime"))
        {
            switch (type)
            {

                #region 判断武器攻击类型
                case "fire":
                    GameObject find = GameObject.Find(from);
                    FireZhanjian(find.name, target);
                    Debug.Log("[战舰反击]：由 "+PlayerPrefs.GetString(find.name)+" 发射，拦截 "+target+" 导弹");
                    break;
                case "attack":
                    Vector3 bornPoint = GetBornPoint(toward);
                    /*GameObject find_attack;
                    try
                    {
                        find_attack = GameObject.Find(from);
                        FirePlane(find_attack.name, target);
                    }
                    catch
                    {*/
                    plane = (GameObject)Instantiate(Resources.Load(GameDefine.EnemyPlane), bornPoint, transform.rotation);
                    plane.name = from;
                    plane.transform.forward = new Vector3(0, 500, 0) - bornPoint;
                    GameData.enemyPlane.Add(plane);
                    FirePlane(plane.name, target);
                    //Debug.Log("mubiao"+target);
                    Debug.Log("[敌机攻击]：由敌机"+plane.name+" 发射，攻击 "+PlayerPrefs.GetString(target)+" 战舰");
                    // }
                    break;
                    #endregion

            }
            //finally
            string _update_finished = "update graduate.all_task set all_task_status='finished' where (all_task_id =" + all_task_id + ") and (all_task_type='" + type + "')";
            UpdateWeaponTask(_update_finished);
            Destroy(weaponself);
        }
    }
}



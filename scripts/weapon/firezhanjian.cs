using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class firezhanjian : MonoBehaviour {
  #region 定义fire变量
    private bool used = false;
    private bool qingqiu = false;
    public int all_task_id;
    private string from;
    private string target;
    private double time;
    #endregion

    private void Start()
    { }


    #region 战舰反击
    public void FireZhanjian(string from, string target)
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
        instance = (GameObject)Instantiate(Resources.Load(GameDefine.DaodanZhanjian), _from.transform.Find("FirePoint").position, transform.rotation);
        instance.GetComponent<fire_daodan>().target = _target.transform;
        instance.GetComponent<fire_daodan>().from = _from.transform;
        GameObject.Find(from).GetComponent<fire>()._target = _target;

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
            DataTable dt = GetWeaponTask(MySqlT._get_fire_task + all_task_id + ")");
            from = dt.Rows[0][1].ToString();
            target = dt.Rows[0][2].ToString();
            time = double.Parse(dt.Rows[0][3].ToString());
            //Debug.Log(type + " " + from + " " + target);
            qingqiu = true;
        }
        #endregion

        //判断时间
        if (time == PlayerPrefs.GetInt("CurrTime"))
        {
            GameObject find = GameObject.Find(from);
            try
            {
                FireZhanjian(find.name, target);
            }
            catch
            {
                Debug.Log("[彩蛋出现]:弹药耗尽！");
            }
            //Debug.Log("[战舰反击]：由 " + PlayerPrefs.GetString(find.name) + " 发射，拦截 " + target + " 导弹");

            #region
            GameData.messageType = 2;
            GameData.message = " 由 " + PlayerPrefs.GetString(find.name) + " 发射，拦截 " + target + " 导弹";
            GameData.canShow = true;
            #endregion

            //finally
            string _update_finished = "update graduate.all_task set all_task_status='finished' where (all_task_id =" + all_task_id + ") and (all_task_type='fire')";
            UpdateWeaponTask(_update_finished);
            Destroy(gameObject);
        }
    }

}

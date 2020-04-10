using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;


#region 定义枚举类型
public enum TaskType
{
    fire,
    move,
}


public enum FireType
{
    Zhanjian,
    Plane,
}
public enum TaskStatus
{
    pending,
    finish,
}
#endregion


public class AutoControl : MonoBehaviour
{

    private string currTask = string.Empty;


    public void SqlTask(string sql_task)
    {
        MySqlT.Instance.DealSqlToSet(sql_task);
    }


    #region 获取当前所有任务并将数据库get属性设置为yes，然后执行
    public void GetAllTask(string sql_alltask)
    {
        DataSet ds = MySqlT.Instance.DealSqlToSet(sql_alltask);
        DataTable dt = ds.Tables[0];

        if (dt == null)
        {
            return;
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            switch (dt.Rows[i][0].ToString())
            {
                #region 得到任务类型并区分
                case "attack":
                    GameObject instance = (GameObject)Instantiate(Resources.Load(GameDefine.Weapon), transform.position, transform.rotation);
                    instance.GetComponent<weapon>().all_task_id = (int)dt.Rows[i][1];
                    break;
                case "fire":
                    GameObject instance_fire = (GameObject)Instantiate(Resources.Load(GameDefine.Weapon), transform.position, transform.rotation);
                    instance_fire.GetComponent<weapon>().all_task_id = (int)dt.Rows[i][1];
                    break;
                case "move":
                    GameObject instance_move = (GameObject)Instantiate(Resources.Load(GameDefine.Move), transform.position, transform.rotation);
                    instance_move.GetComponent<move>().all_task_id = (int)dt.Rows[i][1];
                    break;
                 case "addobj":
                    GameObject instance_addobj = (GameObject)Instantiate(Resources.Load(GameDefine.addobj), transform.position, transform.rotation);
                    instance_addobj.GetComponent<addobj>().addobj_id = (int)dt.Rows[i][1];
                    break;

                default:
                    break;
                    #endregion
            }
            string UpdateSql = "UPDATE `graduate`.`all_task` SET `all_task_get` = 'yes' WHERE (`all_task_type` = '"+dt.Rows[i][0].ToString()+"') and (`all_task_id` = '"+(int)dt.Rows[i][1]+"')";
            //Debug.Log(UpdateSql);
            SqlTask(UpdateSql);
        }
    }
    #endregion



    public void OnGUI()
    {

    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //task
        if (GameDefine.CanGetTask)
        {
            Debug.Log("获取任务");
            GetAllTask(MySqlT._get_all_task);
            GameDefine.CanGetTask = false;
        }
    }
}

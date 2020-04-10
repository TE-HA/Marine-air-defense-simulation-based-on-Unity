using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class addobj : MonoBehaviour
{
    public bool used = false;
    public int addobj_id;
    // Use this for initialization
    void Start()
    {

    }

    public void GetAddobjTask(string sql_move_task)
    {
        DataSet ds = MySqlT.Instance.DealSqlToSet(sql_move_task);
        DataTable dt = ds.Tables[0];
        if (dt == null)
        {
            return;
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string add_obj = dt.Rows[i][1].ToString();
            float move_task_x = (float)dt.Rows[i][2];
            float move_task_z = (float)dt.Rows[i][3];
            AutoGenarate(add_obj, move_task_x, move_task_z);
        }
    }

    public void AutoGenarate(string add_obj, float x, float z)
    {
        Vector3 hangmu = GameManger.Instance.BoatMiddlePoint();
        GameObject instance_hangmu = (GameObject)Instantiate(Resources.Load(GameDefine.addobjname + add_obj), new Vector3(hangmu.x + x, transform.position.y, hangmu.z + z), transform.rotation);
        instance_hangmu.transform.position = new Vector3(hangmu.x + x, transform.position.y, hangmu.z + z);
    }

    public void UpdateMoveTask(string sql_move_task)
    {
        MySqlT.Instance.DealSqlToSet(sql_move_task);
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (!used)
        {
            GetAddobjTask(MySqlT._get_addobj_task + addobj_id + ")");
            string _update_finished = "update graduate.all_task set all_task_status='finished' where (all_task_id =" + addobj_id + ") and (all_task_type='addobj')";
            UpdateMoveTask(_update_finished);
            used = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

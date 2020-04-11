using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class move : MonoBehaviour
{
    private bool used;
    public int all_task_id;
    // Use this for initialization
    void Start()
    {

    }

    public void AutoMove(string move_obj, float x, float z)
    {
        GameObject instance = GameObject.Find(move_obj);
        try
        {
            instance.GetComponent<BoatMove>().enabled = false;
        }
        catch
        {
            //Debug.Log("[改变航向]：改变 " + PlayerPrefs.GetString(instance.name) + " 位置，偏移量为x: " + x + " z: " + z);
            #region 
            GameData.messageType = 3;
            GameData.message = "改变 " + PlayerPrefs.GetString(instance.name) + " 位置，偏移量为x: " + x + " z: " + z;
            GameData.canShow = true;
            #endregion
        }

        instance.AddComponent<BoatMove>().move_task_x = x;
        instance.GetComponent<BoatMove>().move_task_z = z;
    }

    public void GetMoveTask(string sql_move_task)
    {
        DataSet ds = MySqlT.Instance.DealSqlToSet(sql_move_task);
        DataTable dt = ds.Tables[0];
        if (dt == null)
        {
            return;
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string move_obj = dt.Rows[i][1].ToString();
            try
            {
                GameObject.Find(move_obj).GetComponent<BoatMove>();
                float move_task_x = (float)dt.Rows[i][2];
                float move_task_z = (float)dt.Rows[i][3];
                AutoMove(move_obj, move_task_x, move_task_z);
            }
            catch
            {
                Destroy(gameObject);
                return;
            }
        }
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
            GetMoveTask(MySqlT._get_move_task + all_task_id + ")");
            string _update_finished = "update graduate.all_task set all_task_status='finished' where (all_task_id =" + all_task_id + ") and (all_task_type='move')";
            UpdateMoveTask(_update_finished);
            used = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

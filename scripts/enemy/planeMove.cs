using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeMove : MonoBehaviour
{
    public float speed=100f;
    private float minDistance = 4000f;//小于此距离时报警
    private int hit_count = 0;
    private bool isHitsmall = false;
    private bool isHitHeavy = false;

    // Use this for initialization
    void Start() { 
    }

    #region 预警机判断导弹距离战场距离的功能
    public float GetDistance(GameObject enemy) {
        return GameManger.Instance.DistanceBetweenTwoGameObject(enemy.transform,gameObject.transform);
    }
    #endregion

    #region 添加至数据库
    public void SqlTask(string sql_task)
    {
        MySqlT.Instance.DealSqlToSet(sql_task);
    }

    public void AddWeaponTask(int id, string target, string form, int toward, double time)
    {
        string sql_all_task = "INSERT INTO `graduate`.`all_task` (`all_task_type`, `all_task_id`, `all_task_status`,`all_task_get`) VALUES('fire', '" + id + "', 'pending','no')";
        //Debug.Log("增加任务"+sql_all_task);
        try
        {
            SqlTask(sql_all_task);
        }
        catch
        {
            Debug.LogError("检查添加数据");
        }


        string sql_weapon_task = "INSERT INTO `graduate`.`weapon_task` (`Tid`, `TTarget`, `TFrom`, `TType`, `TQueue`, `TTime`, `TToward`) VALUES ('" + id + "', '" + target + "', '" + form + "', 'fire', '1000', '" + time + "', '" + toward + "')";
        //Debug.Log(sql_weapon_task);
        try
        {
            SqlTask(sql_weapon_task);
        }
        catch
        {
            //
            Debug.LogError("检查添加数据");
        }
    }

    public void AddMoveTask(int id, string obj, float offset_x, float offset_y)
    {
        string sql_all_task = "INSERT INTO `graduate`.`all_task` (`all_task_type`, `all_task_id`, `all_task_status`,`all_task_get`) VALUES('move', '" + id + "', 'pending','no')";
        //Debug.Log("增加任务"+sql_all_task);
        try
        {
            SqlTask(sql_all_task);
        }
        catch
        {
            Debug.LogError("检查添加数据");
        }


        string sql_move_task = "INSERT INTO `graduate`.`move_task` (`move_task_id`, `move_task_obj`, `move_task_x`, `move_task_z`) VALUES ('" + id + "', '" + obj + "', '" + offset_x + "', '" + offset_y + "');";
        //Debug.Log(sql_weapon_task);
        try
        {
            SqlTask(sql_move_task);
        }
        catch
        {
            //
            Debug.LogError("检查添加数据");
        }

    }
    #endregion

    #region 预警功能同时添加任务至数据库（指控系统）
    public void Warning()
    {
        try
        {
            int lengh = GameData.enemyDaodan.Count;
            
            for (int i = 0; i < lengh; i++)
            {
                //Debug.Log(GameData.enemyDaodan[i].name);
                if (GetDistance(GameData.enemyDaodan[i]) < minDistance)
                {
                    //指控功能添加。。。。
                    
                    AddWeaponTask(PlayerPrefs.GetInt("TaskID"), GameData.enemyDaodan[i].name, "km_1", -1, PlayerPrefs.GetInt("CurrTime") + 2);
                    PlayerPrefs.SetInt("TaskID", PlayerPrefs.GetInt("TaskID") + 1);

                    AddMoveTask(PlayerPrefs.GetInt("TaskID"),"km_main",200,200);
                    PlayerPrefs.SetInt("TaskID", PlayerPrefs.GetInt("TaskID") + 1);

                    GameData.enemyDaodan.Remove(GameData.enemyDaodan[i]);

                    GameDefine.CanGetTask = true;
                }
                else
                {
                    continue;
                }
            }
        }
        catch { }
    }
    #endregion


    private int jiange = 60;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (jiange<0) {
            Warning();
            jiange = 60;
        }
        else {
            jiange--;
        }
        transform.forward = transform.forward + transform.right * 0.004f - transform.up * hit_count * 0.0005f*0.01f;
        //transform.forward = transform.forward - transform.up * hit_count * 0.0005f * 0.01f;

        transform.position += transform.forward * speed * Time.deltaTime;

        if (isHitHeavy)
        {
            //Destroy(Instantiate(Resources.Load(GameDefine.HitBoomExplosion), transform.position, Quaternion.identity), 1f);
        }
        if (isHitsmall)
        {
            Destroy(Instantiate(Resources.Load(GameDefine.BeHitEffect), transform.position, Quaternion.identity), 1f);
        }

        if (transform.position.y < 5)
        {
            //LoadShip();            
            Destroy(Instantiate(Resources.Load(GameDefine.HitWaterExplosion), transform.position, Quaternion.identity), 1f);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "daodan")
        {
            //Debug.Log("hit");
            hit_count++;
            isHitsmall = true;
            if (hit_count >= 3)
            {
                isHitHeavy = true;
            }
        }
    }
}

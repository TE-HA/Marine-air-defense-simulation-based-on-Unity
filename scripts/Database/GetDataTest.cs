using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 数据库链接测试脚本
public class GetDataTest : MonoBehaviour {
	// Use this for initialization
	void Start () {
        MySqlConnection conn = null;
        for (int i = 1; i <= 1000; ++i)
        {
            //获取连接
            conn = ConnectionPool.getPool().getConnection();
            try
            {
                //数据操作
                MySqlCommand cmd = new MySqlCommand("Select * from graduate.all_task", conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Debug.Log("type"+dr[0]+"ID: " + dr[1] + "，状态：" + dr[2]+"是否get:"+dr[3]);
                }
                dr.Close();
                //将连接添加回连接池中
                ConnectionPool.getPool().closeConnection(conn);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
#endregion

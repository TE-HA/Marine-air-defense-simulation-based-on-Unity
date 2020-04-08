using UnityEngine;
using System;
using System.Data;
using MySql.Data.MySqlClient;

public class MySqlT
{
    #region sql查询语句集合
    //查询所有任务
    public static string _get_all_task = "SELECT * FROM graduate.all_task where (all_task_status='pending')and (all_task_get='no')";
    //查询id为***的weapon任务
    public static string _get_weapon_task = "SELECT * FROM graduate.weapon_task where (Tid=";
    //查询id为***的move任务
    public static string _get_move_task = "SELECT * FROM graduate.move_task where (move_task_id=";
    //更新任务状态
    public static string _update_status = "update graduate.all_task set all_task_get='no'";
    public static string _update_status_pending = "update graduate.all_task set all_task_status='pending'";
    #endregion

    //单例可存储数据文件
    private static MySqlT _Instance;
    public static MySqlT Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new MySqlT();
            }
            return _Instance;
        }
    }

    #region 对sql语句进行处理的主要方法  返回DataSet
    public DataSet DealSqlToSet(string sqlString)
    {
        MySqlConnection conn = null;
        //获取连接
        conn = ConnectionPool.getPool().getConnection();
        try
        {
            //数据操作
            DataSet ds = new DataSet();
            MySqlDataAdapter mySqlAdapter = new MySqlDataAdapter(sqlString, conn);
            mySqlAdapter.Fill(ds);
            //将连接添加回连接池中
            ConnectionPool.getPool().closeConnection(conn);
            return ds;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            return null;
        }
    }
    #endregion


    #region 代码模板
    void find()
    {
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
                    Debug.Log("type" + dr[0] + "ID: " + dr[1] + "，状态：" + dr[2] + "是否get:" + dr[3]);
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
    #endregion

}

#region 原代码(弃用)

/*
//连接类对象
private static MySqlConnection mySqlConnection;
        //IP地址
        private static string host= "localhost";
        //端口号
        //private static string port;
        //用户名
        private static string userName="root";
        //密码
        private static string password="root";
        //数据库名称
        private static string databaseName="graduate";

        /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="_host">ip地址</param>
    /// <param name="_userName">用户名</param>
    /// <param name="_password">密码</param>
    /// <param name="_databaseName">数据库名称</param>
    public MySqlT(string _host, string _userName, string _password, string _databaseName)
    {
        host = _host;
        //port = _port;
        userName = _userName;
        password = _password;
        databaseName = _databaseName;
        OpenSql();
    }

    /// <summary>
    /// 打开数据库
    /// </summary>
    public void OpenSql()
    {
        try
        {
            string mySqlString = string.Format("Database={0};Data Source={1};User Id={2};Password={3}"
                , databaseName, host, userName, password);
            mySqlConnection = new MySqlConnection(mySqlString);
            //if(mySqlConnection.State == ConnectionState.Closed)
            mySqlConnection.Open();
        }
        catch (Exception e)
        {
            throw new Exception("服务器连接失败，请重新检查MySql服务是否打开。" + e.Message.ToString());
        }

    }

    /// <summary>
    /// 关闭数据库
    /// </summary>
    public void CloseSql()
    {
        if (mySqlConnection != null)
        {
            mySqlConnection.Close();
            mySqlConnection.Dispose();
            mySqlConnection = null;
        }
    }
    

    /// <summary>
    /// 执行SQL语句
    /// </summary>
    /// <param name="sqlString">sql语句</param>
    /// <returns></returns>
    /// 
    public DataSet DealSqlToSet(string sqlString)
    {
        OpenSql();
        if (mySqlConnection.State == ConnectionState.Open)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter mySqlAdapter = new MySqlDataAdapter(sqlString, mySqlConnection);
                mySqlAdapter.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception("SQL:" + sqlString + "/n" + e.Message.ToString());
            }
            finally
            {
            }
            return ds;
        }
        CloseSql();
        return null;
    }
}
*/
#endregion


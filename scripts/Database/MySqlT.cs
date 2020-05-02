using UnityEngine;
using System;
using System.Data;
using MySql.Data.MySqlClient;

public class MySqlT
{
    #region sql查询语句集合
    //查询所有任务
    public static string _get_all_task = "SELECT * FROM graduate.all_task where (all_task_status='pending')and (all_task_get='no')";
    //查询id为***的attack任务
    public static string _get_attack_task = "SELECT * FROM graduate.attack_task where (attack_task_id=";
    //查询id为***的fire任务
    public static string _get_fire_task = "SELECT * FROM graduate.fire_task where (fire_task_id=";
    //查询id为***的move任务
    public static string _get_move_task = "SELECT * FROM graduate.move_task where (move_task_id=";
    //查询id为***的addobj任务
    public static string _get_addobj_task = "SELECT * FROM graduate.addobj_task where (Aid=";

    //更新任务状态(测试用)
    public static string _update_status = "update graduate.all_task set all_task_get='no'";
    public static string _update_status_pending = "update graduate.all_task set all_task_status='pending'";

    //查询拦截每发导弹次数
    public static string _count_every_daodan = "SELECT fire_task_target,count(0)  as chongfu FROM graduate.fire_task group by fire_task_target having count(fire_task_target >=1);";

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


    #region 添加至数据库
    #region 数据库访问方法

    public void SqlTask(string sql_task)
    {
        DealSqlToSet(sql_task);
    }
    #endregion

    #region 添加武器反击任务
    public void AddFireTask(int id, string target, string form, double time)
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


        string sql_fire_task = "INSERT INTO `graduate`.`fire_task` (`fire_task_id`, `fire_task_target`, `fire_task_from`, `fire_task_time`) VALUES ('" + id + "', '" + target + "', '" + form + "', '" + time + "')";
        //Debug.Log(sql_weapon_task);
        try
        {
            SqlTask(sql_fire_task);
        }
        catch
        {
            //
            Debug.LogError("检查添加数据");
        }
    }


    #endregion

    #region 添加移动任务
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

    #region 同时更改all_task and weapon_task数据库
    public void AddToAttackDataBase(int id, string target, string form, float kill,int toward, double time)
    {
        string sql_all_task = "INSERT INTO `graduate`.`all_task` (`all_task_type`, `all_task_id`, `all_task_status`,`all_task_get`) VALUES('attack', '" + id + "', 'pending','no')";
        try
        {
            SqlTask(sql_all_task);
        }
        catch
        {
            Debug.LogError("检查添加数据");
        }


        string sql_attack_task = "INSERT INTO `graduate`.`attack_task` (`attack_task_id`, `attack_task_target`, `attack_task_from`, `attack_task_time`, `attack_task_toward`, `attack_task_queue`) VALUES ('" + id + "', '" + target + "', '" + form  + "', '" + time + "', '" + toward + "', '"+kill + "')";
        try
        {
            SqlTask(sql_attack_task);
        }
        catch
        {
            Debug.LogError("检查添加数据");
        }
    }
    #endregion

    #region 同时更改all_task and addobj_task数据库
    public void AddObjDataBase(int id, string target, float x, float y)
    {
        string sql_all_task = "INSERT INTO `graduate`.`all_task` (`all_task_type`, `all_task_id`, `all_task_status`,`all_task_get`) VALUES('addobj', '" + id + "', 'pending','no')";
        try
        {
            SqlTask(sql_all_task);
        }
        catch
        {
            Debug.LogError("检查添加数据");
        }


        string sql_addobj_task = "INSERT INTO `graduate`.`addobj_task` (`Aid`, `Atarget`, `Ax`, `Ay`) VALUES ('" + id + "', '" + target + "', '" + x + "','" + y + "')";
        try
        {
            SqlTask(sql_addobj_task);
        }
        catch
        {
            Debug.LogError("检查添加数据");
        }
    }
    #endregion


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


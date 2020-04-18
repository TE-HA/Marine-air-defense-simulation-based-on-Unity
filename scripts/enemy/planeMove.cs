using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeMove : MonoBehaviour
{
    /*#region 参数设置
    public float speed=100f;
    private int jiange = 60;//时间间隔
    private float minDistance = 10000f;//小于此距离时报警
    private int hit_count = 0;
    private bool isHitsmall = false;
    private bool isHitHeavy = false;
    #endregion

 
    #region 预警机判断导弹距离战场距离的功能
    public float GetDistance(GameObject enemy) {
        return GameManger.Instance.DistanceBetweenTwoGameObject(enemy.transform,gameObject.transform);
    }
    #endregion
    #region 预警功能同时添加任务至数据库（指控系统）
    public void Warning()
    {
        // try
        // {
        int lengh = GameData.EnemyDaodan.Count;

        for (int i = 0; i < lengh; i++)
        {
            //Debug.Log(GameData.enemyDaodan[i].name);
            if (GetDistance(GameData.EnemyDaodan[i]) < minDistance)
            {
                //指控功能添加。。。。
                //taskHeap.Instance.Insert();

               /* MySqlT.Instance.AddWeaponTask(PlayerPrefs.GetInt("TaskID"), GameData.enemyDaodan[i].name, "km_1", -1, PlayerPrefs.GetInt("CurrTime") + 2);
                PlayerPrefs.SetInt("TaskID", PlayerPrefs.GetInt("TaskID") + 1);

                /* AddMoveTask(PlayerPrefs.GetInt("TaskID"),"km_main",200,200);
                 PlayerPrefs.SetInt("TaskID", PlayerPrefs.GetInt("TaskID") + 1);


                GameData.EnemyDaodan.Remove(GameData.EnemyDaodan[i]);
                GameDefine.CanGetTask = true;
            }
            else
            {
                continue;
            }
        }

        int lengh2 = GameData.enemyPlane.Count;

        for (int i = 0; i < lengh2; i++)
        {
            //Debug.Log(GameData.enemyDaodan[i].name);
            if (GetDistance(GameData.enemyPlane[i]) < minDistance)
            {
                //指控功能添加。。。。

                MySqlT.Instance.AddWeaponTask(PlayerPrefs.GetInt("TaskID"), GameData.enemyPlane[i].name, "km_5", -1, PlayerPrefs.GetInt("CurrTime") + 2);
                PlayerPrefs.SetInt("TaskID", PlayerPrefs.GetInt("TaskID") + 1);

                GameData.enemyPlane.Remove(GameData.enemyPlane[i]);
                GameDefine.CanGetTask = true;
            }
            else
            {
                continue;
            }

            //}
            // catch { }
        }
    }
    #endregion


    // Update is called once per frame
    #region 追踪算法
    void FixedUpdate()
    {
        if (jiange<0) {
           // Warning();
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
    #endregion


    #region 检测撞击
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
    #endregion
}*/
}

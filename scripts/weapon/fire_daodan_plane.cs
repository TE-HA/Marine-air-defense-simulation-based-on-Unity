using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fire_daodan_plane : MonoBehaviour
{
    #region 敌机进攻导弹参数设置
    public Transform target;
    public Transform from;
    private float speed = 200f;
    private float RocSpeed = 180f;
    private Vector3 _target;
    private bool effect_1 = false;
    private bool effect_2 = false;
    #endregion

    // Use this for initialization
    void Start()
    {
        _target = target.position - from.position;
        GameData.Instance.isAdded.Add(gameObject, false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        if ((GameDefine.MuteEffect == false))
        {
            ////////////
            //特效1
            ////////////
            Destroy(Instantiate((GameObject)Instantiate(Resources.Load(GameDefine.DaodanFly), transform.Find("point").position, Quaternion.identity)), 3f);
            //effect_1 = true;
        }
        else
        {
            if ((gameObject.transform.position.y < 450) && (GameDefine.CurrentCamera.name == "Camera2D"))
            {
                gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
        }


        float a = Vector3.Angle(transform.forward, _target) / RocSpeed;
        if (a > -0.1f || a < 0.1f)
        {
            transform.forward = Vector3.Slerp(transform.forward, _target, Time.deltaTime / a).normalized;
        }
        else
        {
            transform.forward = Vector3.Slerp(transform.forward, _target, 1).normalized;
        }
        transform.position += transform.forward * speed * Time.deltaTime;


        if (transform.position.y <= -2)
        {
            //Destroy(collision.gameObject);
            if (GameDefine.MuteEffect == false)
            {
                ///////
                ////特效2
                ///////
                if (!effect_2)
                {
                    effect_2 = true;
                    Destroy(Instantiate((GameObject)Instantiate(Resources.Load(GameDefine.HitWaterExplosion), transform.Find("point").position, Quaternion.identity)), 3f);
                }
            }

            destory_free();
        }
    }

    void destory_free()
    {
        try
        {
            int[] end = GameData.Instance.target_watch[gameObject.name];

            GameObject w1 = GameObject.Find(gameObject.name).transform.Find("watch_" + end[0]).gameObject;
            GameObject w2 = GameObject.Find(gameObject.name).transform.Find("watch_" + end[1]).gameObject;
            w1.GetComponent<watching>().FreeWatching();
            w2.GetComponent<watching>().FreeWatching();

            GameData.Instance.target_watch.Remove(gameObject.name);
            GameData.Instance.isAdded.Remove(gameObject);
        }
        catch { }
        finally
        {
            Destroy(gameObject);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == GameDefine.Tag.plane.ToString() || collision.gameObject.tag == GameDefine.Tag.zhanjian.ToString())
        {
            PlayerPrefs.SetInt(collision.gameObject.name + "_info_slider", PlayerPrefs.GetInt(collision.gameObject.name + "_info_slider") - (int)gameObject.GetComponent<dangerValue>().DangerValue);
            Debug.Log(gameObject.name + " " + collision.gameObject.name);
            GameData.Instance.behitInfo.Add(gameObject.name, collision.gameObject.name);
            Debug.Log(GameData.Instance.behitInfo[gameObject.name]);
            destory_free();
        }
        else if (collision.gameObject.tag == GameDefine.Tag.daodan.ToString())
        {
            if (GameManger.Instance.Percent())
            {
                #region
                GameData.messageType = 4;
                GameData.message = " 拦截导弹成功";
                GameData.canShow = true;
                #endregion

                destory_free();
            }
            else
            {
                if (GameManger.Instance.DistanceBetweenTwoGameObject(gameObject.transform, GameObject.Find("km_main").transform) < 3000)
                {
                    gameObject.GetComponent<dangerValue>().DangerValue += 800;
                    taskHeap.Instance.Insert(new TaskNode(gameObject.name, gameObject.GetComponent<dangerValue>().DangerValue));
                }
                else
                {
                    gameObject.GetComponent<dangerValue>().DangerValue *= 2;
                    taskHeap.Instance.Insert(new TaskNode(gameObject.name, gameObject.GetComponent<dangerValue>().DangerValue));
                }
                #region
                GameData.messageType = 5;
                GameData.message = " 拦截导弹失败，将其视为新威胁";
                GameData.canShow = true;
                #endregion
            }
        }
    }
}



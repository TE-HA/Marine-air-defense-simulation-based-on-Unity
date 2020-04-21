using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_daodan : MonoBehaviour
{
    #region 友军反击导弹类：参数设置
    private float m_speed = 250f;
    private float first_speed = 100f;
    private float RocSpeed = 300f;
    // private float RocSpeed_second = 90000000f;
    private float height = 60f;
    public Transform target;
    public Transform from;
    private float accelerlation = 40f;
    private float a_first = 80f;
    private bool jiasu = false;
    private float x = 0.2f;
    private bool isHit = false;

    private bool effect_1 = false;
    private bool effect_2 = false;
    private bool effect_3 = false;

    #endregion


    void Start()
    {
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        if (transform.position.y < height && !jiasu)
        {
            if (GameDefine.MuteEffect == false)
            {
                if (!effect_1)
                {
                    ////////////
                    //特效1
                    ////////
                    Destroy(Instantiate((GameObject)Instantiate(Resources.Load(GameDefine.FireOut), transform.Find("point").position, Quaternion.identity)), 0.1f);
                    effect_1 = true;
                }
            }
            transform.forward = Vector3.up + Vector3.left * x;
            transform.position += 20f * transform.forward * a_first * Time.deltaTime * Time.deltaTime;
        }
        else
        {
            jiasu = true;
            if (transform.position.y > 50)
            {
                RocSpeed = float.MaxValue - 10f;
            }
            if (GameDefine.MuteEffect == false)
            {
                if (!effect_2)
                {
                    ////////////
                    //特效2
                    //////////
                    //effect_2 = true;
                    Destroy(Instantiate((GameObject)Instantiate(Resources.Load(GameDefine.DaodanFly), transform.Find("point").position, Quaternion.identity)), 3f);
                }
            }
            else
            {
                if (GameDefine.CurrentCamera.name == "Camera2D")
                {
                    gameObject.transform.localScale = GameDefine.daodanScale;
                }
            }

            Vector3 _target = (target.position - transform.position).normalized;


            float a = Vector3.Angle(transform.forward, _target) / RocSpeed;

            if (a > -0.1f || a < 0.1f)
            {
                transform.forward = Vector3.Slerp(transform.forward, _target, Time.deltaTime / a).normalized;
            }
            else
            {
                m_speed += accelerlation * Time.deltaTime;
                transform.forward = Vector3.Slerp(transform.forward, _target, 1).normalized;
            }

            transform.position += transform.forward * m_speed * Time.deltaTime;

            if (transform.position.y <= -2)
            {
                Destroy(gameObject);
                if (GameDefine.MuteEffect == false)
                {
                    if (!effect_3)
                    {
                        ////////////
                        //特效3
                        ////////
                        effect_3 = true;
                        Destroy(Instantiate((GameObject)Instantiate(Resources.Load(GameDefine.HitWaterExplosion), transform.Find("point").position, Quaternion.identity)), 3f);
                    }
                }
            }
        }
        //canfire = false;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == GameDefine.Tag.zhanjian.ToString() || collision.gameObject.tag == GameDefine.Tag.daodan.ToString())
        {
            Destroy(gameObject);
            Destroy(Instantiate((GameObject)Instantiate(Resources.Load(GameDefine.HitBoomExplosion), transform.Find("point").position, Quaternion.identity)));
            isHit = true;
        }

        if (collision.gameObject.tag == GameDefine.Tag.plane.ToString())
        {
            #region
            GameData.messageType = 6;
            GameData.message = "拦截 " + collision.gameObject.name + " 飞机成功";
            GameData.canShow = true;
            #endregion

           // Destroy(Instantiate((GameObject)Instantiate(Resources.Load(GameDefine.HitBoomExplosion), transform.Find("point").position, Quaternion.identity)));
            isHit = true;
            Destroy(gameObject);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyplanemove : MonoBehaviour {
    #region 定义参数
    public float speed = 100f;
    private int hit_count = 0;
    private bool isHitsmall = false;
    private bool isHitHeavy = false;
    #endregion
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.forward = transform.forward - transform.up * hit_count*0.01f;

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

        if (gameObject.transform.position.x>3000||gameObject.transform.position.x<-3000||gameObject.transform.position.z>3000||gameObject.transform.position.z<-3000) {
            Destroy(gameObject);
        }
    }

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
}

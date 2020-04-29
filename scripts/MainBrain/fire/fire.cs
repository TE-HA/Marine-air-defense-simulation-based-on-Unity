using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    public GameObject _target;
    public bool used = false;
    public bool ones = false;

    // Use this for initialization
    void Start()
    {

    }

    public void Ray()
    {
        if (!ones)
        {
            GameObject fireRay = (GameObject)Instantiate(Resources.Load(GameDefine.Ray));
            fireRay.name = GameDefine.FireRayName;
            fireRay.GetComponent<RayShot>().from = gameObject;
            fireRay.GetComponent<RayShot>().to = _target;
            fireRay.GetComponent<RayShot>().RayType = GameDefine.RayType.FireRay.ToString();
            ones = true;

            string fire = gameObject.name;
            int num = int.Parse(fire.Substring(3, 1));
            GameData.Instance.fire[num - 1] = 0;
        }
    }

    // Update is called once per frame
    private int fasheshiyan = 120;
    void FixedUpdate()
    {
        if (used && fasheshiyan > 0)
        {
            //正在占用期
            Ray();
            fasheshiyan--;
        }
        else
        {
            used = false;
            ones = false;

            string fire = gameObject.name;
            int num = int.Parse(fire.Substring(3, 1));
            GameData.Instance.fire[num - 1] = 1;

            fasheshiyan = 120;
        }


        if (_target == null)
        {
            //目标消失。立即释放资源
            used = false;
            ones = false;

            fasheshiyan = 120;
        }
    }
}

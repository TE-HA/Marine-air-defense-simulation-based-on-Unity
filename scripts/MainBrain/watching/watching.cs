using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watching : MonoBehaviour
{
    public GameObject _target;
    public GameObject _from;
    public int index = -1;
    public bool used = false;
    public bool ones = false;

    public watching()
    {

    }

    // Use this for initialization
    void Start()
    {

    }

    public void Ray()
    {
        GameObject watchRay = (GameObject)Instantiate(Resources.Load(GameDefine.Ray));
        watchRay.name = GameDefine.WatchRay;
        watchRay.GetComponent<RayShot>().from = _from;
        watchRay.GetComponent<RayShot>().to = _target;
        watchRay.GetComponent<RayShot>().RayType = GameDefine.RayType.WatchRay.ToString();


        string fire = _from.name;
        int num = int.Parse(fire.Substring(3, 1));
        GameData.Instance.watch[num - 1]--;

        ones = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!ones && used)
        {
            Ray();
        }
        if (_target == null)
        {
            if (index != -1)
            {
                GameData.Instance.watchAssets[index].used = false;
                //Debug.Log("我释放了第 " + index + " 号制导资源");
                index = -1;
                string fire = _from.name;
                int num = int.Parse(fire.Substring(3, 1));
                GameData.Instance.watch[num - 1]++;

                Destroy(gameObject);
            }
        }
    }
}

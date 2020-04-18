using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watching : MonoBehaviour
{
    public GameObject _target;
    public int index = -1;
    public bool used = false;
    public bool ones = false;
    // Use this for initialization
    void Start()
    {

    }

    public void Ray()
    {
        if (!ones) {
            GameObject watchRay = (GameObject)Instantiate(Resources.Load(GameDefine.Ray));
            watchRay.name = GameDefine.WatchRay;
            watchRay.GetComponent<RayShot>().from = gameObject;
            watchRay.GetComponent<RayShot>().to = _target;
            watchRay.GetComponent<RayShot>().RayType = GameDefine.RayType.WatchRay.ToString();
            ones = true;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (used) {
            Ray();
        }
        if (_target==null) {
            used = false;
            if (index!=-1) {
                GameData.Instance.watchAssets[index].used = false;
            }
        }
    }
}

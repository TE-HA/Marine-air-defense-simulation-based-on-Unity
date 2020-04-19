using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warning : MonoBehaviour
{
    private bool used = false;
    private GameObject follow;
    private int minDistance = 10000;
    // Use this for initialization
    void Start()
    {

    }

    #region 判断导弹距离战场距离的功能
    public float GetDistance(GameObject enemy)
    {
        if (enemy==null) {
            return float.MaxValue;
        }
        return GameManger.Instance.DistanceBetweenTwoGameObject(enemy.transform, gameObject.transform);
    }
    #endregion
    public void Warning()
    {

        int lengh2 = GameData.Instance.EnemyPlane.Count;

        for (int i = 0; i < lengh2; i++)
        {
            if (GetDistance(GameData.Instance.EnemyPlane[i]) < minDistance)
            {
                follow = GameData.Instance.EnemyPlane[i];

                //warning
                Ray(follow);
                taskHeap.Instance.Insert(new TaskNode(GameData.Instance.EnemyPlane[i].name, GameData.Instance.EnemyPlane[i].GetComponent<dangerValue>().DangerValue));
                GameData.Instance.EnemyPlane.Remove(GameData.Instance.EnemyPlane[i]);
                return;
            }
            else
            {
                continue;
            }
        }

        int lengh = GameData.Instance.EnemyDaodan.Count;
        for (int i = 0; i < lengh; i++)
        {
            if (GetDistance(GameData.Instance.EnemyDaodan[i]) < minDistance)
            {
                follow=GameData.Instance.EnemyDaodan[i];
                Ray(follow);
                taskHeap.Instance.Insert(new TaskNode(GameData.Instance.EnemyDaodan[i].name, GameData.Instance.EnemyDaodan[i].GetComponent<dangerValue>().DangerValue));
                GameData.Instance.EnemyDaodan.Remove(GameData.Instance.EnemyDaodan[i]);
                return;
            }
            else
            {
                continue;
            }
        }
    }


    void Ray(GameObject _target)
    {
        GameObject warningRay = (GameObject)Instantiate(Resources.Load(GameDefine.Ray));
        warningRay.name = GameDefine.WarningRayName;
        warningRay.GetComponent<RayShot>().from = gameObject;
        warningRay.GetComponent<RayShot>().to = _target;
        warningRay.GetComponent<RayShot>().RayType = GameDefine.RayType.WarningRay.ToString();
        used = true;
    }

    void FixedUpdate()
    {
        if (!used)
        {
            Warning();
        }
        if (follow==null) {
            used = false;
        }
    }
}

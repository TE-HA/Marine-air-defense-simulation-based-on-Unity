using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskHeapControler : MonoBehaviour
{
    private int jiange = 60;
    private int dotask = 60;
    private static readonly object fire_task_add = new object();
    // Use this for initialization
    void Start()
    {

    }

    int[] zhidaoAdd(string target)
    {
        int[] end = new int[2];
        #region 制导资源分配

        for (int i = 0; i < 6 * GameDefine.watchingAssets - 1; i++)
        {
            int first = i;
            if (!GameData.Instance.watchAssets[first].used)
            {
                //Debug.Log("我占用了第 " + first + " 号制导资源");
                GameData.Instance.watchAssets[first].used = true;

                GameObject watching = (GameObject)Instantiate(Resources.Load(GameDefine.WatchRayRay));
                watching.name = "watch_" + first;
                watching.transform.SetParent(GameObject.Find(target).transform);
                watching.GetComponent<watching>()._target = GameObject.Find(target);
                watching.GetComponent<watching>()._from = GameObject.Find("km_" + ((first / GameDefine.watchingAssets) + 1).ToString());
                watching.GetComponent<watching>().used = true;
                watching.GetComponent<watching>().ones = false;
                watching.GetComponent<watching>().index = first;

                end[0] = first;
                break;
            }
        }

        for (int i = 6 * GameDefine.watchingAssets - 1; i >= 0; i--)
        {
            int second = i;
            if (!GameData.Instance.watchAssets[second].used)
            {
                //Debug.Log("我占用了第 " + second + " 号制导资源");
                GameData.Instance.watchAssets[second].used = true;

                GameObject watching = (GameObject)Instantiate(Resources.Load(GameDefine.WatchRayRay));
                watching.name = "watch_" + second;
                watching.transform.SetParent(GameObject.Find(target).transform);
                watching.GetComponent<watching>()._target = GameObject.Find(target);
                watching.GetComponent<watching>()._from = GameObject.Find("km_" + ((second / GameDefine.watchingAssets) + 1).ToString());
                watching.GetComponent<watching>().used = true;
                watching.GetComponent<watching>().ones = false;
                watching.GetComponent<watching>().index = second;

                end[1] = second;
            
                break;
            }
        }

        GameData.Instance.target_watch.Add(target, end);
        return end;
        #endregion
    }
    // Update is called once per frame

    void FixedUpdate()
    {
        if (jiange < 0)
        {
            jiange = 60;
            check_watching_assets();
            taskHeap.Instance.Update_Queue();
            //jiange = 60;
        }
        else
        {
            jiange--;
        }

        if (dotask<0) {
            if (taskHeap.Instance.heap[0] == null)
            {
                return;
            }

            TaskNode task = taskHeap.Instance.heap[0];
            taskHeap.Instance.Show();
            taskHeap.Instance.Delete(0);
            //Debug.Log(task.Name+" "+task.Tqueue);
            string target = task.Name;
            string fireUnit = "km_" + FireUnit(target);
            if (fireUnit == "km_")
            {
                /*if (twomitutes < 0)
                {
                    taskHeap.Instance.Insert(new TaskNode(task.Name, task.Tqueue));
                    twomitutes = 60;
                }
                else {
                    twomitutes--;
                }*/
                taskHeap.Instance.Insert(new TaskNode(task.Name, task.Tqueue));

                return;
            }

            if (!GameObject.Find(task.Name).GetComponent<isWatched>().watched)
            {
                int[] end = zhidaoAdd(target);
                GameObject.Find(task.Name).GetComponent<isWatched>().watched = true;
            }

            MySqlT.Instance.AddFireTask(PlayerPrefs.GetInt("TaskID"), target, fireUnit, PlayerPrefs.GetInt("CurrTime") + 1);
            PlayerPrefs.SetInt("TaskID", PlayerPrefs.GetInt("TaskID") + 1);
            GameDefine.CanGetTask = true;
            dotask = 60;
        }
        else {
            dotask--;
        }
    }

    private void check_watching_assets()
    {
        int watch_not_used = 0;
        for (int i = 0; i < GameData.Instance.watch.Length; i++)
        {
            watch_not_used += GameData.Instance.watch[i];
        }

        if ((float)watch_not_used / 18 < 0.2f)
        {
            GameDefine.canFireValue += 30;
        }
        else if ((float)watch_not_used / 18 > 0.5f)
        {
            if (GameDefine.canFireValue > 150)
            {
                GameDefine.canFireValue -= 50;
            }
            else if (GameDefine.canFireValue <= 150 && GameDefine.canFireValue >100)
            {
                GameDefine.canFireValue -= 10;

            }
        }
        else { }
    }

    public string FireUnit(string target)
    {
        float[] disArr = new float[6];
        int[] index = new int[6];

        for (int i = 0; i < disArr.Length; i++)
        {
            //Debug.Log(GameObject.Find("km_" + (i + 1).ToString()).name);
            try
            {
                Vector3 time_2s = new Vector3();
                time_2s.x = 200 * 3 * GameObject.Find(target).transform.forward.x;
                time_2s.y = 200 * 3 * GameObject.Find(target).transform.forward.y;
                time_2s.z = 200 * 3 * GameObject.Find(target).transform.forward.z;
                //Debug.Log(time_2s);
                disArr[i] = (GameManger.Instance.DistanceBetweenTwoVector3(GameObject.Find(target).transform.position + time_2s, GameObject.Find("km_" + (i + 1).ToString()).transform.position)) / PlayerPrefs.GetInt("km_" + (i + 1).ToString() + "_fireAssets");
            }
            catch
            {
                disArr[i] = float.MaxValue;
            }
            index[i] = i + 1;
        }

        for (int i = 0; i < disArr.Length; i++)
        {
            for (int j = disArr.Length - 1; j > i; j--)
            {
                if (disArr[i] > disArr[j])
                {
                    float temp = disArr[i];
                    disArr[i] = disArr[j];
                    disArr[j] = temp;

                    int temp2 = index[i];
                    index[i] = index[j];
                    index[j] = temp2;
                }
            }
        }
        string ziyuan = "";
        for (int i = 0; i < disArr.Length; i++)
        {
            ziyuan += disArr[i].ToString() + "_";
        }
        //Debug.Log(ziyuan);

        for (int i = 0; i < disArr.Length; i++)
        {
            if (GameObject.Find("km_" + index[i].ToString()).GetComponent<fire>().used == false && PlayerPrefs.GetInt("km_" + index[i].ToString() + "_fireAssets") > 0)
            {
                GameObject.Find("km_" + index[i].ToString()).GetComponent<fire>()._target = GameObject.Find(target);
                GameObject.Find("km_" + index[i].ToString()).GetComponent<fire>().used = true;
                PlayerPrefs.SetInt("km_" + index[i].ToString() + "_fireAssets", PlayerPrefs.GetInt("km_" + index[i].ToString() + "_fireAssets") - 1);
                return index[i].ToString();
            }
        }
        Debug.Log("[彩蛋出现]:武器均被占用！");
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskHeapControler : MonoBehaviour
{
    private int jiange = 60;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (taskHeap.Instance.heap[0] == null)
        {
            return;
        }
        if (jiange < 10)
        {
            taskHeap.Instance.UpdateQueue();
            jiange = 60;
        }
        else
        {
            jiange--;
        }

        if (taskHeap.Instance.heap[0].Tqueue > GameDefine.canFireValue)
        {
            string target = taskHeap.Instance.heap[0].Name;
            string fireUnit = "km_" + FireUnit(target);

            //制导资源
            if (taskHeap.Instance.heap[0].Tqueue < 200)
            {
                for (int i = 0; i < 6 * GameDefine.watchingAssets - 1; i++)
                {
                    int first = i;
                    if (!GameData.Instance.watchAssets[first].used)
                    {
                        //Debug.Log("我占用了第 " + first + " 号制导资源");
                        GameData.Instance.watchAssets[first].used = true;

                        GameObject watching = (GameObject)Instantiate(Resources.Load(GameDefine.WatchRayRay));
                        watching.GetComponent<watching>()._target = GameObject.Find(target);
                        watching.GetComponent<watching>()._from = GameObject.Find("km_" + ((first / GameDefine.watchingAssets) + 1).ToString());
                        watching.GetComponent<watching>().used = true;
                        watching.GetComponent<watching>().ones = false;
                        watching.GetComponent<watching>().index = first;
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
                        watching.GetComponent<watching>()._target = GameObject.Find(target);
                        watching.GetComponent<watching>()._from = GameObject.Find("km_" + ((second / GameDefine.watchingAssets) + 1).ToString());
                        watching.GetComponent<watching>().used = true;
                        watching.GetComponent<watching>().ones = false;
                        watching.GetComponent<watching>().index = second;
                        break;

                    }
                }

            }

            MySqlT.Instance.AddWeaponTask(PlayerPrefs.GetInt("TaskID"), target, fireUnit, -1, PlayerPrefs.GetInt("CurrTime") + 1);
            PlayerPrefs.SetInt("TaskID", PlayerPrefs.GetInt("TaskID") + 1);
            GameDefine.CanGetTask = true;
            taskHeap.Instance.Delete(0);
        }
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
                time_2s.x = 200 * 2 * GameObject.Find(target).transform.forward.x;
                time_2s.y = 200 * 2 * GameObject.Find(target).transform.forward.y;
                time_2s.z = 200 * 2 * GameObject.Find(target).transform.forward.z;
                //Debug.Log(time_2s);
                disArr[i] = (GameManger.Instance.DistanceBetweenTwoVector3(GameObject.Find(target).transform.position+time_2s, GameObject.Find("km_" + (i + 1).ToString()).transform.position)) / PlayerPrefs.GetInt("km_" + (i + 1).ToString() + "_fireAssets");
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
        Debug.Log("[彩蛋出现]，武器均被占用！");
        return null;
    }
}

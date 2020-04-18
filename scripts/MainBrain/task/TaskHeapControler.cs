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

            for (int i=0;i<6*GameDefine.watchingAssets-1;i++) {
                int first = i;
                if (!GameData.Instance.watchAssets[first].used)
                {
                    GameData.Instance.watchAssets[first]._target = GameObject.Find(target);
                    GameData.Instance.watchAssets[first].used = true;
                    GameData.Instance.watchAssets[first].index = first;
                }
                int second = i + 1;
                if (first != second && GameData.Instance.watchAssets[second].used)
                {
                    GameData.Instance.watchAssets[second]._target = GameObject.Find(target);
                    GameData.Instance.watchAssets[second].used = true;
                    GameData.Instance.watchAssets[second].index = second;
                    break;
                }
                else
                {
                    if (second == 17)
                    {
                        second = -1;
                    }
                    second++;
                }
            }
            /*int first;
            while (true)
            {
                first = Random.Range(0, 17);
                if (!GameData.Instance.watchAssets[first].used)
                {
                    GameData.Instance.watchAssets[first]._target = GameObject.Find(target);
                    GameData.Instance.watchAssets[first].used = true;
                    GameData.Instance.watchAssets[first].index = first;
                    break;
                }
                else {
                    continue;
                }
            }

            int second;
            while (true)
            {
                second = Random.Range(0, 17);
                if (first != second && !GameData.Instance.watchAssets[second].used)
                {
                    GameData.Instance.watchAssets[second]._target = GameObject.Find(target);
                    GameData.Instance.watchAssets[second].used = true;
                    GameData.Instance.watchAssets[second].index = second;
                    break;
                }
                else
                {
                    continue;
                }
            }*/
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
            Debug.Log(GameObject.Find("km_" + (i + 1).ToString()).name);
            try
            {
                disArr[i] = GameManger.Instance.DistanceBetweenTwoGameObject(GameObject.Find(target).transform, GameObject.Find("km_" + (i + 1).ToString()).transform);
            }
            catch {
                disArr[i] = float.MaxValue;
            }
            index[i] = i + 1;
        }

        for (int i = 0; i < disArr.Length; i++)
        {
            for (int j = i; j > 0; j--)
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

        for (int i = 0; i < disArr.Length; i++)
        {
            if (GameObject.Find("km_"+index[i].ToString()).GetComponent<fire>().used == false)
            {
                GameObject.Find("km_" + index[i].ToString()).GetComponent<fire>()._target = GameObject.Find(target);
                GameObject.Find("km_" + index[i].ToString()).GetComponent<fire>().used =true;
                return index[i].ToString();
            }
        }
        Debug.Log("[彩蛋出现]，武器均被占用！");
        return null;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class taskHeap
{
    //单例可存储数据文件
    private static taskHeap _Instance;
    public static taskHeap Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new taskHeap();
            }
            return _Instance;
        }
    }

    //public TaskNode top;
    //public TaskNode end;
    public TaskNode[] heap;
    public int TaskCount = 0;
    private int HeapSize = 20;

    public taskHeap()
    {
        heap = new TaskNode[HeapSize];
    }

    public void Update_Queue()
    {
        for (int i = 0; i < TaskCount; i++)
        {
            if (heap[i].Tqueue < GameDefine.canFireValue)
            {
                //释放制导资源
                // GameData.Instance.watchAssets[heap[i].watch_1].used = false;
                // GameData.Instance.watch[heap[i].watch_1]++;
                // GameData.Instance.FreeWatchAssets(heap[i].watch_1);
                // GameData.Instance.FreeWatchAssets(heap[i].watch_2);
                int[] end = GameData.Instance.target_watch[heap[i].Name];

                GameObject w1 = GameObject.Find(heap[i].Name).transform.Find("watch_" + end[0]).gameObject;
                GameObject w2 = GameObject.Find(heap[i].Name).transform.Find("watch_" + end[1]).gameObject;
                w1.GetComponent<watching>().FreeWatching();
                w2.GetComponent<watching>().FreeWatching();
                Debug.Log("资源被抢占");
                GameData.Instance.target_watch.Remove(heap[i].Name);
                Delete(i);
            }
        }
    }

    public void Delete(int index) {
        Swap(index,TaskCount-1);
        heap[TaskCount - 1] = null;
        TaskCount--;
        DownJust(index);
    }

    public void Show()
    {
        string end = string.Empty;
        if (heap == null)
        {
            return;
        }
        for (int i = 0; i < TaskCount; i++)
        {
            end+=heap[i].Tqueue+" ";
        }
        GameObject.Find("Heap").GetComponent<nodeControl>().jiedian = end;
        Debug.Log(end);
    }

    public void Insert(TaskNode node)
    {
        try
        {
            heap[TaskCount] = node;
            UpJust(TaskCount);
            TaskCount++;

      }
        catch
        {
           UnityEngine.Debug.Log("yuejie");
        }
    }

    public void UpJust(int index)
    {
        if (heap[index].Tqueue > heap[(index - 1) / 2].Tqueue)
        {
            Swap(index, (index - 1) / 2);
        }
        else
        {
            return;
        }
        UpJust((index - 1) / 2);
    }

    public void DownJust(int index)
    {
        int index_left = index * 2 + 1;
        int index_right = index * 2 + 2;
        if (index_left>=TaskCount||index_right>=TaskCount) {
            return;
        }
        if (heap[index_left].Tqueue > heap[index_right].Tqueue)
        {
            Swap(index, index_left);
            DownJust(index_left);
        }
        else {
            Swap(index, index_right);
            DownJust(index_right);
        }
    }


    public void Swap(int index1, int index2)
    {
        TaskNode temp = new TaskNode();
        temp = heap[index1];
        heap[index1] = heap[index2];
        heap[index2] = temp;
    }
}

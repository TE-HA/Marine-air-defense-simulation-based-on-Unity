using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class taskHeap
{
    //单例可存储数据文件
    private static taskHeap _Instance;
    private static readonly object taskheap_lock = new object();
    //private bool delLock = false;

    public static taskHeap Instance
    {
        get
        {
            //利用双锁模式避免高并发下非单例模式
            if (_Instance == null)
            {
                lock (taskheap_lock)
                {
                    if (_Instance == null)
                    {
                        _Instance = new taskHeap();
                    }
                }
            }
            return _Instance;
        }
    }

    //public TaskNode top;
    //public TaskNode end;
    public TaskNode[] heap;
    public int TaskCount = 0;
    private int HeapSize = 100;

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
                GameData.Instance.isAdded[GameObject.Find(heap[i].Name)] = false;
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
        //Debug.Log(end);
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

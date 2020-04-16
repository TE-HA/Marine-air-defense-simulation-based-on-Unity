using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TaskNode
{
    public int TaskID;
    public float Tqueue = -1;
    public TaskNode leftchild;
    public TaskNode rightchild;

    public TaskNode(int id,float queue)
    {
        this.TaskID = id;
        this.Tqueue = queue;
    }

    public void display() {
        Debug.Log(TaskID);
    }
}

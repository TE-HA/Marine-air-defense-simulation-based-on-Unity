using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TaskNode : MonoBehaviour
{
    public int TaskID;
    public float Tqueue = -1;
    public TaskNode leftchild;
    public TaskNode rightchild;

    public int jiange = 60;

    private void FixedUpdate()
    {
        if (jiange < 0)
        {
            Tqueue++;
            jiange = 60;
        }
        else {
            jiange--;
        }
    }

    public TaskNode() {

    }
    public TaskNode(int id,float queue)
    {
        this.TaskID = id;
        this.Tqueue = queue;
    }

    public void display() {
        Debug.Log(TaskID);
    }
}

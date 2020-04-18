using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TaskNode
{
    public string Name;
    public float Tqueue = -1;
    public int jiange = 60;

   
    public TaskNode() {

    }
    public TaskNode(string name,float queue)
    {
        this.Name = name;
        this.Tqueue = queue;
    }

    public void display() {
        Debug.Log(Name+" "+Tqueue);
    }
}

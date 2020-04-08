using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TaskNode{
    public string TTarget;
    public string TFrom;
    public float TQueue;//优先级别
    public int TTime;
    public int TToward;

    public TaskNode(string target,string from,float queue,int time,int toward) {
        this.TTarget = target;
        this.TFrom = from;
        this.TQueue = queue;
        this.TTime = time;
        this.TToward = toward;
    }


}

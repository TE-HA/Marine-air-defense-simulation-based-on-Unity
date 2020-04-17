using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class taskHeap : MonoBehaviour

{
    public TaskNode top;
    public TaskNode end;
    public int taskCount = 0;

    public void show(TaskNode node)
    {
        if (node == null)
        {
            return;
        }
        show(node.leftchild);
        node.display();
        show(node.rightchild);
    }


    public void Swap(TaskNode node1, TaskNode node2)
    {
        TaskNode temp = new TaskNode();
        temp = node1;
        node1 = node2;
        node2 = temp;
    }

    //完全二叉树
    public void Insert(TaskNode node)
    {

    }

    private void FixedUpdate()
    {
        if (top.Tqueue > GameDefine.canFireValue)
        {
            //弹出堆顶，调整堆

        /*AddWeaponTask(PlayerPrefs.GetInt("TaskID"), GameData.enemyDaodan[i].name, "km_1", -1, PlayerPrefs.GetInt("CurrTime")+2);
                PlayerPrefs.SetInt("TaskID", PlayerPrefs.GetInt("TaskID") + 1);
*/
        }
    }
}

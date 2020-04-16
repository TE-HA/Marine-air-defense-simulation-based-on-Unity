using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class taskHeap
{
    public TaskNode top;
    public TaskNode end;
    public int taskCount = 0;

    public void show(TaskNode node) {
        if (node==null) {
            return;
        }
        show(node.leftchild);
        node.display();
        show(node.rightchild);
    }

    //完全二叉树
    public void insert(TaskNode node)
    {
        if (top == null)
        {
            top = node;
            end = node;
            taskCount++;
            return;
        }

        TaskNode curr = top;

        while (true)
        {
            if (curr.leftchild == null)
            {
                curr.leftchild = node;
                taskCount++;
                return;
            }
            else if (curr.rightchild == null)
            {
                curr.rightchild = node;
                taskCount++;
                return;
            }
            else
            {
                curr = curr.leftchild;
            }
        }
    }
}

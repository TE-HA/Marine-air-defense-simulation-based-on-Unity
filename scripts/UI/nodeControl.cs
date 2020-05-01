using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nodeControl : MonoBehaviour
{
    TaskNode[] now;
    private GameObject node1;
    private GameObject node2;
    private GameObject node3;
    private GameObject node4;
    private GameObject node5;
    private GameObject node6;
    private GameObject node7;
    private GameObject[] node_list = new GameObject[7];
    public string jiedian;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            node_list[i] = gameObject.transform.Find("node" + (i + 1).ToString()).gameObject;
            node_list[i].SetActive(false);
        }
    }

    void ManageActive(int index)
    {
        for (int i = 0; i < index; i++)
        {
            node_list[i].SetActive(true);
        }

        for (int i = index; i < 7; i++)
        {
            node_list[i].SetActive(false);
        }
    }

    string[] jiedian_value() {
        return jiedian.Split();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        string[] kk = jiedian_value();
        ManageActive(kk.Length-1);

        for (int i = 0; i < kk.Length; i++)
        {
            node_list[i].transform.Find("value").GetComponent<Text>().text = kk[i];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetest : MonoBehaviour
{
    private int flag = 1;
    private int count = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("CurrTime") / 60 == count)
        {
            MySqlT.Instance.AddMoveTask(PlayerPrefs.GetInt("TaskID"), "km_main", 200 * flag, 200 );
            PlayerPrefs.SetInt("TaskID", PlayerPrefs.GetInt("TaskID") + 1);
            MySqlT.Instance.AddMoveTask(PlayerPrefs.GetInt("TaskID"), "km_1", 200 * flag, 200 );
            PlayerPrefs.SetInt("TaskID", PlayerPrefs.GetInt("TaskID") + 1);
            MySqlT.Instance.AddMoveTask(PlayerPrefs.GetInt("TaskID"), "km_2", 200 * flag, 200 );
            PlayerPrefs.SetInt("TaskID", PlayerPrefs.GetInt("TaskID") + 1);
            MySqlT.Instance.AddMoveTask(PlayerPrefs.GetInt("TaskID"), "km_3", 200 * flag, 200 );
            PlayerPrefs.SetInt("TaskID", PlayerPrefs.GetInt("TaskID") + 1);
            MySqlT.Instance.AddMoveTask(PlayerPrefs.GetInt("TaskID"), "km_4", 200 * flag, 200 );
            PlayerPrefs.SetInt("TaskID", PlayerPrefs.GetInt("TaskID") + 1);
            MySqlT.Instance.AddMoveTask(PlayerPrefs.GetInt("TaskID"), "km_5", 200 * flag, 200 );
            PlayerPrefs.SetInt("TaskID", PlayerPrefs.GetInt("TaskID") + 1);
            MySqlT.Instance.AddMoveTask(PlayerPrefs.GetInt("TaskID"), "km_6", 200 * flag, 200 );
            PlayerPrefs.SetInt("TaskID", PlayerPrefs.GetInt("TaskID") + 1);

            GameDefine.CanGetTask = true;
            flag = -flag;
            count++;
        }
    }
}

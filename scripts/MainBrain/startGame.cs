using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startGame : MonoBehaviour {
    #region 好像没啥用的脚本
    private Button startButton;
    // Use this for initialization
    void Start()
    {
        startButton = transform.Find("StartGame").GetComponent<Button>();
        startButton.onClick.AddListener(StartClick);
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    private void StartClick()
    {
        //Debug.Log("c");
        GameManger.Instance.LoadSence("currentGame");
    }
    #endregion

}

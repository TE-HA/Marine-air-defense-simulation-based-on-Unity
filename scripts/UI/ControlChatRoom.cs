﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlChatRoom : MonoBehaviour
{
    private int count = 0;
    private Text chatText;
    private ScrollRect scrollRect;

    void Start()
    {
        chatText = GameObject.Find("ChatText").GetComponent<Text>();
        scrollRect = GameObject.Find("TextShowPanel").GetComponent<ScrollRect>();
    }

    void Clear()
    {
        chatText.text = string.Empty;
    }
    void FixedUpdate()
    {
        if (GameData.canShow)
        {
            string addText = string.Empty;
            switch (GameData.messageType)
            {
                case 1:
                    addText = "\n\n " + "<color=white>" + "*第 " + PlayerPrefs.GetInt("CurrTime").ToString() + " 秒:" + "</color>:" + "<color=red>" + "[敌机攻击]" + "</color>: " + GameData.message;
                    chatText.text += addText;
                    break;
                case 2:
                    addText = "\n\n " + "<color=white>" + "*第 " + PlayerPrefs.GetInt("CurrTime").ToString() + " 秒:" + "</color>:" + "<color=blue>" + "[战舰反击]" + "</color>: " + GameData.message;
                    chatText.text += addText;
                    break;
                case 3:
                    addText = "\n\n " + "<color=white>" + "*第 " + PlayerPrefs.GetInt("CurrTime").ToString() + " 秒:" + "</color>:" + "<color=blue>" + "[战舰移动]" + "</color>: " + GameData.message;
                    chatText.text += addText;
                    break;
                case 4:
                    addText = "\n\n " + "<color=white>" + "*第 " + PlayerPrefs.GetInt("CurrTime").ToString() + " 秒:" + "</color>:" + "<color=green>" + "[拦截导弹成功]" + "</color>: " + GameData.message;
                    chatText.text += addText;
                    break;
                case 5:
                    addText = "\n\n " + "<color=white>" + "*第 " + PlayerPrefs.GetInt("CurrTime").ToString() + " 秒:" + "</color>:" + "<color=black>" + "[拦截失败]" + "</color>: " + GameData.message;
                    chatText.text += addText;
                    break;
                case 6:
                    addText = "\n\n " + "<color=white>" + "*第 " + PlayerPrefs.GetInt("CurrTime").ToString() + " 秒:" + "</color>:" + "<color=green>" + "[拦截飞机成功]" + "</color>: " + GameData.message;
                    chatText.text += addText;
                    break;

            }
            GameData.canShow = false;
            #region 关键代码
            if (count > 50)
            {
                Clear();
                count = 0;
            }
            else
            {
                count++;
            }
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
            Canvas.ForceUpdateCanvases();
            #endregion
        }
    }
}

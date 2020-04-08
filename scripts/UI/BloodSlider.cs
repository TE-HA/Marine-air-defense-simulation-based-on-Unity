using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BloodSlider : MonoBehaviour {
    #region 血条(需要初始化)
    private Text objName;
    private Slider mSlider;
	// Use this for initialization
	void Start () {

        #region 获取存档数据
        objName = gameObject.transform.Find("name").GetComponent<Text>();
        objName.text =PlayerPrefs.GetString(gameObject.name+"_name");

        mSlider = transform.GetComponent<Slider>();
        mSlider.value = PlayerPrefs.GetInt(gameObject.name+"_slider");
        mSlider.maxValue = PlayerPrefs.GetInt(gameObject.name+"_slider");
        #endregion

    }
    float curr;
	// Update is called once per frame
	void FixedUpdate () {
        curr = PlayerPrefs.GetInt(mSlider.name+"_slider");
        mSlider.value = curr;
	}
    #endregion
}

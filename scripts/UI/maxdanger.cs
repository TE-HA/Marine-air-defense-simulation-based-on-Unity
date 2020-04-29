using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class maxdanger : MonoBehaviour {

	// Update is called once per frame
	void FixedUpdate () {
        gameObject.GetComponent<Text>().text = GameDefine.canFireValue.ToString();
	}
}

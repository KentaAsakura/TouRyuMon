/*
攻撃ボタン
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atackButton : MonoBehaviour {

	mainCanvas mainStatus;
	
	// Use this for initialization
	void Start () {
		GameObject canvas = GameObject.FindWithTag("canvas");
		mainStatus = canvas.GetComponent<mainCanvas>();
	}
	
	// Update is called once per frame
	void Update () {
		if(mainStatus.pcFlag == true){
			this.gameObject.SetActive(false);
		}
		
	}
}

/*
ルール説明の外部サイトリンクボタン
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ruleButton : MonoBehaviour {
	
	string url = "https://asakurasandesuyo.hatenablog.com/entry/2018/10/27/142803";
	main mainStatus;
	// Use this for initialization
	void Start () {
		GameObject canvas = GameObject.FindWithTag("canvas");
		mainStatus = canvas.GetComponent<main>();
	}
	
	// Update is called once per frame
	//web公開時にボタンを非表示にする
	void Update () {
		if(mainStatus.pcFlag == true){
			this.gameObject.SetActive(false);
		}
	}
	
	public void OnClick(){
		Application.OpenURL(url);
	}
}
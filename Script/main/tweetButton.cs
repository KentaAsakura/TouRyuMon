/*
ゲームオーバー時にスコアをツイートするボタン
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tweetButton : MonoBehaviour {
	
	mainCanvas mainStatus;
	
	// Use this for initialization
	void Start () {
		GameObject canvas = GameObject.FindWithTag("canvas");
		mainStatus = canvas.GetComponent<mainCanvas>();
		if(mainStatus.pcFlag == true){
			this.gameObject.SetActive(false);
		}		
	}

	public void OnClick(){
		//Application.OpenURL("https://www.google.co.jp/");
		Application.OpenURL("https://twitter.com/intent/tweet?text=" + WWW.EscapeURL("投龍門で" + mainCamera.score.ToString()) + "点を獲得しました。一緒に遊ぼう！" + "&url=" + WWW.EscapeURL("https://asakurasandesuyo.hatenablog.com/entry/2018/10/27/142803"));
	}
	
}

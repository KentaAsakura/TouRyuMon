/*
ルール説明の外部サイトリンクボタン
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Privacy : MonoBehaviour {
	
	string url = "https://asakurasandesuyo.hatenablog.com/archive/2020/09/03";
	main mainStatus;
	// Use this for initialization
	void Start () {
	}
		
	public void OnClick(){
		Application.OpenURL(url);
	}
}
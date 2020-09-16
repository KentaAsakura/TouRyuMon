/*
ハイスコアリセットの確認ボタン
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resetButton : MonoBehaviour {
	
	public GameObject startButton;
	public GameObject ruleButton;
	public GameObject titleImage;
	public GameObject yesButton;
	public GameObject noButton;
	public GameObject alertText;
		
	// Use this for initialization
	void Start () {
		
	}
	
	//各種ボタンの表示・非表示切り替え
	public void OnClick () {
		startButton.SetActive(false);
		ruleButton.SetActive(false);
		titleImage.SetActive(false);
		yesButton.SetActive(true);
		noButton.SetActive(true);
		alertText.SetActive(true);
		this.gameObject.SetActive(false);
	}
}
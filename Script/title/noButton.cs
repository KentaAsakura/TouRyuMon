/*
ハイスコアリセット画面のキャンセルボタン
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noButton : MonoBehaviour {
	
	public GameObject startButton;
	public GameObject ruleButton;
	public GameObject titleImage;
	public GameObject resetButton;
	public GameObject yesButton;
	public GameObject alertText;
	
	public void OnClick () {
		//各種ボタンの表示・非表示切り替え
		startButton.SetActive(true);
		ruleButton.SetActive(true);
		resetButton.SetActive(true);
		titleImage.SetActive(true);
		yesButton.SetActive(false);
		alertText.SetActive(false);
		this.gameObject.SetActive(false);
	}
}
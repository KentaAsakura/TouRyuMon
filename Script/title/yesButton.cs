/*
ハイスコアリセット実行ボタン
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class yesButton : MonoBehaviour {
	
	private string highScoreKey = "highScore";
	
	public void OnClick () {
		PlayerPrefs.SetInt(highScoreKey,0);
		//ボタンやスコアを再表示させるためにシーンをリセットさせる
		SceneManager.LoadScene("Title");
	}
}

/*
スコア表示
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main : MonoBehaviour {
	
	private string highScoreKey = "highScore";
	public GameObject scoreBoard;
	
	public bool pcFlag = true;
	
	// Use this for initialization
	void Start () {
		scoreBoard.GetComponent<Text>().text = "HighScore:" + PlayerPrefs.GetInt(highScoreKey,0).ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

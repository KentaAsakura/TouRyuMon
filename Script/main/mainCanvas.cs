/*
メインキャンバス(主にスコアの処理)
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainCanvas : MonoBehaviour {
	public GameObject scoreBoard;
	Text scoreText;
	Vector2 scoreBordPos;
	Vector2 scoreBordSize;
	float originScoreBordSizeX;
	float originScoreBordSizeY;
	private int highScore;
	private string highScoreKey = "highScore";
	//ゲームオーバー時の処理は一度だけ行う
	bool gameOverCheck = false;
	bool highScoreCheck = false;
	public GameObject player;
	player playerStatus;
	public GameObject atackButton;
	public GameObject jumpButton;
	public GameObject titleButton;
	public GameObject tweetButton;
	public GameObject readyBoard;
	bool isReady;
	int readyTime;
	public bool pcFlag = false;
	
	// Use this for initialization
	void Start () {
		scoreText = scoreBoard.GetComponent<Text>();
		scoreBordPos = scoreBoard.GetComponent<RectTransform>().anchoredPosition;
		scoreBordSize = scoreBoard.GetComponent<RectTransform>().sizeDelta;
		originScoreBordSizeX = scoreBordSize.x;
		originScoreBordSizeY = scoreBordSize.y;
		highScore = PlayerPrefs.GetInt(highScoreKey,0);
		playerStatus = player.GetComponent<player>();
		readyTime = 60;
		isReady = true;
	}
	
	// Update is called once per frame
	void Update () {
		//ゲーム開始直後のReady Go!表示
		if(isReady == true){
			readyTime = readyTime-1;
			if(readyTime<0){
				isReady = false;
				readyBoard.GetComponent<Text>().text = "GO!!";
				Destroy(readyBoard,0.5f);
				
				playerStatus.isReady = false;
			}
			if(readyTime%10 == 0){
				if(readyBoard.activeSelf == true){
					readyBoard.SetActive(false);
				}else{
					readyBoard.SetActive(true);
				}
			}
		}
		//プレイ中のスコア表示
		if(playerStatus.gameOver == false){
			scoreText.text = "HighScore:"+highScore+"\nScore:"+mainCamera.score+"\nGameLevel:"+mainCamera.gameLevel;
		}else{
			//ゲームオーバー時のスコア表示
			if(highScoreCheck == true){
				scoreText.text = "GameOver\nNew Record!!\nHighScore:"+highScore;
			}else{
				scoreText.text = "GameOver\nScore:"+mainCamera.score+"\nHighScore:"+highScore;
			}
		}
		//ゲームオーバー処理
		if(playerStatus.gameOver == true && gameOverCheck == false){
			gameOverCheck = true;
			//ハイスコア更新判定
			if(highScore < mainCamera.score){
				highScore = mainCamera.score;
				PlayerPrefs.SetInt(highScoreKey,highScore);
				highScoreCheck = true;
			}
			//スコア表示部を中央に移動
			scoreBoard.GetComponent<RectTransform>().anchoredPosition =new Vector3(0f,0f,0f);
			scoreBoard.GetComponent<RectTransform>().anchorMax =new Vector2(0.5f,0.5f);
			scoreBoard.GetComponent<RectTransform>().anchorMin =new Vector2(0.5f,0.5f);
			scoreText.fontSize = 36;
			scoreBordSize.x = 1.5f*originScoreBordSizeX ;
			scoreBordSize.y = 2*originScoreBordSizeY;
			scoreBoard.GetComponent<RectTransform>().sizeDelta = scoreBordSize;
			scoreText.text = "GameOver\nScore:"+mainCamera.score+"\nhighScore:"+highScore;
			scoreText.alignment = TextAnchor.UpperCenter;;
			scoreText.color = new Color(255f/255f,0,0);
			//atackButton.SetActive(false);
			//jumpButton.SetActive(false);
			Destroy(atackButton);
			Destroy(jumpButton);
			titleButton.SetActive(true);
			tweetButton.SetActive(true);
		}
	}
}
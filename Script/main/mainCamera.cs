/*
メインカメラ
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainCamera : MonoBehaviour {
	static public float scrollSpeed;
	static public float gameLevel;
	public GameObject ground;
	float groundWidth;
	int groundCount = 0;
	float lastInstantX = 0;
	int lastInstantY = 0;
	public GameObject slime;
	public GameObject bat;
	Vector3 instantPos;
	Vector3 instantGroundPos;
	Vector3 instantEnemyPos;
	Rigidbody2D rb;
	static public int score = 0;
	float resetTime = 2500f;
	float speedupTimer;
	GameObject player;
	player playerStatus;
	AudioSource[] audioSources;
	AudioSource bgm;
	AudioSource seNg;
	
// Use this for initialization
	void Start () {
		score = 0;
		scrollSpeed = 4f;
		gameLevel = 0;
		speedupTimer = resetTime;
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = new Vector2(scrollSpeed,0);
		player = GameObject.FindWithTag("player");
		playerStatus = player.GetComponent<player>();
		lastInstantX = this.transform.position.x;
		audioSources = GetComponents<AudioSource>();
		bgm = audioSources[0];
		seNg = audioSources[1];
		groundWidth = ground.GetComponent<RectTransform>().sizeDelta.x;
	}
	
	// Update is called once per frame
	void Update () {
		//プレイヤー消滅後にnull参照が発生しないように、カメラをplayerタグに変更する
		if(playerStatus.gameOver == true && this.gameObject.tag != "player"){
			this.gameObject.tag = "player";
			bgm.Stop();
			seNg.Play(0);
		}
		//徐々にスクロールの速さを上げる
		speedupTimer = speedupTimer-1;
		if(speedupTimer <= -1 && playerStatus.gameOver == false){
			scrollSpeed = scrollSpeed+1f;
			gameLevel += 1f;
			speedupTimer = resetTime;
			rb.velocity = new Vector2(scrollSpeed,0);
		}
		//スクロールでスコア増加
		if(playerStatus.gameOver == false && ((int)speedupTimer)%10 == 0){
			score = score+1;
		}
		if(playerStatus.gameOver == true){
		}
		//地形の生成
		groundCount++;
		if(this.transform.position.x - lastInstantX >= groundWidth+2f){
			instantPos.x = this.transform.position.x;
			instantPos.y = lastInstantY+Random.Range(-3f,0.5f);
			if(instantPos.y > 2){
				instantPos.y=2;
			}
			if(instantPos.y < -1){
				instantPos.y=-1;
			}
			instantPos.z = 0;
			instantGroundPos = instantPos;
			instantEnemyPos = instantPos;
			instantGroundPos.x = instantGroundPos.x+12f;
			instantGroundPos.z = 2;
			Instantiate(ground,instantGroundPos,this.transform.rotation);
			instantEnemyPos = instantGroundPos;
			instantEnemyPos.x = instantEnemyPos.x + Random.Range(-groundWidth/6,groundWidth/4);
			if(Random.Range(0f,1f)>0.6f){
				instantEnemyPos.y = instantEnemyPos.y + Random.Range(2f,4f);
				Instantiate(bat,instantEnemyPos,this.transform.rotation);
			}else{
				instantEnemyPos.y = instantEnemyPos.y + 3f;
				Instantiate(slime,instantEnemyPos,this.transform.rotation);
			}
			groundCount = 0;
			lastInstantX = instantPos.x;
			lastInstantY = (int)instantPos.y;
		}
	}
}

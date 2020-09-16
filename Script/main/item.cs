/*
アイテムの行動
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour {
	GameObject player;
	player playerStatus;
	public GameObject number1600;
	private AudioSource audioSource;
	SpriteRenderer sprite;
	Collider2D thisCollider;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		sprite = GetComponent<SpriteRenderer>();
		thisCollider = GetComponent<CircleCollider2D>();
		player = GameObject.Find("player");
		if(player != null){
			playerStatus = player.GetComponent<player>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		//取得処理
		if(other.gameObject.tag == "player"){
			audioSource.Play(0);
			Destroy(sprite);
			Destroy(thisCollider);
			if(this.gameObject.tag == "powerItem"){
				if(playerStatus.shotLevel != 3){
					playerStatus.shotLevel = 3;
				}else{
					//既に取得済みの場合はスコア加算
					mainCamera.score = mainCamera.score+1600;
					Instantiate(number1600,this.transform.position,this.transform.rotation);
				}
			}else if(this.gameObject.tag == "speedItem"){
				playerStatus.speedLevel =+1f;
			}
			Destroy(gameObject,0.5f);
		//地形に当たったら消滅
		}else if(other.gameObject.tag == "thorns"){
			Destroy(gameObject);
		}
	}
}
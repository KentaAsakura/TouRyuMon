//弾の挙動
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour {
	Rigidbody2D rb;
	float shotSpeed = 2*mainCamera.scrollSpeed;
	int penLife = 0;
	GameObject player;
	player playerStatus;
	private GameObject bgm;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		player = GameObject.Find("player");
		playerStatus = player.GetComponent<player>();
		shotSpeed = 2*playerStatus.speed;
		//弾の射出角度の指定
		if(playerStatus.shotArrow == 0){
			rb.velocity = new Vector2(shotSpeed,0);
			playerStatus.shotArrow = playerStatus.shotArrow+1;
		}else if(playerStatus.shotArrow == 1){
			rb.velocity = new Vector2(shotSpeed*Mathf.Cos(15*Mathf.Deg2Rad),shotSpeed*Mathf.Sin(15*Mathf.Deg2Rad));
			playerStatus.shotArrow = playerStatus.shotArrow+1;
		}else if(playerStatus.shotArrow == 2){
			rb.velocity = new Vector2(shotSpeed*Mathf.Cos(345*Mathf.Deg2Rad),shotSpeed*Mathf.Sin(345*Mathf.Deg2Rad));
			playerStatus.shotArrow = 0;
		}else{
			Destroy(gameObject);
			playerStatus.shotArrow = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//一定時間経過で消滅させる
		penLife = penLife+1;
		if(penLife > 90){
			Destroy(gameObject);
		}
	}
	//衝突時に自分を消す
	void OnTriggerEnter2D (Collider2D other) {
		if(other.gameObject.tag != "crash" && 
			other.gameObject.tag != "shot1" && 
			other.gameObject.tag != "player" && 
			other.gameObject.tag != "speedItem" && 
			other.gameObject.tag != "powerItem"){
			//衝突時に敵を消す
			if(other.gameObject.tag == "enemy"){
				other.gameObject.tag = "crash";
			}
			Destroy(gameObject);
		}
	}
}
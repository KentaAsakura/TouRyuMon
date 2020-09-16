/*
敵の行動
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAction : MonoBehaviour {
	Vector3 enemyPos;
	private Rigidbody2D rb;
	float scrollSpeed;
	public float isThrowAngle = 120.0f;
	//持たれることでtrueになり破壊される状態になる
	bool onGround = false;
	bool stay = true;
	int stayTime = 0;
	enemy enemyStatus;
	//ぶつかった敵のステータス
	enemy otherEnemyStatus;
	string itsName;
	GameObject player;
	player playerStatus;
	public GameObject powerItem;
	public GameObject speedItem;
	public GameObject number0050;
	public GameObject number0100;
	public GameObject number0200;
	public GameObject number0400;
	public GameObject number0800;
	public GameObject number1600;
	public GameObject number3200;
	private AudioSource audioSource;
	//アニメーション遷移フラグ
	private const string key_isHeld = "isHeld";
	private const string key_isDestroyed = "isDestroyed";
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("player");
		playerStatus = player.GetComponent<player>();
		rb = GetComponent<Rigidbody2D> ();
		enemyStatus = GetComponent<enemy>();
		audioSource = GetComponent<AudioSource>();
		
		scrollSpeed = mainCamera.scrollSpeed;
		itsName = enemyStatus.enemyName;
		playerStatus = player.GetComponent<player>();
	}
	
	// Update is called once per frame
	void Update () {
		stayTime = stayTime+1;
		if(stayTime == 30){
			stay = false;
		}
		if(enemyStatus.isHeld == true && this.tag != "crash" && this.tag != "holdCrash"){
			enemy.isHeldPosition(this.transform.localScale.y);
			enemyPos = enemy.isHeldPosition(this.transform.localScale.y);
			rb.transform.position = enemyPos;
		}else if(enemyStatus.isThrow == true){
			//投げられた
			rb.velocity = enemy.isThrowVector(isThrowAngle,enemyStatus.isRebound,playerStatus.speed);
			isThrowAngle = isThrowAngle-1f;
			if(isThrowAngle <= 60.0f){
				isThrowAngle = 60.0f;
			}
		}else if(stay == false){
			if(itsName == "slime"){
				if(onGround == true){
					rb.velocity = new Vector2(-scrollSpeed*0.5f,0);
				}else{
					rb.velocity = new Vector2(-scrollSpeed*0.5f,-scrollSpeed*0.5f);
				}
			}else if(itsName == "bat"){
				rb.velocity = new Vector2(-scrollSpeed*0.2f,0);
			}
		}
		//落下で消滅
		if(this.transform.position.y < -5){
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		//撃たれた時に自分を消す
		if(other.gameObject.tag == "shot1"){
			enemyStatus.enemyAction.SetBool(key_isDestroyed, true);
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			GameObject.Destroy(this.GetComponent<BoxCollider2D>());
			GameObject.Destroy(this.GetComponent<BoxCollider2D>());
			Destroy(gameObject,0.5f);
			Instantiate(number0050,this.transform.position,this.transform.rotation);
			audioSource.Play(0);
			mainCamera.score = mainCamera.score+50;
		}
		//トゲにぶつかったときに自分を消す
		if(other.gameObject.tag == "thorns"){
			enemyStatus.isHeld = false;
			enemyStatus.isThrow = false;
			enemyStatus.enemyAction.SetBool(key_isDestroyed, true);
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			GameObject.Destroy(this.GetComponent<BoxCollider2D>());
			this.tag = "crash";
			Destroy(gameObject,0.5f);
			//playerが乗っているときに破壊された場合にplayerのステータスを変更する
			if(enemyStatus.isMounted == true){
				playerStatus.onGround = false;
				playerStatus.onEnemy = false;
				playerStatus.jumpUp = false;
			}
		}
		if(other.gameObject.tag == "ground"){
			onGround = true;
		}
	}	
	void OnTriggerExit2D (Collider2D other) {
		if(other.gameObject.tag == "ground"){
			onGround = false;
		}
	}
	
	void OnCollisionEnter2D (Collision2D other) {
		//持たれてる時に敵と衝突
		if(enemyStatus.isHeld == true && other.gameObject.tag != "player" 
										&& other.gameObject.tag != "ground" ){
			enemyStatus.enemyAction.SetBool(key_isDestroyed, true);
			GameObject.Destroy(this.GetComponent<BoxCollider2D>());
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			this.tag = "holdCrash";
			Destroy(gameObject,0.5f);
			audioSource.Play(0);
			mainCamera.score = mainCamera.score+100;
			Instantiate(number0100,this.transform.position,this.transform.rotation);
		}
		
		//投げられた敵との衝突(自身が反射状態のときは処理しない)
		if(other.gameObject.tag == "crashableEnemy" && enemyStatus.isRebound == false){
			enemyStatus.enemyAction.SetBool(key_isDestroyed, true);
			GameObject.Destroy(this.GetComponent<BoxCollider2D>());
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
				this.tag = "crash";
			Destroy(gameObject,0.5f);
			audioSource.Play(0);
			otherEnemyStatus = other.gameObject.GetComponent<enemy>();
			if(otherEnemyStatus.enemyClash < 5){
				otherEnemyStatus.enemyClash += 1 ;
			}
			//スコア加算処理
			mainCamera.score = mainCamera.score + 100 * (int)Mathf.Pow(2,otherEnemyStatus.enemyClash);
			//取得スコアの画面表示
			if(otherEnemyStatus.enemyClash == 1){
				Instantiate(number0200,this.transform.position,this.transform.rotation);
			}else if(otherEnemyStatus.enemyClash == 2){
				Instantiate(number0400,this.transform.position,this.transform.rotation);
			}else if(otherEnemyStatus.enemyClash == 3){
				Instantiate(number0800,this.transform.position,this.transform.rotation);
			}else if(otherEnemyStatus.enemyClash == 4){
				Instantiate(number1600,this.transform.position,this.transform.rotation);
			}else if(otherEnemyStatus.enemyClash == 5){
				Instantiate(number3200,this.transform.position,this.transform.rotation);
			}
			
			//playerが乗っているときに破壊された場合にplayerのステータスを変更する(空中で歩行してしまう現象の回避)
			if(enemyStatus.isMounted == true){
				playerStatus.onGround = false;
				playerStatus.onEnemy = false;
				playerStatus.jumpUp = false;
			}
			//消滅時に確率でアイテムを落とす
			if(Random.Range(1,100)>60){
				Instantiate(powerItem,this.transform.position,this.transform.rotation);
			}
		}
		
		//投げられ中の衝突
		if(enemyStatus.isThrow == true){
			//地形にぶつかって消滅
			if(other.gameObject.tag == "ground" ||
				other.gameObject.tag == "thorns"){
				enemyStatus.isHeld = false;
				enemyStatus.isThrow = false;
				enemyStatus.enemyAction.SetBool(key_isDestroyed, true);
				rb.constraints = RigidbodyConstraints2D.FreezeAll;
				GameObject.Destroy(this.GetComponent<BoxCollider2D>());
				this.tag = "crash";
				Destroy(gameObject,0.5f);
				audioSource.Play(0);
				mainCamera.score = mainCamera.score+100;
				Instantiate(number0100,this.transform.position,this.transform.rotation);
			//プレイヤーにぶつかってキャッチされる(プレイヤー側で処理)
			}else if(other.gameObject.tag == "player"){
			//弾とぶつかると消滅(弾側で処理)
			}else if(other.gameObject.tag == "shot1"){
				audioSource.Play(0);
				mainCamera.score = mainCamera.score+50;
				Instantiate(number0050,this.transform.position,this.transform.rotation);
			//敵とぶつかった場合の挙動
			}else{
				otherEnemyStatus = other.gameObject.GetComponent<enemy>();
				//投げられた、または持たれている敵との衝突で消滅
				if(otherEnemyStatus.isThrow == true ||
					otherEnemyStatus.isHeld == true){
					enemyStatus.isHeld = false;
					enemyStatus.isThrow = false;
					enemyStatus.enemyAction.SetBool(key_isDestroyed, true);
					rb.constraints = RigidbodyConstraints2D.FreezeAll;
					GameObject.Destroy(this.GetComponent<BoxCollider2D>());
					this.tag = "crash";
					Destroy(gameObject,0.5f);
					audioSource.Play(0);
				}else{
					//移動中の敵との衝突で消滅
					enemyStatus.isRebound = true;
					isThrowAngle = 120.0f;
				}
			}
		}
		//プレイヤーとの衝突でキャッチされる
		if(other.gameObject.tag == "player"){
			enemyStatus.isMounted = true;
		}
	}
	
	//プレイヤーとの衝突でキャッチされる
	void OnCollisionExit2D (Collision2D other) {
		if(other.gameObject.tag == "player"){
			enemyStatus.isMounted = false;
		}
	}
}
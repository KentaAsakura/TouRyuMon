//プレイヤー(自機)の行動
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class player : MonoBehaviour {
	float scrollSpeed;
	float jumpPower = 5f;
	Rigidbody2D rb;
	public float jumpTime = 0f;
	private float jumpLimit = 25f;
	//キーを押している間に上昇できるかの判定
	public bool  jumpUp = false;
	//敵を持っているか判定
	public bool holdEnemy = false;
	//地面か敵の上に立っているか判定
	public bool onGround = false;
	//敵の上に立っているか判定
	public bool onEnemy = false;
	Vector2 throwSpeed;
	public bool gameOver = false;
	//持ってる物のステータス
	GameObject holdObject;
	enemy holdObjectStatus;
	public GameObject shot;
	Vector3 shotPos;
	//射出中の弾の数
	public int isShot;
	//弾の射出方向    0:→ 1:／ 2:￥
	public int shotArrow;
	public int shotLevel;
	public float speedLevel;
	public float speed;
	float shotInstX;
	//音声関連
	private AudioSource seJump;
	private AudioSource seThrow;
	private Animator playerAction;
	//乗っているもののステータス
	GameObject onEnemyObject;
	private const string key_isHeld = "isHeld";
	//アニメーション遷移フラグ
	private const string key_isJump = "isJump";
	private const string key_isHold = "isHold";
	//ゲーム開始直後は動けないようにするフラグ
	public bool isReady;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		playerAction = GetComponent<Animator>();
		shotInstX = this.GetComponent<RectTransform>().sizeDelta.x+0.1f;
		AudioSource[] audioSources = GetComponents<AudioSource>();
		seJump = audioSources[0];
		seThrow = audioSources[1];
		isReady = true;
		isShot = 0;	//射出中の弾の数
		shotArrow = 0;
		shotLevel = 1;
		speedLevel = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		speed = mainCamera.scrollSpeed*(1f+0.05f*speedLevel);
		//ゲーム開始直後は行動不可
		if(isReady == false){
			//Xキーを離した時に上昇不可になる
			if(Input.GetKeyUp(KeyCode.M) || CrossPlatformInputManager.GetButtonUp("Jump")){
				jumpUp = false;
			}
			//設置状態でXキーを押すとジャンプ
			if((onGround == true || onEnemy == true)&& (Input.GetKeyDown(KeyCode.M) || CrossPlatformInputManager.GetButtonDown("Jump"))){
				rb.velocity = new Vector2(speed,jumpPower);
				jumpTime++;
				jumpUp =true;
				onGround = false;
				seJump.Play(0);
			}
			//キーを押し続け上昇する
			if(jumpUp == true && (Input.GetKey(KeyCode.M) || CrossPlatformInputManager.GetButton("Jump"))){
				rb.velocity = new Vector2(speed,jumpPower);
				jumpTime++;
				//Xキーの押下が一定時間続くと上昇が止まる
				if(jumpTime >= jumpLimit){
					jumpUp = false;
				}
			}
			//下降の判定
			if(onGround == false && onEnemy == false && jumpUp == false){
				rb.velocity = new Vector2(speed,-jumpPower);
			}else if(onGround == true){
			//接地状態で歩く
				rb.velocity = new Vector2(speed,0);
			}
			//射出中の弾が強化レベル未満で何も持っていない時にZキーでショット
			if(holdEnemy == false && 
				(Input.GetKeyDown(KeyCode.N) || CrossPlatformInputManager.GetButtonDown("Atack"))){
				shotPos.x = this.transform.position.x+shotInstX;
				shotPos.y = this.transform.position.y;
				shotPos.z = this.transform.position.z;
				shotArrow = 0;
				//射出中の弾の数で発射する弾の数を変化させる(画面上に最大3個までしか弾を発射させない)
				for(int i=0;i<shotLevel;i++){
					isShot = GameObject.FindGameObjectsWithTag("shot1").Length;
					if(isShot < shotLevel){
						Instantiate(shot,shotPos,this.transform.rotation);
					}else{
						break;
					}
				}
				shotArrow = 0;
			}
			//持ってる敵を投げる
			if(holdEnemy == true && 
				(Input.GetKeyUp(KeyCode.N) || CrossPlatformInputManager.GetButtonUp("Atack"))){
				holdEnemy = false;
				playerAction.SetBool(key_isHold, false);
				seThrow.Play(0);
				holdObjectStatus.isHeld =false;
				holdObjectStatus.isThrow =true;
				holdObject = null;
				holdObjectStatus = null;
			}
		//ゲーム開始直後に空中に浮いてるプレイヤーを落下させる
		}else{
			//下降の判定
			if(onGround == true){
				rb.velocity = new Vector2(speed,0);
			}else{
				rb.velocity = new Vector2(speed,-jumpPower);
			}
		}
	}
	
	void OnTriggerExit2D (Collider2D other) {
		//地面や敵から離れて空中でのジャンプ不可になる(二段ジャンプ回避)
		if(other.gameObject.tag == "ground" || other.gameObject.tag == "enemy"){
			onGround = false;
			onEnemy = false;
			playerAction.SetBool(key_isJump, true);
		}
	}
	void OnTriggerEnter2D (Collider2D other) {
		//地面や敵に着地してジャンプ可能になる
		if(other.gameObject.tag == "ground"){
			jumpTime = 0f;
			onGround = true;
			playerAction.SetBool(key_isJump, false);
			jumpUp = false;//koko
		}
		if(other.gameObject.tag == "enemy"){
			jumpTime = 0f;
			onEnemy = true;
			playerAction.SetBool(key_isJump, false);
			jumpUp = false;//koko
		}
	}
		
	void OnCollisionEnter2D (Collision2D other) {
		//投げられている敵をキャッチ
		if(other.gameObject.tag == "crashableEnemy"){
			if(other.gameObject.GetComponent<enemy>().isRebound == true){
				holdObjectStatus = other.gameObject.GetComponent<enemy>();
				holdObjectStatus.isHeld = true;
				holdObjectStatus.isThrow = false;
				holdObjectStatus.isRebound = false;
				other.gameObject.GetComponent<enemyAction>().isThrowAngle = 120.0f;
				holdObjectStatus.enemyAction.SetBool(key_isHeld, true);
				holdEnemy = true;
				onGround = false;
				onEnemy = false;
				playerAction.SetBool(key_isHold, true);
			}
		}
	}
	
	void OnTriggerStay2D (Collider2D other) {
		//トゲに触ってゲームオーバー
		if(other.gameObject.tag == "thorns"){
			//持ってる敵を最後の悪あがきで投げる(これが無いと持っていた敵が不自然に空中に留まり続ける)
			if(holdEnemy == true){
				holdEnemy = false;
				playerAction.SetBool(key_isHold, false);
				holdObjectStatus.isHeld =false;
				holdObjectStatus.isThrow =true;
			}
			gameOver = true;
			this.gameObject.SetActive(false);
		}
		//接地したはずなのにジャンプできなくなる場合がある現象の対策
		if(other.gameObject.tag == "ground" || other.gameObject.tag == "enemy"){
			jumpTime = 0f;
			onGround = true;
		}
		//何も持たずに敵に乗っているときに敵を持つ
		if(other.gameObject.tag == "enemy" && holdEnemy == false && 
			(Input.GetKeyDown(KeyCode.N) || CrossPlatformInputManager.GetButtonDown("Atack"))){
			holdObject = other.gameObject;
			holdObjectStatus = holdObject.GetComponent<enemy>();
			holdObjectStatus.isHeld = true;
			holdObject.tag = "crashableEnemy";
			holdObjectStatus.enemyAction.SetBool(key_isHeld, true);
			holdEnemy = true;
			onGround = false;
			onEnemy = false;
			playerAction.SetBool(key_isHold, true);
		}
	}
}

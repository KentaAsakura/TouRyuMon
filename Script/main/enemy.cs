/*
敵のステータスを管理する
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
	Vector3 heldPos;
	static GameObject player;
	player playerStatus;
	public string enemyName;
	int enemyNameCount;
	//他の敵とぶつかって破壊した回数
	public int enemyClash = 0;
	public bool isThrow = false;
	public bool isHeld = false;
	public bool isRebound = false;
	public bool isCrashable = false;
	public bool isMounted = false;
	public Animator enemyAction;
	public bool gameClear;
	public bool gameOver;
	
	void Start () {
		player = GameObject.FindWithTag("player");
		playerStatus = player.GetComponent<player>();
		enemyAction = GetComponent<Animator>();
		enemyName = transform.name;
		enemyNameCount = enemyName.Length;
		//(clone)を除去
		enemyName = enemyName.Substring(0,enemyNameCount-7);
	}
	
	public static Vector2 isThrowVector(float angle,bool isRebound,float power){
		float isThrowX;
		float isThrowY;
		if(isRebound == false){
			isThrowX = 2*power*Mathf.Sin(angle*Mathf.Deg2Rad);
			isThrowY = -1*power*Mathf.Cos(angle*Mathf.Deg2Rad);
		}else{
			isThrowX = -2*power*Mathf.Sin(angle*Mathf.Deg2Rad);
			isThrowY = -1*power*Mathf.Cos(angle*Mathf.Deg2Rad);
		}
		
		Vector2 returnVector = new Vector2(isThrowX,isThrowY);
		return returnVector;
	}
	
	public static Vector3 isHeldPosition(float thisHeight){
		float heldPosX = player.transform.position.x;
		float heldPosY = player.transform.position.y+player.transform.localScale.y/2f+thisHeight;
		float heldPosZ = 0;
		Vector3 returnVector = new Vector3(heldPosX,heldPosY,heldPosZ);
		return returnVector;
	}
}

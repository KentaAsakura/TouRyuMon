/*
地形の行動
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : MonoBehaviour {
	int destroyCount=0;
	
	// Update is called once per frame
	void Update () {
		//自動生成後に一定時間経過で消滅させる
		destroyCount++;
		if(destroyCount >= 1000){
			Destroy(gameObject);
		}
	}
}
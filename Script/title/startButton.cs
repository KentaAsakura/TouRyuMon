/*
ゲームスタートボタン
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startButton : MonoBehaviour {
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void OnClick(){
		audioSource.Play(0);
		StartCoroutine("waitLoad");
	}
	
	IEnumerator waitLoad(){
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene("Main");
	}
	
}

/*
タイトルに戻るボタン
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class titleButton : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void OnClick(){
		Debug.Log("title");
		SceneManager.LoadScene("Title");
	}
	
}

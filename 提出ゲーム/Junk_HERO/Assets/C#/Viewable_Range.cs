using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//imageが見える距離を制御するスクリプト
public class Viewable_Range : MonoBehaviour { 

	GameObject maincamera;


	public GameObject view;

	float viewablerange = 30; 

	// Use this for initialization
	void Start () {

		maincamera = GameObject.Find ("/Junk_Robot/camera/MainCamera");

	}
	
	// Update is called once per frame
	void Update () {

		//見える距離以内なら見せる、そうでないなら消す
		if (Vector3.Distance(transform.position,maincamera.transform.position) < viewablerange) {
			view.transform.LookAt(maincamera.transform.position);
			view.SetActive (true);
		} else {
			view.SetActive (false);
		}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_Change : MonoBehaviour {//当たったものの色を変えるスクリプト

	Vector4 color;


	void Start () {

		color = GetComponent<MeshRenderer> ().material.color;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision cln) {

		if (cln.collider.tag != "Player") {

			cln.collider.GetComponent<MeshRenderer> ().material.color = color;

		}

	}
}

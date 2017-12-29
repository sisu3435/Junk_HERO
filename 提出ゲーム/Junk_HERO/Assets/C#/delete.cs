using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete : MonoBehaviour {//時間で消滅するスクリプト
	
	public float count;


	void Start () {
		
	}
	

	void Update () {
		count -= Time.deltaTime;
		if (count < 0) { //指定された秒数を過ぎたら自分を破壊する
			Destroy (gameObject);
		}
	}
}

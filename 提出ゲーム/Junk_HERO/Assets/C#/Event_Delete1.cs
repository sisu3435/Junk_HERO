using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Delete1 : MonoBehaviour {//一定の数の敵を倒すと消滅するオブジェクトのスクリプト

	public int Count;

	public GameObject[] Enemy;


	void Start () {

		Count = Enemy.Length;
	}
	

	void Update () {

		//敵が倒されたらカウントを減らす
		for (int i = 0; i < Enemy.Length; i++) {
			if (Enemy [i] == null) {
				Enemy [i] = gameObject;
				Count -= 1;
			}
		}
		
		if (Count == 0) { // 敵の数が0になったら自分を破壊する
			Destroy (gameObject);
		}



	}
}

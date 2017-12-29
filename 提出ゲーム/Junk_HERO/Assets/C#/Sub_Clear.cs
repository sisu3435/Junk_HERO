using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//サブ条件を達成した時にそれをステージクリアに送るスクリプト
public class Sub_Clear : MonoBehaviour {


	void Start () {
		
	}
	

	void Update () {
		
	}

	//サブ条件を達成したことをゴールに送る
	public void subclear(GameObject goal){

		goal.GetComponent<Stage_Clear> ().sub_clear = true;

	}


}
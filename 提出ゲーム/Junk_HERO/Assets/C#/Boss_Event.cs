using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bossを登場させるスクリプト
public class Boss_Event : MonoBehaviour {

	//退路を断つもの(壁等)
	public GameObject Closure_Wall;

	//ボスとの戦闘が始まっているか
	public bool boss_battle = false; 

	//物を消すのかどうか
	//(trueなら出現させ、falseなら消滅させる)
	public bool fdelete_tpop; 


	void Start () {
		
	}
	

	void Update () {
		
	}


	void OnTriggerEnter(Collider col){
		
		if (col.tag == "Player") {

			//退路を断つ為の処理(扉を出現させるなど）
			if (Closure_Wall != null) {
				
				if (fdelete_tpop == true) { //出現させる
					
					Closure_Wall.GetComponent<BoxCollider> ().enabled = true;

					Closure_Wall.GetComponent<MeshRenderer> ().enabled = true;

				}

				else if (fdelete_tpop == false) { //消滅させる
					
					Closure_Wall.GetComponent<BoxCollider> ().enabled = false;

					Closure_Wall.GetComponent<MeshRenderer> ().enabled = false;

				}

			}

			boss_battle = true; //ボスとの戦闘を開始する

		}

	}


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//プレイヤーの技を管理するスクリプト
public class Player_S_Select : MonoBehaviour { 



	public GameObject S1;//特殊行動１

	public GameObject S2;//〃2

	public GameObject S3;//〃3

	public GameObject S4;//〃4

	public Text S_select;// 現在選択中の技を示すテキスト



	public int Selected = 1; //選択された技番号





	// Use this for initialization
	void Start () {

		S_select.text = "1：" + S1.name;

	}
	
	// Update is called once per frame
	void Update () {


		// 技を切り替える（ 1→2→3→4→1 ）
		if (Input.GetMouseButtonDown (1)) { 
			if (Selected == 1) {

				Selected = 2;

				S_select.text = Selected + "：" +
					S2.name;

			} else if (Selected == 2) {

				Selected = 3;

				S_select.text = Selected + "：" +
					S3.name;

			} else if (Selected == 3) {

				Selected = 4;

				S_select.text = Selected + "：" +
					S4.name;

			} else {

				Selected = 1;

				S_select.text = Selected + "：" +
					S1.name;

			}

		}

	}



}
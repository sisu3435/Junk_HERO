using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour { //第一ステージの補助のテキストを表示するスクリプト

	public Text command; // 命令を出すテキスト

	public GameObject Panel; //commandの後ろのパネル

	public int flag;

	void Start () {
	
	}
	

	void Update () {
		
	}


	void OnTriggerEnter(Collider col){

		if (col.tag == "Player") {

			StartCoroutine ("Text_Change");

		}

	}


	IEnumerator Text_Change(){

		command.enabled = true;

		Panel.SetActive (true);

		switch (flag) {

		case 1:
			command.text = "今から指示をしていきます。\n" +
			"指示した通りに行動してください。";

				yield return new WaitForSeconds (2f);

			command.text = "Qキーでメニューが開けます。\n" +
				"操作方法を確認してください。";

			break;

		case 2:

			command.text = "パイプをジャンプで飛び越えてください。\n" +
				"Spaceキー";

			break;

		case 3:

			command.text = "囲いの中にあるJunkを回収して技1を使い、\n" +
				"段差を飛び越えてください。\n" +
				"Junkが10以上の時、技をS1にして左クリック";

			break;

		case 4:

			command.text = "技2を使い、目の前の紫色の壁を破壊して進んでください。\n" +
				"Junkが20以上の時、技をS2にして左クリック";

			break;

		case 5:

			command.text = "目の前のオブジェクトは技2、技３で破壊できます。\n" +
				"破壊してJunkを回収してください。";

			break;

		case 6:

			command.text = "前の扉をくぐればチュートリアルクリアです。" +
				"お疲れ様でした。";

			break;


		}

		yield return new WaitForSeconds (10f);

		command.enabled = false;

		Panel.SetActive (false);

	}


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//サブ条件が達成されていたらオブジェクトを見えるようにする処理
public class Sub_Clear_Pop : MonoBehaviour {

	public int sub_number;

	public GameObject[] pop_object;

	void Start () {

		//特定のステージのサブ条件を達成していたら見えるようにする。
		if (PlayerPrefs.GetInt ("Stage" + sub_number, 0) == 1) {

			for (int i = 0; i < pop_object.Length; i++) {

				pop_object[i].SetActive (true);

			}

		} else {

			for (int i = 0; i < pop_object.Length; i++) {

				pop_object[i].SetActive (false);

			}

		}


		Destroy (gameObject);

	}
	

	void Update () {
		
	}


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Move : MonoBehaviour { //プレイヤーを動かすスプリクト

	Rigidbody rb; //Rigidbody

	public bool isGround = true; //接地判定

	public LayerMask Yuka; //床のレイヤー

	float high_limit; //高さ制限

	private float x; //横

	private float z; //縦

	float rote_x,rote_y;// カメラの縦横

	float speed = 8f; //移動速度

	public float jumpPower = 1000; //跳躍力

	public Camera MainCamera; // メインカメラ

	public GameObject Camera_Rote; // カメラの向き用のゲームオブジェクト

	public AudioSource AS; // 音を流すオーディオソース

	public AudioClip Junp_Sound;//ジャンプする音

	Vector3 sphere_first; //Spの初期サイズ


	void Start () {


		rb = GetComponent<Rigidbody> (); //RigidBody

		Cursor.visible = false; //マウスのカーソルの不可視化

		Cursor.lockState = CursorLockMode.Locked; //マウスのカーソルの位置の固定化

		rote_x = 0;

		rote_y = 0;

		high_limit = 30;

	}


	void Update () {

		if (transform.position.y < -3) { //穴に落ちた場合ゲームオーバーにする
		
			Cursor.visible = true;

			Cursor.lockState = CursorLockMode.None;

			SceneManager.LoadScene ("GameOver_Scene");
		
		}
			


		float mouse_x_delta = Input.GetAxis ("Mouse X");//マウスの横移動

		float mouse_y_delta = Input.GetAxis ("Mouse Y");//マウスの前後移動

		float angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f); //向きの取得

		Vector3 dir = new Vector3 (Mathf.Sin (angleDir), 0, Mathf.Cos (angleDir)); //前後移動用

		Vector3 undir = new Vector3 (Mathf.Cos (-angleDir), 0, Mathf.Sin (-angleDir)); //左右移動用

		x = Input.GetAxisRaw ("Horizontal");//横方向の移動キー取得

		z = Input.GetAxisRaw ("Vertical");//縦方向の移動

		//中央から足元にかけて、接地判定用のラインを引く
		isGround = Physics.Linecast (
			transform.position + transform.up * 0.5f,
			transform.position - transform.up * 0.5f,
			Yuka); //Linecastが判定するレイヤ-

		//一定以上の高さに達した時、強制的に下へ落とす
		if (transform.position.y > high_limit) {

			rb.velocity = new Vector3 (rb.velocity.x, -1f, rb.velocity.z);

		}

		if (x != 0 || z != 0) { // 移動処理
			
			transform.position += dir * z * speed * Time.deltaTime
			+ undir * x * speed * Time.deltaTime;
			
		}

		if (Input.GetKey (KeyCode.RightArrow)) { //十字キーでの視点操作(横)
			
			mouse_x_delta = 1;

		} else if (Input.GetKey (KeyCode.LeftArrow)) { 
			
			mouse_x_delta = -1;

		}


		if (Input.GetKey (KeyCode.UpArrow)) {//十字キーでの視点操作(縦)
			
			mouse_y_delta = -1;

		} else if (Input.GetKey (KeyCode.DownArrow)) {
			
			mouse_y_delta = 1;

		}

		if (mouse_x_delta != 0) { //横回転
			
			rote_x += mouse_x_delta * 10;

		}

		if (mouse_y_delta != 0) { // カメラだけ縦回転（-60～60度以内）
			
			rote_y -= mouse_y_delta;

			if (rote_y < -30) {
				
				rote_y = -30;

			}

			if (rote_y > 30) {
				
				rote_y = 30;

			}
		}
			
			// 向き（横）
		transform.rotation = Quaternion.Euler 
			(transform.rotation.x, rote_x, transform.rotation.z);

		Camera_Rote.transform.rotation = Quaternion.Euler (rote_y,
			rote_x, Camera_Rote.transform.rotation.z); //向き（縦)




		if (isGround && Input.GetKeyDown (KeyCode.Space)) { //ジャンプ

			rb.AddForce (transform.up * jumpPower);

			AS.clip = Junp_Sound;

			AS.Play ();

		}

		if (Input.GetKeyDown (KeyCode.Escape)) { // ゲームを終了させる
			
			Cursor.visible = true;

			Cursor.lockState = CursorLockMode.None;

			Application.Quit ();

		}

	}






}
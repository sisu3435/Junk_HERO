using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subjectmatter_Canvas_Controll : MonoBehaviour {

	Canvas subjectmatter_canvas;//素材元を表示するキャンバス


	void Start () {

		subjectmatter_canvas = GetComponent<Canvas> ();

		subjectmatter_canvas.enabled = false;
		
	}
	

	void Update () {
		
	}


	//素材元を表示/非表示する処理
	public void SubjectMatter_View()
	{

		if (subjectmatter_canvas.enabled == false) {

			subjectmatter_canvas.enabled = true;

		} else {

			subjectmatter_canvas.enabled = false;

		}

	}

}
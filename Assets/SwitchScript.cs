using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour {

	public Camera camA, camB;

	private Color color;
	private SpriteRenderer sr;
	private Transform selfTrans;
	private bool isGrowing = false;

	private void OnEnable(){
		sr = GetComponent<SpriteRenderer>();
		color = sr.color;
		selfTrans = transform;
	}

	private void Update(){
		if(isGrowing == true){
			if( selfTrans.localScale.x < 45f ) {
				selfTrans.localScale += new Vector3(1 + Time.deltaTime, 1 + Time.deltaTime, 0);
			}else{
				isGrowing = false;
				camA.backgroundColor = color;
				camB.backgroundColor = color;

				gameObject.SetActive(false);
				Time.timeScale = 1;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D _collider){
		if(_collider.gameObject.CompareTag("Ball")){
			Time.timeScale = 0;
			isGrowing = true;
		}
	}
}

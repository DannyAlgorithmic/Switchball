using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	private PlayerEnum PlayerFilter;


	private void OnCollisionEnter2D(Collision2D _collision){
		if(_collision.gameObject.CompareTag("Flipper")){
			FlipperController ctrl = _collision.transform.parent.GetComponent<FlipperController>();
			PlayerFilter = ctrl.Player;

			Debug.Log("Ball Is Effecting: " + PlayerFilter);
		}
	}

}

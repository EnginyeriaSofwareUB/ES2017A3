using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour {

	public enum MOVEMENT_DIRECTION {
		HORIZONTAL, VERTICAL
	}

	public int length;

	private Vector3 vector;

	// Use this for initialization
	void Start () {
	}

	public void setMovementDirection(MOVEMENT_DIRECTION direction){
		switch (direction) {
		case(MOVEMENT_DIRECTION.HORIZONTAL):
			this.vector = Vector3.right;
			break;
		case(MOVEMENT_DIRECTION.VERTICAL):
			this.vector = Vector3.up;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (this.vector * Mathf.Cos (Time.time) * Time.deltaTime * this.length);
	}
}

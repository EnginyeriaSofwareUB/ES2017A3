using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour {

	public enum MOVEMENT_DIRECTION {
		HORIZONTAL, VERTICAL
	}

	public int length;

	public float speed = 0.2f;

	public float top;

	public float floor;

	private Vector3 vector;
	private float coordinate;
	
	private MOVEMENT_DIRECTION direction;

	// Use this for initialization
	void Start () {
	}

	public void setMovementDirection(MOVEMENT_DIRECTION direction){
		this.direction = direction;
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
		switch (this.direction) {
		case(MOVEMENT_DIRECTION.HORIZONTAL):
			this.coordinate = gameObject.transform.position.x;
			break;
		case(MOVEMENT_DIRECTION.VERTICAL):
			this.coordinate = gameObject.transform.position.y;
			break;
		}
		if (this.coordinate > top || this.coordinate < floor)
			this.speed = -this.speed;
		transform.Translate (this.vector * speed);
	}
}

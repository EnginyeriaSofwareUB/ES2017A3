using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner: MonoBehaviour{

	public GameObject platform;

	private static Vector3[] coordinates = {
		new Vector3 (32.58f, -6.89f, 0f),
		new Vector3 (-15.44f, -6.89f, 0f),
		new Vector3 (9.15f, -0.67f, 0f)
	};

	public static string tag = "Platform";

	public void generatePlatforms(){
		for (int i = 0; i < 3; i++) {
			GameObject emptyObject = new GameObject ();
			emptyObject.name = "Platform";
			emptyObject.transform.position = coordinates [i];
			emptyObject.tag = tag;
			GameObject platform = Instantiate(this.platform, Vector3.zero, Quaternion.identity);
			platform.transform.parent = emptyObject;
			MovablePlatform script = emptyObject.AddComponent<MovablePlatform> ();
			if (i < 2)
				script.setMovementDirection (MovablePlatform.MOVEMENT_DIRECTION.VERTICAL);
			else
				script.setMovementDirection (MovablePlatform.MOVEMENT_DIRECTION.HORIZONTAL);
			script.length = 2;
		}
	}
}
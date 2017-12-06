using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner: MonoBehaviour {

	private GameObject platformVertical, platformHorizontal;

	private List<Vector3> coordinates;

	private static Vector2[] topFloors = {
		new Vector2 (-0.4f, -15.26f),
		new Vector2 (-3.5f, -10.7f),
		new Vector2 (13.16f, 5.18f)
	};

	public static string PlatformTag = "MovingPlatform";

	void Start() {
		this.coordinates = new List<Vector3>();
		this.coordinates.Add(new Vector3 (32.58f, -6.89f, 0f));
		this.coordinates.Add(new Vector3 (-15.44f, -6.89f, 0f));
		this.coordinates.Add(new Vector3 (9.15f, -0.67f, 0f));
		this.platformVertical = Resources.Load("Meshes/SceneObjects/MovablePlatforms/VerticalMovablePlatform") as GameObject;
		this.platformHorizontal = Resources.Load("Meshes/SceneObjects/MovablePlatforms/HorizontalMovablePlatform") as GameObject;
		this.generatePlatforms();
	}

	private void generatePlatforms(){
		generatePlatform(0.075f, this.platformVertical, this.coordinates[0], Vector3.one * 0.7f, MovablePlatform.MOVEMENT_DIRECTION.VERTICAL, topFloors[0]);
		generatePlatform(0.075f, this.platformVertical, this.coordinates[1], Vector3.one * 0.7f, MovablePlatform.MOVEMENT_DIRECTION.VERTICAL, topFloors[1]);
		generatePlatform(0.075f, this.platformHorizontal, this.coordinates[2], Vector3.one, MovablePlatform.MOVEMENT_DIRECTION.HORIZONTAL, topFloors[2]);
	}

	private void generatePlatform(float speed, GameObject prefab, Vector3 position, Vector3 scale, MovablePlatform.MOVEMENT_DIRECTION direction, Vector2 topFloor){
		GameObject emptyObject = new GameObject ();
		emptyObject.name = "Platform";
		emptyObject.transform.position = position;
		emptyObject.tag = PlatformTag;
		GameObject platform = Instantiate(prefab, Vector3.zero, prefab.transform.rotation);
		platform.tag = PlatformTag;
		setParent(platform, emptyObject);
		platform.transform.localScale = scale;
		MovablePlatform script = emptyObject.AddComponent<MovablePlatform> ();
		script.setMovementDirection (direction);
		script.speed = speed;
		script.top = topFloor.x;
		script.floor = topFloor.y;
	}

	private void setParent(GameObject child, GameObject parent){
		Quaternion saveLocalRotation = child.transform.localRotation;
		child.transform.parent = parent.transform;
		child.transform.localPosition = Vector3.zero;
		child.transform.localRotation = saveLocalRotation;
	}
}
using UnityEngine;
using System.Collections;

public class Teletransporte : MonoBehaviour
{

	private GameObject efectoBola;
	private GameManager gameManager;
	private Totem totem;
	private bool transportado;

    // Use this for initialization
    void Start()
    {
		GameObject efectoBola = Instantiate(Resources.Load("ErekiBall2"), this.transform.position, Quaternion.identity) as GameObject;
		efectoBola.transform.parent = this.transform;
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		totem = GetComponentInParent<Totem>();
		transportado = false;

    }
  

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown (0)) {
			
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) //, 100
			{
				string tag = hit.collider.tag;
				if (tag == "TerrainObject")
				{
					Debug.Log ("susususususususu");
				}
				float z = totem.gameObject.transform.position.z;
				Vector3 newPos = new Vector3 (hit.point.x, hit.point.y, z);
				totem.gameObject.transform.position = newPos;
				transportado = true; 

			}
		}

		if (transportado) {
			Destroy (gameObject); 
			Destroy (efectoBola);
		}
    }
}

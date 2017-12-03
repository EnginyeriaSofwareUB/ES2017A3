using UnityEngine;
using System.Collections;

public class CheckIsVisible : MonoBehaviour
{
	private Totem totem;

	//IMPORTANTE:
	// Para que este módulo funcione se requiere que el objeto tenga un spriteRenderer de lo contrario no hará nada.
	// Use this for initialization
	void Start ()
	{
		this.totem = GetComponent<Totem> ();
	}

	void OnBecameInvisible() {
		//Debug.Log("CheckIsVisible :: Destroy object");
		if (this.totem != null) {
			Debug.Log("X is = " + this.totem.gameObject.transform.position.x +  " Y is = " + this.totem.gameObject.transform.position.y);
			this.totem.suicide();
		} else {
			Destroy(this.gameObject);
		}
	}
}


using UnityEngine;
using System.Collections;

public class CheckIsVisible : MonoBehaviour
{
	private Totem totem;

	private StateHolder stateHolder;

	//IMPORTANTE:
	// Para que este módulo funcione se requiere que el objeto tenga un spriteRenderer de lo contrario no hará nada.
	// Use this for initialization
	void Start ()
	{

		this.totem = GetComponent<Totem> ();
		this.stateHolder = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateHolder>();
	}

	void OnBecameInvisible() {
		if(this.stateHolder.isPlaying()){
			if (this.totem != null) {
				this.totem.eliminarTotem();
			} else {
				Destroy(this.gameObject);
			}
		}
	}
}


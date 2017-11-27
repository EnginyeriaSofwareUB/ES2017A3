using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class GrenadeLogic : MonoBehaviour {

		private CircleCollider2D destructionCircle;
        public float damage = 1;

        public GameObject Player { get; set; }

		private Collision2D collision;

		private bool ignore = false;


        void Start () {
			this.destructionCircle = GetComponent<CircleCollider2D> ();
        }
	
        void Update ()
        {
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.gameObject == this.Player)
            {
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision.collider);
            }

            string tag = collision.collider.tag;
            if (tag == "TerrainObject")
            {
				this.collision = collision;
				if (!ignore)
					Invoke("DoSomething", 2);
				ignore = true;
            }
            else if (tag.Contains("Player"))
            {
                int id = collision.collider.gameObject.GetInstanceID();
                GameObject[] totems = GameObject.FindGameObjectsWithTag(tag.Replace("Module", ""));
                foreach (GameObject g in totems)
                {
                    Totem totem = g.GetComponent<Totem>();
                    foreach (GameObject mod in totem.Modulos)
                    {
                        if (mod.GetInstanceID() == id)
                            totem.DecreaseVida(damage);
                    }
                }
            }

            //Destroy(this.gameObject);
        }

		void DoSomething() {
			Terrain2 t = this.collision.gameObject.GetComponent<Terrain2>();
			t.DestroyGround (destructionCircle);
			Destroy (this.gameObject);
		}
	}
}

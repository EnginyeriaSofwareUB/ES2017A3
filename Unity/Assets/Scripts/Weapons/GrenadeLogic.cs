using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class GrenadeLogic : MonoBehaviour {

        public float radius = 5.0F;
        public float power = 10.0F;

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

            this.collision = collision;

            //Destroy(this.gameObject);
        }

		void DoSomething() {
            /*Vector3 explosionPos = this.transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

                if (rb != null)
                    rb.AddForce(explosionPos, ForceMode2D.Impulse);
            }*/
            Terrain2 t = this.collision.gameObject.GetComponent<Terrain2>();
            if (t != null)
			    t.DestroyGround (destructionCircle);
			Destroy (this.gameObject);
		}
	}
}

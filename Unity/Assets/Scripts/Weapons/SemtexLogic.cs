using Assets.Scripts.Environment;
using UnityEngine;
using YounGenTech.HealthScript;

namespace Assets.Scripts.Weapons
{
    public class SemtexLogic : MonoBehaviour
    {

        public float radius = 6.0F;
        public float power = 10.0F;

        private CircleCollider2D destructionCircle;
        public float damage = 1;

        public GameObject Player { get; set; }

		private Collision2D collision;

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
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision.collider);
				Rigidbody2D r = GetComponent<Rigidbody2D> ();
				r.velocity = Vector3.zero;
				r.gravityScale = 0;
				this.collision = collision;
				Invoke("Explosion", 2);
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
                            totem.SendMessage("Damage", new HealthEvent(gameObject, damage));
                            totem.DecreaseVida();
                    }
                }
            }
        }

		void Explosion() {
            Terrain2 t = this.collision.gameObject.GetComponent<Terrain2>();
            if (t != null)
            {
                destructionCircle.radius = radius;
                t.DestroyGround(destructionCircle);
            }
            Destroy(this.gameObject);
        }
	}
}

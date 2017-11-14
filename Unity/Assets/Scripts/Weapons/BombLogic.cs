using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class BombLogic : ExplosionBehavior {

		public CircleCollider2D destructionCircle;
        public float damage = 1;

        public GameObject Player { get; set; }

        void Start () {
			this.destructionCircle = GetComponent<CircleCollider2D> ();
        }
	
        void Update ()
        {
            if (this.UpdateExplosion())
            {
                Destroy(this.gameObject);
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.gameObject == this.Player)
            {
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision.collider);
                return;
            }

			if (collision.collider.tag == "TerrainObject") 
			{
				Terrain2 t = collision.gameObject.GetComponent<Terrain2>();
				t.DestroyGround (destructionCircle);
				Destroy (this.gameObject);
			}
            string tag = collision.collider.tag;
            if (tag.Contains("Player"))
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
                Destroy(this.gameObject);
            }
        }
    }
}

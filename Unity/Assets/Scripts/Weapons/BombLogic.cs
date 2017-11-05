using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class BombLogic : ExplosionBehavior {

		public CircleCollider2D destructionCircle;

        public GameObject Player { get; set; }

        private Totem totem;

        void Start () {
			this.destructionCircle = GetComponent<CircleCollider2D> ();
            this.totem = GetComponent<Totem>();
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
            //&& !collision.collider.tag.Equals(this.Player.tag + "Module")
            Debug.LogError(this.totem.tag);
            if (collision.collider.tag.Contains("PlayerModule"))
            {
                Totem t = collision.gameObject.GetComponent<Totem>();
                t.DecreaseVida(100);
                Destroy(this.gameObject);
            }
        }
    }
}

using Assets.Scripts.Environment;
using UnityEngine;
using YounGenTech.HealthScript;

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

        public GameObject explosion;


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
					Invoke("Explosion", 2);
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
                        {
                            // totem.DecreaseVida(damage);
                            totem.SendMessage("Damage", new HealthEvent(gameObject, damage));
                            totem.DecreaseVida();
                        }
                    }
                }
            }

            this.collision = collision;
        }
        void Explosion()
        {
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
            {
                destructionCircle.radius = radius;
                t.DestroyGround(destructionCircle);
            }
            GameObject executeDeathExplosion = Instantiate(this.explosion, this.gameObject.transform.position, this.explosion.transform.rotation);
            Destroy(executeDeathExplosion, executeDeathExplosion.GetComponent<AudioSource>().clip.length);
            //Destroy(this.gameObject);
        }
    }
}

using Assets.Scripts.Environment;
using UnityEngine;
using YounGenTech.HealthScript;

namespace Assets.Scripts.Weapons
{
    public class MissileLogic : MonoBehaviour
    {

        public float radius = 7.0F;
        public float power = 10.0F;

        private CircleCollider2D destructionCircle;
        public float damage = 1;

        public GameObject Player { get; set; }

        public GameObject explosion;

		public ParticleSystem particle;

        void Start()
        {
            this.destructionCircle = GetComponent<CircleCollider2D>();
			ParticleSystem fire = Instantiate(this.particle, this.gameObject.transform.position, this.particle.transform.rotation);
			fire.transform.parent = this.transform;
        }

        void Update()
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
                Terrain2 t = collision.gameObject.GetComponent<Terrain2>();
                float tmp = destructionCircle.radius;
                destructionCircle.radius = radius;
                t.DestroyGround(destructionCircle);
                destructionCircle.radius = tmp;
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
                            if (totem.IgluActivado() && (mod.GetInstanceID() == totem.GetIDModuloProtegidoIglu()))
                            {
                                Iglu ig = totem.GetComponentInChildren<Iglu>();
                                ig.IncNumeroUsos();
                                Destroy(this.gameObject);
                            }
                            else
                            {
                                totem.SendMessage("Damage", new HealthEvent(gameObject, damage));
                                totem.DecreaseVida();
                            }
                        }
                    }
                }
            }

            GameObject executeDeathExplosion = Instantiate(this.explosion, this.gameObject.transform.position, this.explosion.transform.rotation);
            Destroy(executeDeathExplosion, executeDeathExplosion.GetComponent<AudioSource>().clip.length);

            Destroy(this.gameObject);
        }
    }
}
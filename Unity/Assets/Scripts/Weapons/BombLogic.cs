using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class BombLogic : ExplosionBehavior {

        public GameObject Player { get; set; }

        void Start () {
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
                
            this.StartExplosion();
        }
    }
}

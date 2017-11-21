using System;
using Assets.Scripts.Player;
using UnityEngine;
using Assets.Scripts.Environment;

namespace Assets.Scripts.Weapon
{
    public class WeaponLogic : MonoBehaviour
    {
        private PlayerBehaviour Player { get; set; }

        // Variable que tiene toda la estructura Totem
        private Totem totem;

        private float ShootingAngle = 45;
        private float ShootingPower = 100;

        private Vector3 ShootingVelocity;
        private Vector3 ShotStartingPoint;

        void Start()
        {
            this.Player = GetComponent<PlayerBehaviour>();
            this.totem = GetComponent<Totem>();
            CalculateVelocity();
        }

        void Update()
        {
            ShotStartingPoint = CalculateFirePoint(ShootingVelocity);
        }


        public void Shoot()
        {
            var weapon = CreateWeapon("Weapons/Bomb", ShotStartingPoint);

            SetWeaponVelocity(weapon, ShootingVelocity);
        }

        public void ChangeDirection(float angle)
        {
            this.ShootingAngle = angle;

            CalculateVelocity();
        }

		public void setPower(float power) {
			this.ShootingPower = power;
			CalculateVelocity ();
		}

        public void ChangePower(Direction direction)
        {
            this.ShootingPower = Direction.Up == direction
                ? this.ShootingPower + 2 >= 100 ? 100 : this.ShootingPower + 2
                : this.ShootingPower - 2 <= 0 ? 2 : this.ShootingPower - 2;

            CalculateVelocity();
        }

        private void SetWeaponVelocity(GameObject weaponObject, Vector3 velocity)
        {
            weaponObject.GetComponent<Rigidbody2D>().velocity = velocity;
        }

        private GameObject CreateWeapon(string weaponID, Vector3 position)
        {
            var weapon = Instantiate(Util.LoadWeapon(weaponID), position, Quaternion.identity) as GameObject;
            // Asignamos el tag de Arma a la bala
            weapon.tag = "Weapon";
            // Añadimos una capa que será la misma que el totem. 
            // Utilizando las capas de colisión evitaremos que una bala disparada por el propio jugador le afecten

            if (totem.gameObject.layer== Global.Capas.totemsPrimerJugador)
                weapon.layer = Global.Capas.balaPrimerJugador;
            else if (totem.gameObject.layer == Global.Capas.totemsSegundoJugador)
                weapon.layer = Global.Capas.balaSegundoJugador;

            return weapon;
        }

        private void CalculateVelocity()
        {
            var radian = this.ShootingAngle;
            var velocity = new Vector3((float)Math.Cos(radian), (float)Math.Sin(radian));
            this.ShootingVelocity = new Vector3(velocity.x * ((this.ShootingPower*4)/10), velocity.y * (this.ShootingPower*4/10));
        }

        private Vector3 CalculateFirePoint(Vector3 velocity)
        {
            var degree = Mathf.Atan2((transform.position.x+velocity.x) - transform.position.x, (transform.position.y+velocity.y) - transform.position.y) * 180 / Mathf.PI;
            var fittedDegree = degree > 90 ? Mathf.Abs((degree - 450) % -360) : Mathf.Abs((degree - 90)%360);
            return AngleToPoint(fittedDegree);
        }

        private Vector3 AngleToPoint(float degree)
        {
            var resultY = transform.position.y + (transform.localScale.y) * Mathf.Sin(degree * (Mathf.PI / 180));
            var resultX = transform.position.x + (transform.localScale.x) * Mathf.Cos(degree * (Mathf.PI / 180));
            return new Vector3(resultX, resultY, 0);
        }
    }
}

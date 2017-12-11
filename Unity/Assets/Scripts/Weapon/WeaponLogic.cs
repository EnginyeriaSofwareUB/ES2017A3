using System;
using Assets.Scripts.Player;
using UnityEngine;
using Assets.Scripts.Environment;

namespace Assets.Scripts.Weapon
{
    public class WeaponLogic : MonoBehaviour
    {
        public String weaponType = "Semtex";

        private PlayerBehaviour Player { get; set; }

        // Variable que tiene toda la estructura Totem
        private Totem totem;

        private float ShootingAngle = 45;
        private float ShootingPower = 100;

        private Vector3 ShootingVelocity;
        private Vector3 ShotStartingPoint;

        private MovimientoController mov;

        void Start()
        {
            this.Player = GetComponent<PlayerBehaviour>();
            this.totem = GetComponent<Totem>();
            this.mov = GetComponent<MovimientoController>();
            CalculateVelocity();
        }

        void Update()
        {
            
        }


        public void Shoot()
        {
			ShotStartingPoint = CalculateFirePoint(ShootingVelocity);

            var weapon = CreateWeapon("Weapons/" + weaponType, ShotStartingPoint);
            //var weapon = CreateWeapon("Weapons/Missile", ShotStartingPoint);

            SetWeaponVelocity(weapon, ShootingVelocity);

            this.mov.setShoot(true);
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
            GameObject tmp = Util.LoadWeapon(weaponID) as GameObject;
            Quaternion t = tmp.transform.rotation;
            if (weaponType.Equals("Missile"))
                 t = new Quaternion(0.7f, 0, ShootingAngle > 1.0f ? 0.7f : -0.7f, 0);
            var weapon = Instantiate(tmp, position, t) as GameObject;
            //var weapon = Instantiate(tmp, position, tmp.transform.rotation) as GameObject;
            // Asignamos el tag de Arma a la bala
            weapon.tag = "Weapon";
            weapon.AddComponent<CheckIsVisible>();
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
			var resultY = transform.position.y;
			var resultX = transform.position.x;
			return new Vector3(resultX, resultY, 0);
        }
    }
}

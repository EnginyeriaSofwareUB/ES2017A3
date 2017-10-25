using System;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public float MovementSpeed;
        public float JumpForce;

        private WeaponLogic WeaponLogic { get; set; }
        private MovimientoController mov { get; set; }

		private StateHolder stateHolder;


        void Start ()
        {
            this.WeaponLogic = GetComponent<WeaponLogic>();
            this.mov = GetComponent<MovimientoController>();
			this.stateHolder = GameObject.FindGameObjectWithTag ("GameController").GetComponent<StateHolder>();

			//Debug.Log("PlayerBehaviour :: stateHolder =  " + stateHolder.is + ".");
        }

        private void Update()
        {
            if (this.mov.PuedeMoverse)
            {
                //MovePlayer();
                WeaponDirection();
                //WeaponForce();
                Shoot();
            }
        }

        private void Shoot()
        {
			if (Input.GetMouseButtonDown(0) && stateHolder.isPlaying())
            {
                WeaponLogic.Shoot();
            }
        }

        private void WeaponDirection()
        {
            if (Math.Abs(Input.GetAxis("Vertical")) > 0)
            {
                var direction = Input.GetAxis("Vertical") > 0 ? Direction.Up : Direction.Down;
                WeaponLogic.ChangeDirection(direction);
            }
        }

        private void WeaponForce()
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
            {
                var direction = Input.GetKeyDown(KeyCode.R) ? Direction.Up : Direction.Down;
                WeaponLogic.ChangePower(direction);
            }
        }

        private void MovePlayer()
        {
            transform.position += new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * MovementSpeed, 0, 0);
        }

        
    }
}
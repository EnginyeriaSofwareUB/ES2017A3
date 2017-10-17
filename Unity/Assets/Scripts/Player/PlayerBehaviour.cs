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
        private Vector3 startMousePosition;
        private float shootingAngle;
        private LineRenderer lineaRenderizar;
        void Start()
        {
            
            this.WeaponLogic = GetComponent<WeaponLogic>();
            this.mov = GetComponent<MovimientoController>();
            lineaRenderizar = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (this.mov.PuedeMoverse)
            {
                //MovePlayer();
                //WeaponDirection();
                WeaponForce();
                Shoot();
            }
        }

      
        private void Shoot()
        {

            if (Input.GetMouseButtonDown(0))
            {
                startMousePosition = Input.mousePosition;

            }

            if (Input.GetMouseButton(0))
            {
                Vector3 mouseDelta = Input.mousePosition - startMousePosition;
                if (mouseDelta.sqrMagnitude < 0.1f)
                {
                    shootingAngle = getAngle(startMousePosition);// don't do tiny rotations.
                }
                else
                {
                    shootingAngle = getAngle(mouseDelta);
                }

                WeaponLogic.ChangeDirection(shootingAngle);
            }

            if (Input.GetMouseButtonUp(0))
            {
                WeaponLogic.ChangeDirection(shootingAngle);
                WeaponLogic.Shoot();
            }
        }

        private void WeaponDirection()
        {
            if (Math.Abs(Input.GetAxis("Vertical")) > 0)
            {
                var direction = Input.GetAxis("Vertical") > 0 ? Direction.Up : Direction.Down;
                WeaponLogic.ChangeDirection(shootingAngle);
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


        private float getAngle(Vector3 position)
        {
            position.z = (transform.position.z - Camera.main.transform.position.z);
            position = Camera.main.ScreenToWorldPoint(position);
            position = position - transform.position;
            float angle = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360;
            return angle;
        }
        
    }
}
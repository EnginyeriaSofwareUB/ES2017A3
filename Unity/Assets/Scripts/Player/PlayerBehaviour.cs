using System;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public float MovementSpeed;
        public float JumpForce;

        private Vector3 startMousePosition;
        private float shootingAngle;
        private LineRenderer lineaRenderizar;
        private WeaponLogic WeaponLogic { get; set; }
        private MovimientoController mov { get; set; }

        private StateHolder stateHolder;

        void Start()
        {
            
            this.WeaponLogic = GetComponent<WeaponLogic>();
            this.mov = GetComponent<MovimientoController>();
            initLine();
            this.stateHolder = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateHolder>();
        }

        private void Update()
        {
            if (this.mov.PuedeMoverse)
            {
                //WeaponDirection();
                //WeaponForce();
                Shoot();
                drawLine();
            }
            else
            {
                deleteLine();
            }
        }

        private void initLine()
        {
            lineaRenderizar = (new GameObject("flecha")).AddComponent<LineRenderer>();
            lineaRenderizar.startWidth=0.1f;
            lineaRenderizar.endWidth = 0.1f;
            lineaRenderizar.material.color = Color.grey;
            //lineaRenderizar.sortingLayerName = "arrowLine";
            lineaRenderizar.sortingOrder = 0;
        }

        private void drawLine()
        {
            lineaRenderizar.gameObject.SetActive(true);
            lineaRenderizar.positionCount = 2;
            lineaRenderizar.SetPosition(0, transform.position);
            startMousePosition = Input.mousePosition;
            startMousePosition.z = (transform.position.z - Camera.main.transform.position.z);
            startMousePosition = Camera.main.ScreenToWorldPoint(startMousePosition);
            lineaRenderizar.SetPosition(1, startMousePosition);
        }

        private void deleteLine()
        {
            lineaRenderizar.positionCount = 0;
        }
        private void Shoot()
        {

            if (Input.GetMouseButtonDown(0) && stateHolder.isPlaying())

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
               // WeaponLogic.ChangeDirection(shootingAngle);
                //WeaponLogic.ChangeDirection(direction);
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

        private float getAngle(Vector3 position)
        {
            /*
            position.z = (transform.position.z - Camera.main.transform.position.z);
            position = Camera.main.ScreenToWorldPoint(position);
            float diffY = position.y - transform.position.y;
            float diffX = position.x - transform.position.x;
            float angle = Mathf.Atan(diffY / diffX);
            return angle;*/

            position.z = (transform.position.z - Camera.main.transform.position.z);
            position = Camera.main.ScreenToWorldPoint(position);
            position = position - transform.position;
            print ("mouse"+position);
            print("personaje"+transform.position);
            float angle = Mathf.Atan2(position.y,position.x) ;
            print (angle*Mathf.Rad2Deg);
            return angle;
        }
    }
}
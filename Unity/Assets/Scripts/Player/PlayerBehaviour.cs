using System;
using Assets.Scripts.Weapon;
using UnityEngine;
using Assets.Scripts.Environment;

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
        private Totem totemController;
        private StateHolder stateHolder;

        void Start()
        {
            
            this.WeaponLogic = GetComponent<WeaponLogic>();
            this.mov = GetComponent<MovimientoController>();
            this.totemController = GetComponent<Totem>();
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
			GameObject flecha = new GameObject ("flecha");
            lineaRenderizar = flecha.AddComponent<LineRenderer>();
            lineaRenderizar.startWidth=0.1f;
            lineaRenderizar.endWidth = 0.1f;
            lineaRenderizar.material = new Material(Shader.Find("Sprites/Default"));

 
            setGradienteLinea(lineaRenderizar);
         
            //lineaRenderizar.sortingLayerName = "arrowLine";
            lineaRenderizar.sortingOrder = 0;


			lineaRenderizar.widthCurve = new AnimationCurve(
				new Keyframe(0, 0.4f)
				, new Keyframe(0.9f, 0.4f) // neck of arrow
				, new Keyframe(0.91f, 1f)  // max width of arrow head
				, new Keyframe(1, 0f));  // tip of arrow
			
        }
        private void setGradienteLinea(LineRenderer line)
        {
            float alpha = 1.0f;
            Gradient gradient = new Gradient();
            if (this.gameObject.layer == Global.Capas.totemsPrimerJugador)
            {
                gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.red, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
                );
            }
            else
            {
                gradient.SetKeys(
              new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.red, 1.0f) },
              new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
              );
            }
            
            line.colorGradient = gradient;
        }
        private void drawLine()
        {
            lineaRenderizar.gameObject.SetActive(true);
            lineaRenderizar.positionCount = 2;
           // lineaRenderizar.SetPosition(0, transform.position);
            startMousePosition = Input.mousePosition;
            startMousePosition.z = (transform.position.z - Camera.main.transform.position.z);
            startMousePosition = Camera.main.ScreenToWorldPoint(startMousePosition);
            //lineaRenderizar.SetPosition(1, startMousePosition);

			lineaRenderizar.SetPositions(new Vector3[] {
				transform.position
				, Vector3.Lerp(transform.position, startMousePosition, 0.9f)
				, Vector3.Lerp(transform.position, startMousePosition, 0.91f)
				, startMousePosition });

			float distanceX = Mathf.Abs (lineaRenderizar.GetPosition (1).x - lineaRenderizar.GetPosition (0).x);
			float distanceY = Mathf.Abs (lineaRenderizar.GetPosition (1).y - lineaRenderizar.GetPosition (0).y);


			float potenceDistance = Mathf.Sqrt(Mathf.Pow(distanceX, 2)  +Mathf.Pow(distanceY, 2));

			WeaponLogic.setPower (potenceDistance * 10 + 1);
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
            //print ("mouse"+position);
            //print("personaje"+transform.position);
            float angle = Mathf.Atan2(position.y,position.x) ;
            //print (angle*Mathf.Rad2Deg);
            return angle;
        }
    }
}
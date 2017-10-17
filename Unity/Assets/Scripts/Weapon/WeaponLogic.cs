using System;
using System.Collections.Generic;
using Assets.Scripts.Player;
using Assets.Scripts.Weapons;
using UnityEngine;
using Assets.Scripts.Environment;

namespace Assets.Scripts.Weapon
{
    public class WeaponLogic : MonoBehaviour
    {
        private PlayerBehaviour Player { get; set; }

        private float ShootingAngle = 45;
        private float ShootingPower = 100;

        private Vector3 ShootingVelocity;
        private Vector3 ShotStartingPoint;

        void Start()
        {
            this.Player = GetComponent<PlayerBehaviour>();
            CalculateVelocity();
        }

        void Update()
        {
            ShotStartingPoint = CalculateFirePoint(ShootingVelocity);
            CheckTrajectory(ShotStartingPoint, ShootingVelocity, Physics2D.gravity);
        }


        public void Shoot()
        {
            var weapon = CreateWeapon("Armas/Bomba", ShotStartingPoint);

            SetWeaponVelocity(weapon, ShootingVelocity);
        }

        public void ChangeDirection(float angle)
        {
            this.ShootingAngle = angle;

            CalculateVelocity();
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
            weapon.GetComponent<BombLogic>().Player = this.gameObject;
            return weapon;
        }

        private void CalculateVelocity()
        {
            var radian = (Mathf.PI / 180) * this.ShootingAngle;
            var velocity = new Vector3((float)Math.Cos(radian), (float)Math.Sin(radian));
            this.ShootingVelocity = new Vector3(velocity.x * ((this.ShootingPower*4)/10), velocity.y * (this.ShootingPower*2/10));
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

        private void CheckTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity, int maxSteps = 100)
        {
            Vector3 position = initialPosition;
            Vector3 velocity = initialVelocity;
            var pathPositions = new List<Vector3> { position };

            float magnitude = 1.0f / velocity.magnitude;
            for (int i = 0; i < maxSteps; ++i)
            {
                if (position.y < Global.TerrainNullPoint.y || position.x < Global.TerrainNullPoint.x || position.x > Global.TerrainEndPoint.x)
                    break;

                var oldPosition = new Vector3(position.x, position.y, position.z);
                position += velocity * magnitude + 0.5f * gravity * magnitude * magnitude;
                velocity += gravity * magnitude;

                pathPositions.Add(position);

                if (CheckTrajectoryCollision(oldPosition, position, pathPositions))
                    break;
            }

            //BuildTrajectoryLine(pathPositions);
        }

        private bool CheckTrajectoryCollision(Vector3 oldPosition, Vector3 newPosition, List<Vector3> positions)
        {
            bool returntype = false;
            var hasHitSomething = Physics2D.Linecast(oldPosition, newPosition);
            if (hasHitSomething)
                if (hasHitSomething.transform.gameObject != Player.gameObject)
                {
                    newPosition = hasHitSomething.transform.position;
                    returntype = true;
                }

            positions.Add(newPosition);

            return returntype;
        }


        private void BuildTrajectoryLine(List<Vector3> positions)
        {
            /*LineRenderer lineRenderer = Player.GetComponent<LineRenderer>();
            lineRenderer.SetVertexCount(positions.Count);
            for (var i = 0; i < positions.Count; ++i)
                lineRenderer.SetPosition(i, positions[i]);
            */
        }
    }
}

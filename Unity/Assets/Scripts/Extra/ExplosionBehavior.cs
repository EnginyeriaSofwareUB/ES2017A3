using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class ExplosionBehavior : MonoBehaviour
    {
        public List<CirclePoint> ExplosionPoints;
        public List<GameObject> TerrainObjects;

        public float ExplosionRadius;       // World pixels
        private float _explosionRadius;     // Screen pixels

        private Vector3 _screenPosition;
        private Vector3 _worldPosition;

        private bool _explosionInitialized = false;
        private bool _handlingExplosion = false;

        protected void StartExplosion()
        {
            Initialize();
            CalculateScreenRadius();
            this._explosionInitialized = true;
        }

        protected bool UpdateExplosion()
        {
            if ((!this._handlingExplosion && this._explosionInitialized) || transform.position.y < -10)
            {
                HandleExplosion();
                return true;
            }
            return false;
        }

        private void Initialize()
        {
            this._screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            this._worldPosition = transform.position;
        }

        private void HandleExplosion()
        {
            this._handlingExplosion = true;

            //CreateExplosionPoints();
            //GetExplodedTerrainObjects();
            InitiateTerrainUpdate();
        }

        private void CalculateScreenRadius()
        {
            var tempVector = new Vector3(_worldPosition.x, (_worldPosition.y + this.ExplosionRadius));
            var tempScreenVector = Camera.main.WorldToScreenPoint(tempVector);
            this._explosionRadius = tempScreenVector.y - this._screenPosition.y;
        }

        private void CreateExplosionPoints()
        {
            var tempList = new List<CirclePoint>();
            for (int i = 0; i < 360; i++)
            {
                var resultY = _screenPosition.y + this._explosionRadius * Mathf.Sin(i * (Mathf.PI / 180));
                var resultX = _screenPosition.x + this._explosionRadius * Mathf.Cos(i * (Mathf.PI / 180));
                var result = Camera.main.ScreenToWorldPoint(new Vector3(resultX, resultY, 0));
                tempList.Add(new CirclePoint(result, degree: i));
            }
            this.ExplosionPoints = tempList;
        }

        private void GetExplodedTerrainObjects()
        {
            var tempList = new List<GameObject>();
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, this.ExplosionRadius);
            foreach (var obj in hitColliders)
                if(obj.CompareTag("TerrainObject"))
                    tempList.Add(obj.gameObject);

            this.TerrainObjects = tempList;
        }

        private void InitiateTerrainUpdate()
        {
            foreach (var terrainObject in this.TerrainObjects)
                terrainObject.SendMessage("UpdateTerrain", this.ExplosionPoints);
        }
    }
}

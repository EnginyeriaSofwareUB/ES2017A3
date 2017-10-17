using System;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class TerrainGenerator : MonoBehaviour
    {
        public GameObject TerrainObject;

        public float TerrainSizeX;
        public float TerrainSizeY;

        private Vector3 TerrainSize {  get {  return Camera.main.ScreenToWorldPoint(new Vector3(this.TerrainSizeX, this.TerrainSizeY));} }
        private Vector3 _currentTerrainSize = new Vector3(0, 0);

        public float CurrentTerrainSizeRatio
        {
            get
            {
                if (!(this._currentTerrainSizeRatioHolder > 0))
                {
                    var result = Camera.main.WorldToScreenPoint(new Vector3(TerrainObject.gameObject.transform.localScale.x * 2, TerrainObject.gameObject.transform.localScale.y, TerrainObject.gameObject.transform.localScale.z)).x;
                    this._currentTerrainSizeRatioHolder = result - Camera.main.WorldToScreenPoint(TerrainObject.gameObject.transform.localScale).x;
                }
                return this._currentTerrainSizeRatioHolder;
            }
        }
        private float _currentTerrainSizeRatioHolder;
        

        void Start()
        {
            Global.TerrainNullPoint = this.gameObject.transform.position;
            Global.TerrainEndPoint = TerrainSize*this.CurrentTerrainSizeRatio;
            Util.StopScene();
        }
	
        void Update ()
        {
            if(!Global.IsTerrainGenerated)
                GenerateTerrainOnUpdate();
        }

        public void GenerateSpecificTerrainObject(Vector3 pointFrom, Vector3 pointTo)
        {
            TerrainObject.gameObject.transform.localScale = new Vector3(TerrainObject.gameObject.transform.localScale.x, pointTo.y-pointFrom.y);
            var newTerrainObject = Instantiate(TerrainObject, new Vector3(pointFrom.x, 0 + (TerrainObject.gameObject.transform.localScale.y / 2) + pointFrom.y, this.transform.position.z), Quaternion.identity) as GameObject;
            Util.AssignObjectParent("Terrain", newTerrainObject);
        }

        private void GenerateTerrainOnUpdate()
        {
            for (int i = 0; i < 50; i++)
            {
                if (!(Camera.main.WorldToScreenPoint(this._currentTerrainSize).x < (TerrainSizeX * this.CurrentTerrainSizeRatio)))
                {
                    Global.IsTerrainGenerated = true;
                    Util.StartScene();
                    return;
                }

                TerrainObject.gameObject.transform.localScale = AssignTerrainObjectSize();
                this._currentTerrainSize.x += TerrainObject.gameObject.transform.localScale.x;
                var newTerrainObject = Instantiate(TerrainObject, new Vector3(this._currentTerrainSize.x, 0 + TerrainObject.gameObject.transform.localScale.y / 2, this.transform.position.z), Quaternion.identity) as GameObject;
                Util.AssignObjectParent("Terrain", newTerrainObject);
            }
        }

        private Vector3 AssignTerrainObjectSize()
        {
            var x = TerrainObject.gameObject.transform.localScale.x;
            var y = (this.TerrainSize.y / 2) *
                    Mathf.PerlinNoise(_currentTerrainSize.x * 0.01f,
                        Mathf.Sqrt(_currentTerrainSize.x < 0 ? _currentTerrainSize.x*-1 : _currentTerrainSize.x));

            return new Vector3(x, y);
        }
    }
}

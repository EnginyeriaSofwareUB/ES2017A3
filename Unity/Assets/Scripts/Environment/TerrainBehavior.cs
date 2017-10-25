using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class TerrainBehavior : MonoBehaviour
    {
        private CirclePoint closestPoint;
        private CirclePoint closestPointAbove;
        private CirclePoint closestPointAboveTopPoint;

        public void UpdateTerrain(List<CirclePoint> explosionPoints)
        {
			Debug.LogError ("I'm in Update Terrain");
            SetTerrainSize(explosionPoints);
        }

        private void SetTerrainSize(List<CirclePoint> explosionPoints)
        {
            bool isEmptyBelow;
            bool isEmptyAbove;
            float minimalNoise = 0.09f;
            SetClosestPoint(explosionPoints, out isEmptyBelow, out isEmptyAbove);

            if (isEmptyBelow)
            {
                var matterToRemove = closestPoint.Vector3.y - (transform.position.y - (transform.localScale.y/2));
                var newScale = transform.localScale.y - (transform.localScale.y - matterToRemove);

                if (newScale > minimalNoise)
                {
					Debug.LogError ("I'm in IF newScale");
					//Destroy (this.gameObject);
					transform.position = new Vector3 (5, 5, 5);
                    //var newPosition = transform.position.y - (transform.localScale.y / 2) + newScale / 2;
                    //transform.localScale = new Vector3(transform.localScale.x, newScale, transform.localScale.z);
                    //transform.position = new Vector3(transform.position.x, newPosition, transform.position.z);
                }
                else
                    Destroy(this.gameObject);
            }
            

			//if (!isEmptyAbove && closestPointAboveTopPoint.Vector3.y - closestPointAbove.Vector3.y > minimalNoise) {
			//	GameObject.FindGameObjectWithTag ("Terrain").GetComponent<TerrainGenerator> ().GenerateSpecificTerrainObject (closestPointAbove.Vector3, closestPointAboveTopPoint.Vector3);
			//}

            if (!isEmptyBelow)
                Destroy(this.gameObject);
        }

        private void SetClosestPoint(List<CirclePoint> points, out bool isEmptyBelow, out bool isEmptyAbove)
        {
            isEmptyAbove = true;
            float topPoint = transform.position.y + (transform.localScale.y/2);
            float bottomPoint = transform.position.y - (transform.localScale.y/2);

            closestPoint = new CirclePoint(new Vector3(), 10000);

            foreach (var obj in points.GetRange(180, 180))
            {
                if (obj.Degree <= 180)
                    continue;

                var result = Mathf.Abs(transform.position.x - obj.Vector3.x);
                if (result <= closestPoint.Distance)
                {
                    closestPoint = new CirclePoint(obj.Vector3, result);
                    var index = obj.Degree == 0 ? 0 : obj.Degree == 180 ? 180 : 360 - obj.Degree;

                    if (topPoint > points[index].Vector3.y)
                    {
                        closestPointAbove = new CirclePoint(new Vector3(transform.position.x, points[index].Vector3.y));
                        closestPointAboveTopPoint = new CirclePoint(new Vector3(transform.position.x, topPoint));
                        isEmptyAbove = false;
                    }    
                }
            }

            isEmptyBelow = !(closestPoint.Vector3.y <= bottomPoint);
        }
    }
}
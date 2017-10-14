using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class ExplosionSimulation : MonoBehaviour
    {
        public GameObject ExplosionObject;

        void Update () {
            MouseClickExplosion();
        }

        private void MouseClickExplosion()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClearCurrentExplosions();
                var screenPosition = Input.mousePosition;
                var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

                var newExplosionObject = Instantiate(this.ExplosionObject, new Vector3(worldPosition.x, worldPosition.y, this.transform.position.z), Quaternion.identity) as GameObject;
                Util.AssignObjectParent("Explosions", newExplosionObject);
            }
        }

        private void ClearCurrentExplosions()
        {
            var objects = GameObject.FindGameObjectsWithTag("ExplosionObject");
            foreach(var obj in objects)
                Destroy(obj);
        }
    }
}

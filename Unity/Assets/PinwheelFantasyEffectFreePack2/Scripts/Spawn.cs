using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public GameObject objectToSpawn;
    public Transform spawnPoint;
    public KeyCode spawnKey = KeyCode.Space;

    public void Update()
    {
        if (objectToSpawn == null)
            return;
        if (spawnPoint == null)
            return;
        if (Input.GetKeyDown(spawnKey))
        {
            GameObject g = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity) as GameObject;
            float forceDirX, forceDirY, forceDirZ;
            forceDirX = Random.Range(-10, 10);
            forceDirY = Random.Range(5, 15);
            forceDirZ = Random.Range(-10, 10);
            g.GetComponent<Rigidbody>().AddForce(forceDirX*100, forceDirY*100, forceDirZ*100);
            Destroy(g, 10);
        }
    }

    public void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = (int)(Screen.height * 0.1f);
        style.normal.textColor = Color.white;
        GUILayout.Label(string.Format("Press {0} to spawn the fireball!", spawnKey.ToString()));
    }
}

using UnityEngine;
using System.Collections;

public class Cohete : MonoBehaviour
{
    private GameObject efectoCohete;
    // Use this for initialization
    void Start()
    {
         efectoCohete = Instantiate(Resources.Load("Fx_OilTrailHIGH_Root"), this.transform.position, Quaternion.identity) as GameObject;
        efectoCohete.transform.parent = this.transform;


        transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.collider.tag;
        if (tag == "TerrainObject")
        {
            Destroy(gameObject);
            Debug.Log("Colision cohete");
            Destroy(efectoCohete);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}

using UnityEngine;
using System.Collections;

public class Teletransporte : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameObject efectoCohete = Instantiate(Resources.Load("ErekiBall2"), this.transform.position, Quaternion.identity) as GameObject;
        efectoCohete.transform.parent = this.transform;
        StartCoroutine(Example());

    }
  
    IEnumerator Example()
    {

        print(Time.time);
        yield return new WaitForSeconds(3);
        GameManager.Instance.totemActual.gameObject.transform.position += new Vector3(3, 0, 0);
        //this.gameObject.transform.parent.gameObject.transform.position += new Vector3(3, 0, 0);
        Destroy(gameObject);

    }
    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    public Rigidbody rgbd;
    public GameObject fieryParticle;
    public GameObject smokeParticle;
    public GameObject explosionParticle;

    private void OnCollisionEnter(Collision collision)
    {
        fieryParticle.SetActive(false);
        smokeParticle.SetActive(false);
        explosionParticle.SetActive(true);
        rgbd.constraints = RigidbodyConstraints.FreezeAll;
    }
}

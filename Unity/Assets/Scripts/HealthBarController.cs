using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {
	private GameObject totemObject;
    private GameObject currentHealthBar;
	private Totem totem;
	private float maxHealth;
    private float currentHealth;

	// Use this for initialization
	void Start () {
		totemObject = transform.parent.gameObject.transform.parent.gameObject;
		totem = totemObject.GetComponent<Totem> ();
        maxHealth = totem.getMaxHealth();
        currentHealth = totem.getCurrentHealth();
        currentHealthBar = transform.GetChild(0).gameObject;

    }

    void UpdateHealthBar()
    {
        currentHealthBar.transform.localScale = new Vector3(currentHealth/maxHealth , 1, 1);
    }

    // Update is called once per frame
    void Update () {
		this.currentHealth = totem.getCurrentHealth ();
        UpdateHealthBar();
	}

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    private float maxHorizontalValue;
    private float minHorizontalValue;

    // Use this for initialization
    void Start()
    {
        Component[] childrenPollygonColliders = GetComponentsInChildren<PolygonCollider2D>();
        float min = float.MaxValue;
        float max = float.MinValue;
        float value;
        foreach (Component component in childrenPollygonColliders){
            value = component.GetComponent<PolygonCollider2D>().bounds.min.x;
            if(value < min)
            {
                min = value;
            }
            value = component.GetComponent<PolygonCollider2D>().bounds.max.x;

            if(value > max)
            {
                max = value;
            }

        }
        //També es podria aprofitar aquest mateix loop per trobar el maxim i minim valor de l'eix Y
        this.minHorizontalValue = min;
        this.maxHorizontalValue = max;
        /*print("Max children value: " + max);
        print("Min children value: " + min);
        print("Min :" + GetComponent<BoxCollider2D>().bounds.min);
        print("Max :" + GetComponent<BoxCollider2D>().bounds.max);*/
    }

    public float getMaxHorizontalValue()
    {
        return this.maxHorizontalValue;
    }

    public float getMinHorizontalValue()
    {
        return this.minHorizontalValue;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int limitePasos;

    public int pasos;

    // Use this for initialization
    void Start()
    {
        pasos = 0;
        limitePasos = 4;
    }

    // Update is called once per frame
    void Update()
    {
        bool right = Input.GetKeyDown("right");
        bool left = Input.GetKeyDown("left");

        if (right || left)
        {
            print("Movimiento");
            pasos += right ? 1 : -1;
            bool parado = verificarPasos();
            if (!parado)
            {
                if (right)
                {
                    //Movimiento derecha
                }
                else
                {
                    //Movimiento izquierda
                }
            }
            else
            {
                //Parado
                pasos += right ? -1 : 1;
                print("Personaje parado");
            }
        }

    }

    bool verificarPasos()
    {
        return Math.Abs(pasos) > limitePasos;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T>: List<T> {

    // Use this for initialization

    public PriorityQueue() : base() { }


    public T Peek()
    {
        return this[0];
    }

    public T Poll()
    {
        //Buscamos el elemento con prioridad
        T obj = this[0];

        //Lo eliminamos de la posición, en este caso como aún no hay algorismo de prioridad cogeremos el 0 (cola normal)
        RemoveAt(0);

        //Lo insertamos al final de la cola
        Add(obj);

        //Lo retornamos
        return obj;
    }
}

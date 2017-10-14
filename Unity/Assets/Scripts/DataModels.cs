using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets
{
    [Serializable]
    public class CirclePoint
    {
        public Vector3 Vector3;
        public float Distance;
        public int Degree;

        public CirclePoint(Vector3 vector, float distance = default(float), int degree = default(int))
        {
            this.Vector3 = vector;
            this.Distance = distance;
            this.Degree = degree;
        }
    }

    public enum Direction
    {
        Up, Down
    }
}

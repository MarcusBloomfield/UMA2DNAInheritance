using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace marcus
{
    public static class MathTools 
    {
        public static float map(float value, float currentMin, float currentMax, float newMin, float newMax)
        {
            return (value - currentMin) / (currentMax - currentMin) * (newMax - newMin) + newMin;
        }
    }
}

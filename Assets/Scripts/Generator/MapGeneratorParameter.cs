using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorParameter : MonoBehaviour {

    public MapGeneratorParameter(int minValue, int maxValue, int value)
    {
        MinValue = minValue;
        MaxValue = maxValue;
        Value = value;
    }

    private int myValue;

    public int Value
    {
        get
        {
            return myValue;
        }
        set
        {
            if ( (value >= MinValue) && (value <= MaxValue) )
            {
                myValue = value;
            }
        }
    }

    public int MinValue { get; private set; }
    public int MaxValue { get; private set; }
}

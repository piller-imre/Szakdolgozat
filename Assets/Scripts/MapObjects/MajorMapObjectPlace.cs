using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorMapObjectPlace : MonoBehaviour {

    public MajorMapObjectPlaces Place;
    private GameObject majorObject;

    public GameObject MajorObject
    {
        get
        {
            return majorObject;
        }
        set
        {
            majorObject = value;
            RotateObject();
        }
    }


    public MajorMapObjectPlace(MajorMapObjectPlaces place, GameObject majorObject)
    {
        Place = place;
        MajorObject = majorObject;
    }


    private void RotateObject()
    {
        if (majorObject != null)
        {
            if (Place > 0)
            {
                majorObject.transform.Rotate(0, (int)Place * 60.0f, 0);
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class MajorMapObject : BaseMapObject {

    public MajorMapObjectPlace[] MajorObjects = new MajorMapObjectPlace[7];

    public int Temperature;
    private int waterLevel = -1;

    public int WaterLevel
    {
        get
        {
            return waterLevel;
        }
        set
        {
            waterLevel = value;

            foreach (var item in MajorObjects)
            {
                if (item != null)
                {
                    if (item.MajorObject.GetComponent<Tree>() != null)
                    {
                        item.MajorObject.GetComponent<Tree>().WaterLevel = value;
                    }
                }
            }
        }
    }

    public MajorMapObjectTypes Type;

    public void Initializer(ObjectTypes objectType, SelectionInfoTypes selectionInfo, Vector3 worldPosition, Vector3 offsetPosition, int temperature, int waterLevel, MajorMapObjectTypes type)
    {
        Temperature = temperature;
        WaterLevel = waterLevel;
        Type = type;

        base.Initializer("MajorMapObject(" + offsetPosition.x + ", " + offsetPosition.z + ")", objectType, selectionInfo, worldPosition, offsetPosition);

        for (int i = -1; i < MajorObjects.Length - 1; i++)
        {
            MajorObjects[i + 1] = new MajorMapObjectPlace((MajorMapObjectPlaces)i, null);
        }

        GetNewMajorObjects(); 
    }

    private void GetNewMajorObjects()
    {
        List<int> OpenPlaces = new List<int>();

        foreach (var item in MajorObjects)
        {
            OpenPlaces.Add((int)item.Place);
        }

        int maxNumOfObjects = Random.Range(1, 8);
        int numOfObjects = 0;

        while (numOfObjects < maxNumOfObjects)
        {
            int index = Random.Range(0, OpenPlaces.Count);

            GameObject actualPlaceObject = null;

            if (Type == MajorMapObjectTypes.Trees)
            {
                actualPlaceObject = MajorMapObjectManager.Instance.GetTree(OpenPlaces[index] == -1);
                actualPlaceObject.GetComponent<Tree>().Initializer(ObjectTypes.MajorObject, SelectionInfoTypes.NonSelectable, WorldPosition, OffsetPosition, Temperature, WaterLevel);
            }
            if (Type == MajorMapObjectTypes.City)
            {
                actualPlaceObject = MajorMapObjectManager.Instance.GetHouse(OpenPlaces[index] == -1);
                actualPlaceObject.GetComponent<House>().Initializer(ObjectTypes.MajorObject, SelectionInfoTypes.NonSelectable, WorldPosition, OffsetPosition, Temperature, WaterLevel);
            }

            foreach (var item in MajorObjects)
            {
                if ((int)item.Place == OpenPlaces[index])
                {
                    item.MajorObject = Instantiate(actualPlaceObject);

                    item.MajorObject.transform.position = WorldPosition;
                    item.MajorObject.transform.parent = this.transform;

                    numOfObjects++;
                    OpenPlaces.RemoveAt(index);
                    break;
                }
            }
        }
    }

    public void WeatherChanged()
    {
        if (Type == MajorMapObjectTypes.City)
        {
            foreach (var item in MajorObjects)
            {
                if (item != null)
                {
                    item.GetComponent<House>().SetHouseAppearance();
                }
            }       
        }
    }
}

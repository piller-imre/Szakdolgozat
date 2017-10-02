using System.Collections.Generic;
using UnityEngine;

public class MajorMapObject2 : BaseMapObject {

    public GameObject Center;
    public GameObject Side1;
    public GameObject Side2;
    public GameObject Side3;
    public GameObject Side4;
    public GameObject Side5;
    public GameObject Side6;

    public int Temperature;
    private int waterLevel = -1;

    public MajorMapObjectTypes Type;

    public int WaterLevel
    {
        get
        {
            return waterLevel;
        }
        set
        {
            waterLevel = value;
            if (Center != null)
            {
                Center.GetComponent<Tree>().WaterLevel = value;
            }
            if (Side1 != null)
            {
                Side1.GetComponent<Tree>().WaterLevel = value;
            }
            if (Side2 != null)
            {
                Side2.GetComponent<Tree>().WaterLevel = value;
            }
            if (Side3 != null)
            {
                Side3.GetComponent<Tree>().WaterLevel = value;
            }
            if (Side4 != null)
            {
                Side4.GetComponent<Tree>().WaterLevel = value;
            }
            if (Side5 != null)
            {
                Side5.GetComponent<Tree>().WaterLevel = value;
            }
            if (Side6 != null)
            {
                Side6.GetComponent<Tree>().WaterLevel = value;
            }
        }
    }

    public void Initializer(ObjectTypes objectType, SelectionInfoTypes selectionInfo, Vector3 worldPosition, Vector3 offsetPosition, int temperature, int waterLevel, MajorMapObjectTypes type)
    {
        Temperature = temperature;
        WaterLevel = waterLevel;
        Type = type;

        base.Initializer("MajorMapObject(" + offsetPosition.x + ", " + offsetPosition.z + ")", objectType, selectionInfo, worldPosition, offsetPosition);

        GetNewMajorObjects();
    }

    private void GetNewMajorObjects()
    {
        int maxNumOfObjects = 0;
        int numOfObjects = 0;

        if (Type == MajorMapObjectTypes.Trees)
        {
            maxNumOfObjects = Random.Range(1, 8);      
        }
        if (Type == MajorMapObjectTypes.City)
        {
            maxNumOfObjects = Random.Range(3, 8);
        }

        while (numOfObjects < maxNumOfObjects)
        {
            List<int> OpenSides = new List<int>();
            OpenSides.Add(0);
            OpenSides.Add(1);
            OpenSides.Add(2);
            OpenSides.Add(3);
            OpenSides.Add(4);
            OpenSides.Add(5);
            OpenSides.Add(6);

            int index = Random.Range(0, OpenSides.Count);

            switch (OpenSides[index])
            {
                case 0:
                    if (Type == MajorMapObjectTypes.Trees)
                    {
                        Center = MajorMapObjectManager.Instance.GetTree(true);
                        Center.GetComponent<Tree>().Initializer(ObjectTypes.MajorObject, SelectionInfoTypes.NonSelectable, WorldPosition, OffsetPosition, Temperature, WaterLevel);
                    }
                    if (Type == MajorMapObjectTypes.City)
                    {
                        Center = MajorMapObjectManager.Instance.GetHouse(true);
                        Center.GetComponent<House>().Initializer(ObjectTypes.MajorObject, SelectionInfoTypes.NonSelectable, WorldPosition, OffsetPosition, Temperature, WaterLevel);
                    }

                    Center = Instantiate(Center);
                    Center.transform.position = WorldPosition;
                    Center.transform.parent = this.transform;

                    numOfObjects++;
                    OpenSides.Remove(index);
                    break;
                case 1:
                    if (Type == MajorMapObjectTypes.Trees)
                    {
                        Side1 = MajorMapObjectManager.Instance.GetTree(false);
                        Side1.GetComponent<Tree>().Initializer(ObjectTypes.MajorObject, SelectionInfoTypes.NonSelectable, WorldPosition, OffsetPosition, Temperature, WaterLevel);
                    }
                    if (Type == MajorMapObjectTypes.City)
                    {
                        Side1 = MajorMapObjectManager.Instance.GetHouse(false);
                        Side1.GetComponent<House>().Initializer(ObjectTypes.MajorObject, SelectionInfoTypes.NonSelectable, WorldPosition, OffsetPosition, Temperature, WaterLevel);
                    }

                    Side1 = Instantiate(Side1);
                    Side1.transform.position = WorldPosition;
                    Side1.transform.parent = this.transform;

                    numOfObjects++;
                    OpenSides.Remove(index);
                    break;
                case 2:
                    if (Type == MajorMapObjectTypes.Trees)
                    {
                        Side2 = MajorMapObjectManager.Instance.GetTree(false);
                        Side2.GetComponent<Tree>().Initializer(ObjectTypes.MajorObject, SelectionInfoTypes.NonSelectable, WorldPosition, OffsetPosition, Temperature, WaterLevel);
                    }
                    if (Type == MajorMapObjectTypes.City)
                    {
                        Side2 = MajorMapObjectManager.Instance.GetHouse(false);
                        Side2.GetComponent<House>().Initializer(ObjectTypes.MajorObject, SelectionInfoTypes.NonSelectable, WorldPosition, OffsetPosition, Temperature, WaterLevel);
                    }

                    Side2 = Instantiate(Side2);
                    Side2.transform.position = WorldPosition;
                    Side2.transform.parent = this.transform;
                    Side2.transform.Rotate(0, 60, 0);

                    numOfObjects++;
                    OpenSides.Remove(index);
                    break;
                case 3:
                    if (Type == MajorMapObjectTypes.Trees)
                    {
                        Side3 = MajorMapObjectManager.Instance.GetTree(false);
                        Side3.GetComponent<Tree>().Initializer(ObjectTypes.MajorObject, SelectionInfoTypes.NonSelectable, WorldPosition, OffsetPosition, Temperature, WaterLevel);
                    }
                    if (Type == MajorMapObjectTypes.City)
                    {
                        Side3 = MajorMapObjectManager.Instance.GetHouse(false);
                        Side3.GetComponent<House>().Initializer(ObjectTypes.MajorObject, SelectionInfoTypes.NonSelectable, WorldPosition, OffsetPosition, Temperature, WaterLevel);
                    }

                    Side3 = Instantiate(Side3);
                    Side3.transform.position = WorldPosition;
                    Side3.transform.parent = this.transform;
                    Side3.transform.Rotate(0, 120, 0);

                    numOfObjects++;
                    OpenSides.Remove(index);
                    break;
                case 4:
                    if (Type == MajorMapObjectTypes.Trees)
                    {
                        Side4 = MajorMapObjectManager.Instance.GetTree(false);
                        Side4.GetComponent<Tree>().Initializer(ObjectTypes.MajorObject, SelectionInfoTypes.NonSelectable, WorldPosition, OffsetPosition, Temperature, WaterLevel);
                    }
                    if (Type == MajorMapObjectTypes.City)
                    {
                        Side4 = MajorMapObjectManager.Instance.GetHouse(false);
                        Side4.GetComponent<House>().Initializer(ObjectTypes.MajorObject, SelectionInfoTypes.NonSelectable, WorldPosition, OffsetPosition, Temperature, WaterLevel);
                    }

                    Side4 = Instantiate(Side4);
                    Side4.transform.position = WorldPosition;
                    Side4.transform.parent = this.transform;
                    Side4.transform.Rotate(0, 180, 0);

                    numOfObjects++;
                    OpenSides.Remove(index);
                    break;
                case 5:
                    if (Type == MajorMapObjectTypes.Trees)
                    {
                        Side5 = MajorMapObjectManager.Instance.GetTree(false);
                        Side5.GetComponent<Tree>().Initializer(ObjectTypes.MajorObject, SelectionInfoTypes.NonSelectable, WorldPosition, OffsetPosition, Temperature, WaterLevel);
                    }
                    if (Type == MajorMapObjectTypes.City)
                    {
                        Side5 = MajorMapObjectManager.Instance.GetHouse(false);
                        Side5.GetComponent<House>().Initializer(ObjectTypes.MajorObject, SelectionInfoTypes.NonSelectable, WorldPosition, OffsetPosition, Temperature, WaterLevel);
                    }

                    Side5 = Instantiate(Side5);
                    Side5.transform.position = WorldPosition;
                    Side5.transform.parent = this.transform;
                    Side5.transform.Rotate(0, 240, 0);

                    numOfObjects++;
                    OpenSides.Remove(index);
                    break;
                case 6:
                    if (Type == MajorMapObjectTypes.Trees)
                    {
                        Side6 = MajorMapObjectManager.Instance.GetTree(false);
                        Side6.GetComponent<Tree>().Initializer(ObjectTypes.MajorObject, SelectionInfoTypes.NonSelectable, WorldPosition, OffsetPosition, Temperature, WaterLevel);
                    }
                    if (Type == MajorMapObjectTypes.City)
                    {
                        Side6 = MajorMapObjectManager.Instance.GetHouse(false);
                        Side6.GetComponent<House>().Initializer(ObjectTypes.MajorObject, SelectionInfoTypes.NonSelectable, WorldPosition, OffsetPosition, Temperature, WaterLevel);
                    }

                    Side6 = Instantiate(Side6);
                    Side6.transform.position = WorldPosition;
                    Side6.transform.parent = this.transform;
                    Side6.transform.Rotate(0, 300, 0);

                    numOfObjects++;
                    OpenSides.Remove(index);
                    break;
            } 
        }
    }

    public void WeatherChanged()
    {
        if (Type == MajorMapObjectTypes.City)
        {
            if (Center != null)
            {
                Center.GetComponent<House>().SetHouseAppearance();
            }
            if (Side1 != null)
            {
                Side1.GetComponent<House>().SetHouseAppearance();
            }
            if (Side2 != null)
            {
                Side2.GetComponent<House>().SetHouseAppearance();
            }
            if (Side3 != null)
            {
                Side3.GetComponent<House>().SetHouseAppearance();
            }
            if (Side4 != null)
            {
                Side4.GetComponent<House>().SetHouseAppearance();
            }
            if (Side5 != null)
            {
                Side5.GetComponent<House>().SetHouseAppearance();
            }
            if (Side6 != null)
            {
                Side6.GetComponent<House>().SetHouseAppearance();
            }
        }
    }
}

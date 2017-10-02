using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectMaterials : MonoBehaviour {

    [Header("Mountain")]
    public Material ColdMountainMaterial;
    public Material NormalMountainMaterial;
    public Material HotMountainMaterial;

    [Header("Tree")]
    public Material[] GreenLeafMaterials;
    public Material[] FallColorMaterials;

    #region SingletonPattern
    public static MapObjectMaterials _instance;
    public static MapObjectMaterials Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MapObjectMaterials>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("MapObjectMaterials");
                    _instance = container.AddComponent<MapObjectMaterials>();
                }
            }
            return _instance;
        }
    }
    #endregion

    public Material GetMountainMaterial(int temperature, int waterLevel)
    {
        Material retMaterial = null;
        int dynamicTemp = Engine.Instance.DynamicTemp.Value;

        switch (Engine.Instance.ActualSeason)
        {
            case Seasons.Summer:
                dynamicTemp += Engine.Instance.SeasonTempModifier;
                break;
            case Seasons.Winter:
                dynamicTemp -= Engine.Instance.SeasonTempModifier;
                break;
        }

        if ((temperature + dynamicTemp >= Engine.Instance.ColdTemp) && (temperature + dynamicTemp - waterLevel * 3 <= Engine.Instance.HotTemp))
        {
            retMaterial = NormalMountainMaterial;
        }
        else if (temperature + dynamicTemp < Engine.Instance.ColdTemp)
        {
            retMaterial = ColdMountainMaterial;
        }
        else
        {
            retMaterial = HotMountainMaterial;
        }

        return retMaterial;
    }

}

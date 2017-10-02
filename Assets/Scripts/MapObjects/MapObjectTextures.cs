using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectTextures : MonoBehaviour {

    [Header("Hex")]
    public Texture[] GrassTextures;  
    public Texture MapEdgeTexture;
    public Texture[] WaterTextures;
    public Texture[] IceTextures;
    public Texture[] SnowTextures;
    public Texture[] SandTextures;

    [Header("House")]
    public Texture[] RoofTextures;
    public Texture RoofSnowTexture;

    #region SingletonPattern
    public static MapObjectTextures _instance;
    public static MapObjectTextures Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MapObjectTextures>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("MapObjectTextures");
                    _instance = container.AddComponent<MapObjectTextures>();
                }
            }
            return _instance;
        }
    }
    #endregion

    public Texture GetHexagonTexture(TileTypes tileType, int temperature, int waterLevel, bool isSelected, out TemperatureZones tempZone)
    {
        tempZone = TemperatureZones.NotSet;
        Texture retTexture = null;
        int selected = 0;
        int dynamicTemp = Engine.Instance.DynamicTemp.Value;

        if (isSelected)
        {
            selected = 1;
        }

        switch (Engine.Instance.ActualSeason)
        {
            case Seasons.Summer:
                dynamicTemp += Engine.Instance.SeasonTempModifier;
                break;
            case Seasons.Winter:
                dynamicTemp -= Engine.Instance.SeasonTempModifier;
                break;
        }


        if ( (tileType == TileTypes.Flat) || (tileType == TileTypes.Mountain) )
        {
            if ( (temperature + dynamicTemp >= Engine.Instance.ColdTemp) && (temperature + dynamicTemp - waterLevel * 3 <= Engine.Instance.HotTemp) )
            {
                retTexture = GrassTextures[selected];
                tempZone = TemperatureZones.Normal;
            }
            else if (temperature + dynamicTemp < Engine.Instance.ColdTemp)
            {
                retTexture = SnowTextures[selected];
                tempZone = TemperatureZones.Cold;
            }
            else
            {
                retTexture = SandTextures[selected];
                tempZone = TemperatureZones.Hot;
            }
        }
        else if (tileType == TileTypes.MapEdge)
        {
            if (temperature + dynamicTemp >= Engine.Instance.ColdTemp - 10)
            { 
                retTexture = MapEdgeTexture;
            }
            else if (temperature + dynamicTemp < Engine.Instance.ColdTemp - 10)
            {
                retTexture = IceTextures[selected];
            }
        }
        else if (tileType == TileTypes.River)
        {
            if (temperature + dynamicTemp >= Engine.Instance.ColdTemp - 5)
            {
                retTexture = WaterTextures[selected];
            }
            else if (temperature + dynamicTemp < Engine.Instance.ColdTemp - 5)
            {
                retTexture = IceTextures[selected];
            }
        }

        return retTexture;
    }
}

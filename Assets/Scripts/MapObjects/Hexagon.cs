using UnityEngine;

public class Hexagon : BaseMapObject
{
    public TileTypes TileType;
    public int Temperature;
    public int WaterLevel = -1;
    private TemperatureZones temperatureZone = TemperatureZones.NotSet;

    public void Initializer(string name, ObjectTypes objectType, SelectionInfoTypes selectionInfo, Vector3 worldPosition, Vector3 offsetPosition, TileTypes tileType, int temperature) 
    {
        TileType = tileType;
        Temperature = temperature;

        base.Initializer(name, objectType, selectionInfo, worldPosition, offsetPosition);

        if ( (tileType == TileTypes.River) || (tileType == TileTypes.MapEdge) )
        {
            WaterLevel = 10;
        }
    }

    public void SetHexTexture()
    {
        TemperatureZones tempZone;

        this.GetComponent<Renderer>().material.mainTexture = MapObjectTextures.Instance.GetHexagonTexture(TileType, Temperature, WaterLevel, _isSelected, out tempZone);

       
        if (tempZone != temperatureZone)
        {
            if (this.GetComponent<MinorMapObject>() != null)
            {
                this.GetComponent<MinorMapObject>().RefreshMinorMapObject();
                temperatureZone = tempZone;
            }    
        }    
    }

    private void Update()
    {
        SetHexTexture();
    }

    public void AddMapObject(HexagonComponents component, MajorMapObjectTypes MajorType = MajorMapObjectTypes.NotSet)
    {
        if (component == HexagonComponents.MinorMapObject)
        {
            this.gameObject.AddComponent<MinorMapObject>().Initializer(ObjectTypes.MinorObject, SelectionInfoTypes.ChildObject, this.WorldPosition, this.OffsetPosition, this.Temperature, WaterLevel);
        }
        if (component == HexagonComponents.MajorMapObject && MajorType != MajorMapObjectTypes.NotSet)
        {
            this.gameObject.AddComponent<MajorMapObject>().Initializer(ObjectTypes.MajorObject, SelectionInfoTypes.ChildObject, this.WorldPosition, this.OffsetPosition, this.Temperature, WaterLevel, MajorType);
        }
        
    }
}

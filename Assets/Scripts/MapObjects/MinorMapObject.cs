using UnityEngine;

public class MinorMapObject : BaseMapObject {

    public int Temperature;
    public int WaterLevel;
    GameObject MinorMapObj = null;
    private TemperatureZones temperatureZone = TemperatureZones.NotSet;

    public void Initializer(ObjectTypes objectType, SelectionInfoTypes selectionInfo, Vector3 worldPosition, Vector3 offsetPosition, int temperature, int waterLevel)
    {
        Temperature = temperature;
        WaterLevel = waterLevel;

        base.Initializer("MinorMapObject(" + offsetPosition.x + ", " + offsetPosition.z + ")", objectType, selectionInfo, worldPosition, offsetPosition);
    }

    public void RefreshMinorMapObject()
    {
        if (MinorMapObj != null)
        {
            MinorMapObjectManager.Instance.DestroyFlowers(MinorMapObj);
        }
        MinorMapObj = MinorMapObjectManager.Instance.GetMinorMapObject(this.gameObject, this.WorldPosition, this.Temperature, this.WaterLevel);
    }
}

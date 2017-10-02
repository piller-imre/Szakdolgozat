using UnityEngine;

public class House : BaseMapObject {

    public int Temperature;
    public int WaterLevel;
    public GameObject[] RoofGameObjects;
    private TemperatureZones temperatureZone = TemperatureZones.NotSet;

    public void Initializer(ObjectTypes objectType, SelectionInfoTypes selectionInfo, Vector3 worldPosition, Vector3 offsetPosition, int temperature, int waterLevel)
    {
        Temperature = temperature;
        WaterLevel = waterLevel;

        base.Initializer("House(" + offsetPosition.x + ", " + offsetPosition.z + ")", objectType, selectionInfo, worldPosition, offsetPosition);
    }

    public void SetHouseAppearance()
    {
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

        if ((Temperature + dynamicTemp >= Engine.Instance.ColdTemp) && (Temperature + dynamicTemp - WaterLevel * 3 <= Engine.Instance.HotTemp) && (temperatureZone != TemperatureZones.Normal))
        {
            for (int i = 0; i < RoofGameObjects.Length; i++)
            {
                RoofGameObjects[i].GetComponent<Renderer>().material.SetTexture("_MainTex", MapObjectTextures.Instance.RoofTextures[Random.Range(0, MapObjectTextures.Instance.RoofTextures.Length)]);
                temperatureZone = TemperatureZones.Normal;
            }
        }
        else if ((Temperature + dynamicTemp < Engine.Instance.ColdTemp) && (temperatureZone != TemperatureZones.Cold))
        {
            for (int i = 0; i < RoofGameObjects.Length; i++)
            {
                RoofGameObjects[i].GetComponent<Renderer>().material.SetTexture("_MainTex", MapObjectTextures.Instance.RoofSnowTexture);
                temperatureZone = TemperatureZones.Cold;
            }
        }
    }

    private void Update()
    {
        SetHouseAppearance();
    }
}

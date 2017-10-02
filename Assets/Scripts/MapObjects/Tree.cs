using UnityEngine;

public class Tree : BaseMapObject {

    public int Temperature;
    public int WaterLevel;
    public GameObject[] Leaves;
    public GameObject[] Trunk;
    private int LastMaterial = 0;

    public void Initializer(ObjectTypes objectType, SelectionInfoTypes selectionInfo, Vector3 worldPosition, Vector3 offsetPosition, int temperature, int waterLevel)
    {
        Temperature = temperature;
        WaterLevel = waterLevel;

        base.Initializer("Tree(" + offsetPosition.x + ", " + offsetPosition.z + ")", objectType, selectionInfo, worldPosition, offsetPosition);
    }

    public void ChangeToNormal()
    {
        LastMaterial = 1;
        foreach (var leaf in Leaves)
        {
            leaf.GetComponent<Renderer>().enabled = true;
            leaf.GetComponent<Renderer>().material = MapObjectMaterials.Instance.GreenLeafMaterials[Random.Range(0, MapObjectMaterials.Instance.GreenLeafMaterials.Length)];
        }
    }

    public void ColorLeaves()
    {
        LastMaterial = 2;
        foreach (var leaf in Leaves)
        {
            leaf.GetComponent<Renderer>().enabled = true;
            leaf.GetComponent<Renderer>().material = MapObjectMaterials.Instance.FallColorMaterials[Random.Range(0, MapObjectMaterials.Instance.FallColorMaterials.Length)]; ;
        }
    }

    public void RemoveLeaves()
    {
        LastMaterial = 3;
        foreach (var leaf in Leaves)
        {
            leaf.GetComponent<Renderer>().enabled = false;
        }
    }

    private void SetTreeAppearance()
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

        if ( (Temperature + dynamicTemp >= Engine.Instance.ColdTemp + 3) && (Temperature + dynamicTemp - WaterLevel * 3 <= Engine.Instance.HotTemp - 3) && (LastMaterial !=1) )
        {
            ChangeToNormal();
        }
        else if ( (Temperature + dynamicTemp >= Engine.Instance.ColdTemp) && (Temperature + dynamicTemp < Engine.Instance.ColdTemp + 3) && (LastMaterial != 2) )
        {
            ColorLeaves();
        }
        else if ( (Temperature + dynamicTemp - WaterLevel * 3 <= Engine.Instance.HotTemp) && (Temperature + dynamicTemp - WaterLevel * 3 > Engine.Instance.HotTemp - 3) && (LastMaterial != 2) )
        {
            ColorLeaves();
        }
        else if ( (Temperature + dynamicTemp < Engine.Instance.ColdTemp || Temperature + dynamicTemp - WaterLevel * 3 > Engine.Instance.HotTemp) && (LastMaterial != 3) )
        {
            RemoveLeaves();
        }
    }

    private void Update()
    {
        SetTreeAppearance();
    }
}

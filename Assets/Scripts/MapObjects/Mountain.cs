using UnityEngine;

public class Mountain : BaseMapObject
{
    public int Temperature;

    public void Initializer(string name, ObjectTypes objectType, SelectionInfoTypes selectionInfo, Vector3 worldPosition, Vector3 offsetPosition, int temperature)
    {
        Temperature = temperature;

        base.Initializer(name, objectType, selectionInfo, worldPosition, offsetPosition);
    }

    public void SetMountainTexture()
    {
        this.GetComponent<Renderer>().material = MapObjectMaterials.Instance.GetMountainMaterial(Temperature, MapGenerator.map[(int)OffsetPosition.x, (int)OffsetPosition.z].GetComponent<Hexagon>().WaterLevel);
    }

    void Update()
    {
        SetMountainTexture();
    }
}

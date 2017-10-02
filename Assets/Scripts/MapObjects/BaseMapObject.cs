using UnityEngine;

public class BaseMapObject : MonoBehaviour {

    public ObjectTypes ObjectType;
    public SelectionInfoTypes SelectionInfo;
    internal bool _isSelected = false;

    public Vector3 WorldPosition;
    public Vector3 OffsetPosition;

    protected void Initializer(string name, ObjectTypes objectType, SelectionInfoTypes selectionInfo, Vector3 worldPosition, Vector3 offsetPosition)
    {
        this.gameObject.name = name;
        this.transform.position = worldPosition;

        ObjectType = objectType;
        SelectionInfo = selectionInfo;
        WorldPosition = worldPosition;
        OffsetPosition = offsetPosition;
    }

    public void onSelect()
    {
        _isSelected = true;
    }

    public void onDeselect()
    {
        _isSelected = false;
    }
}

using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour {

	public GameObject selectedObject;
    Hexagon hex;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                //Debug.Log("Mouse is over: " + hitInfo.collider.name);

                // The collider we hit may not be the "root" of the object
                // You can grab the most "root-est" gameobject using
                // transform.root, though if your objects are nested within
                // a larger parent GameObject (like "All Units") then this might
                // not work.  An alternative is to move up the transform.parent
                // hierarchy until you find something with a particular component.

                //GameObject hitObject = hitInfo.transform.root.gameObject;
                GameObject hitObject = hitInfo.transform.gameObject;

                SelectObject(hitObject);

            }
            else
            {
                ClearSelection();
            }
        }
	
	}

	void SelectObject(GameObject obj)
    {
		if(selectedObject != null)
        {
			if(obj == selectedObject)
            {
                return;
            }

            //if (obj.GetComponent<BaseMapObject>().SelectionInfo == SelectionInfoTypes.NonSelectable)
            //{
            //    Debug.Log("NonSelectable");
            //    return;
            //}

            //if (obj.GetComponent<BaseMapObject>().SelectionInfo == SelectionInfoTypes.ChildObject)
            //{
            //    Debug.Log("Parent");
            //}

            if ( obj.GetComponent<Mountain>() != null )
            {
                Vector3 coord = obj.GetComponent<Mountain>().OffsetPosition;
                //Debug.Log(coord);
                obj = MapGenerator.map[(int)coord.x, (int)coord.z];
            }

			ClearSelection();
		}

		selectedObject = obj;

        //Debug.Log(selectedObject);

        selectedObject.GetComponent<Hexagon>().onSelect(); 
	}

	void ClearSelection()
    {
		if(selectedObject == null)
        {
            return;
        }

        selectedObject.GetComponent<Hexagon>().onDeselect();

        selectedObject = null;
	} 
}

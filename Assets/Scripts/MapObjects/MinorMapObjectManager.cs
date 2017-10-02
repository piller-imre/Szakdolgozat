using UnityEngine;

public class MinorMapObjectManager : MonoBehaviour {

    public GameObject[] Flowers;
    public GameObject[] DesertObjects;

    public Vector3 range;

    #region SingletonPattern
    public static MinorMapObjectManager _instance;
    public static MinorMapObjectManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MinorMapObjectManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("MinorMapObjectManager");
                    _instance = container.AddComponent<MinorMapObjectManager>();
                }
            }
            return _instance;
        }
    }
    #endregion

    public GameObject GetMinorMapObject(GameObject parentObj, Vector3 worldPos, int temperature, int waterLevel)
    {
        GameObject parent = new GameObject();
        parent.transform.position = worldPos;
        parent.transform.parent = parentObj.transform;

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
            GetFlowers(parent, waterLevel);
        }
        else if (temperature + dynamicTemp < Engine.Instance.ColdTemp)
        {
            //cold
        }
        else
        {
            GetRocks(parent);
        }

        return parent;
    }

    private void GetFlowers(GameObject parent, int waterLevel)
    {
        GameObject child;

        int numOfFlowers = Random.Range(1, waterLevel - 3);

        for (int i = 0; i < numOfFlowers; i++)
        {
            int currentFlower = Random.Range(0, Flowers.Length);

            child = Instantiate(Flowers[currentFlower]);

            child.transform.position = new Vector3((float)(parent.transform.position.x + Random.Range(-0.45f, 0.45f) * range.x), Flowers[currentFlower].transform.position.y, (float)(parent.transform.position.z + Random.Range(-0.45f, 0.45f) * range.z));

            child.transform.parent = parent.transform;
        }
    }

    private void GetRocks(GameObject parent)
    {
        GameObject child;

        int numOfRocks = Random.Range(0, 3);

        for (int i = 0; i < numOfRocks; i++)
        {
            int currentRock = Random.Range(0, DesertObjects.Length);

            child = Instantiate(DesertObjects[currentRock]);

            child.transform.position = new Vector3((float)(parent.transform.position.x + Random.Range(-0.5f, 0.5f) * range.x), DesertObjects[currentRock].transform.position.y, (float)(parent.transform.position.z + Random.Range(-0.5f, 0.5f) * range.z));

            child.transform.parent = parent.transform;
        }
    }

    public void DestroyFlowers(GameObject parent)
    {
        int childs = parent.transform.childCount;
        for (int i = childs - 1; i > 0; i--)
        {
            GameObject.Destroy(parent.transform.GetChild(i).gameObject);
        }

        Destroy(parent);
    }

    private void Start()
    {
        range = MapGenerator.Instance.Offset;
    }
}

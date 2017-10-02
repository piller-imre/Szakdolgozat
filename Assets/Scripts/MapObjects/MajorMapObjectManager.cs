using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorMapObjectManager : MonoBehaviour {

    public GameObject[] Trees_Center;
    public GameObject[] Trees_Side;
    public GameObject[] Houses_Center;
    public GameObject[] Houses_Side;

    #region SingletonPattern
    public static MajorMapObjectManager _instance;
    public static MajorMapObjectManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MajorMapObjectManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("MajorMapObjectManager");
                    _instance = container.AddComponent<MajorMapObjectManager>();
                }
            }
            return _instance;
        }
    }
    #endregion

    public GameObject GetTree(bool isCenter)
    {
        if (isCenter)
        {
            return Trees_Center[Random.Range(0, Trees_Center.Length)];
        }
        else
        {
            return Trees_Side[Random.Range(0, Trees_Side.Length)];
        }
    }

    public GameObject GetHouse(bool isCenter)
    {
        if (isCenter)
        {
            return Houses_Center[Random.Range(0, Houses_Center.Length)];
        }
        else
        {
            return Houses_Side[Random.Range(0, Houses_Side.Length)];
        }
    }
}

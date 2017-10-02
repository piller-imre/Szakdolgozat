using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityToolbag;

public class MapGenerator : MonoBehaviour {

    #region Fields

    [Header("Logger Settings")]
    public bool LoggerEnabled;

    [Header("Map Config")]
    public Vector3 startPos;
    public Vector3 Offset;
    public float gap;
    private bool isMapEmpty = true;

    [Header("Meshes")]
    public GameObject HexPreFab;
    public GameObject[] MountainPreFabs;

    public static GameObject[,] map;
    private List<Vector2> OpenFlatTiles = new List<Vector2>();
    private int MaxNumOfCities;
    private int MaxNumOfBiome;

    #region OffsetDisrections
    public Vector2[,] offsetDirections = new Vector2[2, 6]
    {
        {
           //Even row
            new Vector2(+1, 0), new Vector2(0, -1), new Vector2(-1, -1),
            new Vector2(-1, 0), new Vector2(-1, +1), new Vector2(0, +1)
        },
        {  
            //Odd row
            new Vector2(+1, 0), new Vector2(+1, -1), new Vector2(0, -1),
            new Vector2(-1, 0), new Vector2(0, +1), new Vector2(+1, +1)
        }
    };
    #endregion

    #endregion

    #region SingletonPattern
    public static MapGenerator _instance;
    public static MapGenerator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MapGenerator>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("MapGenerator");
                    _instance = container.AddComponent<MapGenerator>();
                }
            }
            return _instance;
        }
    }
    #endregion

    #region MainMethods

    /// <summary>
    /// API: Yes 
    /// Use to creates a new hexagon grid map in the game
    /// </summary>
    public void CreateMap()
    {
        if (!isMapEmpty)
        {
            return;
        }

        isMapEmpty = false;

        myLogger.CreateLogFile();
        myLogger.AddToLogFile("MapGenerator", "Details: ", true);
        myLogger.AddToLogFile("Map size: " + Engine.Instance.MapWidth.Value + " x " + Engine.Instance.MapHeight.Value);
        myLogger.AddToLogFile("MountainPercent: " + Engine.Instance.MountainPercent.Value);
        myLogger.AddToLogFile("MapGenerator", "CreateMap Start", true);

        map = new GameObject[Engine.Instance.MapWidth.Value + 2, Engine.Instance.MapHeight.Value + 2];

        Engine.Instance.CameraLimitX2 = map.GetLength(0) * Offset.x;
        Engine.Instance.CameraLimitY2 = map.GetLength(1) * Offset.z;

        

        Thread myThread = new Thread(() =>
        {
            bool BreakCondition = false;

            Dispatcher.Invoke(() =>
            {
                CreateMapEdge();
            });

            while (!BreakCondition)
            {
                Dispatcher.Invoke(() =>
                {
                    if (Input.GetKey("down"))
                    {
                        print("down arrow key is held down");
                        BreakCondition = true;
                    }
                });
            }

            BreakCondition = false;

            Dispatcher.Invoke(() =>
            {
                CreateMountains();
            });

            while (!BreakCondition)
            {
                Dispatcher.Invoke(() =>
                {
                    if (Input.GetKey("down"))
                    {
                        print("down arrow key is held down");
                        BreakCondition = true;
                    }
                });
            }

            BreakCondition = false;

            Dispatcher.Invoke(() =>
            {
                CreateRivers();
            });

            while (!BreakCondition)
            {
                Dispatcher.Invoke(() =>
                {
                    if (Input.GetKey("down"))
                    {
                        print("down arrow key is held down");
                        BreakCondition = true;
                    }
                });
            }

            BreakCondition = false;

            Dispatcher.Invoke(() =>
            {
                FillEmptyTiles();
            });

            while (!BreakCondition)
            {
                Dispatcher.Invoke(() =>
                {
                    if (Input.GetKey("down"))
                    {
                        print("down arrow key is held down");
                        BreakCondition = true;
                    }
                });
            }

            BreakCondition = false;

            Dispatcher.Invoke(() =>
            {
                SetWaterLevelOnTiles();
            });

            while (!BreakCondition)
            {
                Dispatcher.Invoke(() =>
                {
                    if (Input.GetKey("down"))
                    {
                        print("down arrow key is held down");
                        BreakCondition = true;
                    }
                });
            }

            BreakCondition = false;

            Dispatcher.Invoke(() =>
            {
                CreateCities();
            });

            while (!BreakCondition)
            {
                Dispatcher.Invoke(() =>
                {
                    if (Input.GetKey("down"))
                    {
                        print("down arrow key is held down");
                        BreakCondition = true;
                    }
                });
            }

            BreakCondition = false;

            Dispatcher.Invoke(() =>
            {
                CreateBiome();
            });

            while (!BreakCondition)
            {
                Dispatcher.Invoke(() =>
                {
                    if (Input.GetKey("down"))
                    {
                        print("down arrow key is held down");
                        BreakCondition = true;
                    }
                });
            }

        });

        myThread.Start();

        //CreateMapEdge();
        //CreateMountains();
        //CreateRivers();
        //FillEmptyTiles();
        //SetWaterLevelOnTiles();
        //CreateCities();
        //CreateBiome();

        myLogger.AddToLogFile("MapGenerator", "CreateMap end");
        myLogger.CloseLogFile();
    }

    /// <summary>
    /// API: Yes
    /// Use to destroy the map
    /// </summary>
    public void ClearMap()
    {
        //myLogger.AddToLogFile("MapGenerator", "ClearMap start", true); 
        try
        {
            while (true) //because not working properly
            {
                if (this.transform.childCount != 0)
                {
                    //Debug.Log("Number of MapObjects: " + this.transform.childCount);
                    foreach (Transform child in this.transform)
                    {
                        GameObject.DestroyImmediate(child.gameObject);
                    }
                }
                else
                {
                    //Debug.Log("Map is Empty");
                    isMapEmpty = true;
                    break;
                }
            }

            OpenFlatTiles.Clear();
        }
        catch (System.Exception)
        {

        }

        //myLogger.AddToLogFile("MapGenerator", "ClearMap end");
        //Debug.Log("Map is Cleared");
    }

    #endregion

    #region API

    /// <summary>
    /// API: Yes, Use to get map length
    /// </summary>
    /// <returns></returns>
    public static Vector2 GetMapLength()
    {
        return new Vector2(map.GetLength(0), map.GetLength(1));
    }

    /// <summary>
    /// API: Yes, get tile at the given position
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static GameObject GetMapTile(Vector2 position)
    {
        return map[(int)position.x, (int)position.y];
    }

    #endregion

    #region OtherMethods

    /// <summary>
    /// API:NO
    /// Create new hexagon tile object
    /// Coordinate transform:
    /// x -> x,
    /// y -> z
    /// </summary>
    /// <param name="offsetPosition"></param>
    /// <param name="actualHexTexture"></param>
    /// <param name="actualHexTerrain"></param>
    /// <returns></returns>
    private GameObject CreateTile(Transform parent, Vector3 offsetPosition, TileTypes tileType, SelectionInfoTypes selectionInfo)
    {
        float tempChange = Engine.Instance.TempDifference.Value / (float)Engine.Instance.MapHeight.Value;
        int temp = Engine.Instance.StaticTemp.Value + (int)(offsetPosition.z * tempChange);

        GameObject actualHex = Instantiate(HexPreFab, parent);

        //position in unity coord
        Vector3 position = CalculateWorldPosition(offsetPosition);

        actualHex.GetComponent<Hexagon>().Initializer("Hex(" + offsetPosition.x + "," + offsetPosition.z + ")", ObjectTypes.Hexagon, selectionInfo, position, offsetPosition, tileType, temp);

        map[(int)offsetPosition.x, (int)offsetPosition.z] = actualHex;

        if (tileType == TileTypes.Mountain)
        {
            GameObject actualHexTerrain = Instantiate(MountainPreFabs[UnityEngine.Random.Range(0, MountainPreFabs.Length)], this.transform);
            Vector3 MountainPos = new Vector3(position.x, actualHexTerrain.transform.position.y, position.z);
            actualHexTerrain.AddComponent<Mountain>().Initializer("Mountain(" + offsetPosition.x + "," + offsetPosition.z + ")", ObjectTypes.Mountain, SelectionInfoTypes.ChildObject, MountainPos, offsetPosition, temp - 5);
        }

        return actualHex;
    }

    /// <summary>
    /// API: No
    /// </summary>
    private void CreateMapEdge()
    {
        myLogger.AddToLogFile("MapGenerator", "CreateMapEdge start", true);

        for (int x = 0; x < map.GetLength(0); x++)
        {
            CreateTile(this.transform, new Vector3(x, 0, 0), TileTypes.MapEdge, SelectionInfoTypes.NonSelectable);
            CreateTile(this.transform, new Vector3(x, 0, map.GetLength(1) - 1), TileTypes.MapEdge, SelectionInfoTypes.NonSelectable);
        }

        for (int y = 0; y < map.GetLength(1); y++)
        {
            CreateTile(this.transform, new Vector3(0, 0, y), TileTypes.MapEdge, SelectionInfoTypes.NonSelectable);
            CreateTile(this.transform, new Vector3(map.GetLength(0) - 1, 0, y), TileTypes.MapEdge, SelectionInfoTypes.NonSelectable);
        }

        myLogger.AddToLogFile("MapGenerator", "CreateMapEdge end");
    }

    /// <summary>
    /// API: No,
    /// </summary>
    private void CreateMountains()
    {
        myLogger.AddToLogFile("MapGenerator", "CreateMountains start", true);

        int maxNumOfMountaintiles = (int)(Engine.Instance.MapWidth.Value * Engine.Instance.MapHeight.Value * (Engine.Instance.MountainPercent.Value / 100.0));
        //Debug.Log(idealNumOfMountaintiles.ToString());

        System.Random rnd = new System.Random();

        int i = 0;
        while (i < maxNumOfMountaintiles)
        {
            Vector3 position = new Vector3(rnd.Next(1, map.GetLength(0) - 1), 0, rnd.Next(1, map.GetLength(1) - 1));
            //Debug.Log(position.ToString());

            //Debug.Log(map[(int)position.x, (int)position.z]);

            if (map[(int)position.x, (int)position.z] == null)
            {
                CreateTile(this.transform, position, TileTypes.Mountain, SelectionInfoTypes.ChildObject);
                i++;
            }
        }

        myLogger.AddToLogFile("MapGenerator", "CreateMountains end");
    }

    /// <summary>
    /// API: No,
    /// </summary>
    private void CreateRivers()
    {
        myLogger.AddToLogFile("MapGenerator", "CreateRiver start", true);

        int idealNumOfRiverTiles = (int)(Engine.Instance.MapWidth.Value * Engine.Instance.MapHeight.Value * (Engine.Instance.RiverPercent.Value / 100.0));
        int numOfRiverTiles = 0;

        while (numOfRiverTiles < idealNumOfRiverTiles)
        {
            Vector2 startPosition = new Vector2();
            Vector2 endPosition = new Vector2();

            System.Random rnd = new System.Random();
            bool isStartPositionFound = false;

            //Search for start position
            myLogger.AddToLogFile("MapGenerator", "SearchStartPosition start", true);

            // it's a flat map or not
            if (Engine.Instance.MountainPercent.Value > 0)
            {
                while (!isStartPositionFound)
                {
                    Vector2 position = new Vector2(rnd.Next(1, map.GetLength(0) - 1), rnd.Next(1, map.GetLength(1) - 1));

                    //Debug.Log(map[(int)position.x, (int)position.y]);
                    //Debug.Log(map[(int)position.x, (int)position.y] != null);

                    if (map[(int)position.x, (int)position.y] != null)
                    {
                        if (map[(int)position.x, (int)position.y].GetComponent<Hexagon>().TileType == TileTypes.Mountain)
                        {
                            int parity = (int)(position.y % 2);

                            for (int i = 0; i < offsetDirections.GetLength(1); i++)
                            {
                                int neighbourX = (int)(position.x + offsetDirections[parity, i].x);
                                int neighbourY = (int)(position.y + offsetDirections[parity, i].y);

                                if (map[neighbourX, neighbourY] == null)
                                {
                                    startPosition = new Vector2(neighbourX, neighbourY);
                                    isStartPositionFound = true;
                                    myLogger.AddToLogFile("MapGenerator", "SearchStartPosition: " + position.x + ", " + position.y);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                startPosition = new Vector2(rnd.Next(1, map.GetLength(0) - 1), rnd.Next(1, map.GetLength(1) - 1));
                myLogger.AddToLogFile("MapGenerator", "SearchStartPosition: " + startPosition.x + ", " + startPosition.y);
            }

            myLogger.AddToLogFile("MapGenerator", "SearchStartPosition end");
            //----------------------

            bool isEndPositionFound = false;

            //Search for end position
            myLogger.AddToLogFile("MapGenerator", "SearchEndPosition Start", true);
            while (!isEndPositionFound)
            {
                Vector2 position;

                position = new Vector2(rnd.Next(0, map.GetLength(0)), rnd.Next(0, map.GetLength(1)));

                if (map[(int)position.x, (int)position.y] != null)
                {
                    if ( (map[(int)position.x, (int)position.y].GetComponent<Hexagon>().TileType == TileTypes.MapEdge) || (map[(int)position.x, (int)position.y].GetComponent<Hexagon>().TileType == TileTypes.River) )
                    {
                        endPosition = new Vector2(position.x, position.y);
                        isEndPositionFound = true;
                        myLogger.AddToLogFile("MapGenerator", "SearchEndPosition: " + position.x + ", " + position.y);
                    }
                }
            }

            myLogger.AddToLogFile("MapGenerator", "SearchEndPosition end");
            //----------------------

            //startPosition = new Vector2(5, 5);
            //endPosition = new Vector2(10, 10);

            myLogger.AddToLogFile("MapGenerator", "PathFinding Start", true);
            Node RiverPath = PathFinding.FindRiverPath(startPosition, endPosition);
            myLogger.AddToLogFile("MapGenerator", "PathFinding end");

            //Debug.Log(RiverPath);
            //Debug.Log(RiverPath.Parent);

            try
            {
                //WTF?
                //Debug.Log(RiverPath.Parent != null);

                while (true) //RiverPath.Parent != null //#BUG
                {
                    if (numOfRiverTiles < idealNumOfRiverTiles)
                    {
                        CreateTile(this.transform, new Vector3(RiverPath.Position.x, 0, RiverPath.Position.y), TileTypes.River, SelectionInfoTypes.Selectable);
                        RiverPath = RiverPath.Parent;
                        numOfRiverTiles++;
                    }
                    else
                    {
                        break;
                    }

                }
            }
            catch (System.Exception)
            {
            }  
        }

        myLogger.AddToLogFile("MapGenerator", "CreateRiver end");
    }

    /// <summary>
    /// API: No,
    /// </summary>
    private void FillEmptyTiles()
    {
        myLogger.AddToLogFile("MapGenerator", "FillEmptyTiles start", true);

        int q = 0;

        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                if (map[x, y] == null)
                {
                    CreateTile(this.transform, new Vector3(x, 0, y), TileTypes.Flat, SelectionInfoTypes.Selectable);
                    OpenFlatTiles.Add(new Vector2(x, y));
                    q++;
                }
            }
        }

        MaxNumOfCities = (int)(OpenFlatTiles.Count * (Engine.Instance.CityPercent.Value / 100.0));
        MaxNumOfBiome = (int)(OpenFlatTiles.Count * (Engine.Instance.BiomePercent.Value / 100.0));

        myLogger.AddToLogFile("MapGenerator", "FillEmptyTiles end");
        //Debug.Log("OpenflatTiles: " + OpenFlatTiles.Count);
        //Debug.Log("Q: " + q);
        //Debug.Log("MaxNumOfCities: " + MaxNumOfCities);
        //Debug.Log("MaxNumOfBiome: " + MaxNumOfBiome);
    }

    private void CreateCities()
    {
        myLogger.AddToLogFile("MapGenerator", "CreateCities start", true);

        if (OpenFlatTiles.Count > 0)
        {
            int numOfCities = 0;
    
            while ( (numOfCities < MaxNumOfCities) && (OpenFlatTiles.Count > 0))
            {
                Vector2 index = OpenFlatTiles[UnityEngine.Random.Range(0, OpenFlatTiles.Count)];

                map[(int)index.x, (int)index.y].GetComponent<Hexagon>().AddMapObject(HexagonComponents.MajorMapObject, MajorMapObjectTypes.City);
                numOfCities++;
                OpenFlatTiles.Remove(index);
            }
        }

         myLogger.AddToLogFile("MapGenerator", "CreateCities end", true);
    }

    private void CreateBiome()
    {
        myLogger.AddToLogFile("MapGenerator", "CreateBiome start", true);

        if (OpenFlatTiles.Count > 0)
        {
            int numOfMinor = 0;
            int numOfMajor = 0;

            while ( (numOfMinor < MaxNumOfBiome || numOfMajor < MaxNumOfBiome ) && (OpenFlatTiles.Count > 0) )
            {
                Vector2 index = OpenFlatTiles[UnityEngine.Random.Range(0, OpenFlatTiles.Count)];
                
                int hexComponentIndex = 0;

                if (map[(int)index.x, (int)index.y].GetComponent<MinorMapObject>() != null)
                {
                    hexComponentIndex += 1;
                }
                if (map[(int)index.x, (int)index.y].GetComponent<MajorMapObject>() != null)
                {
                    hexComponentIndex += 2;
                }

                switch (hexComponentIndex)
                {
                    case 0:
                        if (UnityEngine.Random.Range(0, 2) == 0)
                        {
                            map[(int)index.x, (int)index.y].GetComponent<Hexagon>().AddMapObject(HexagonComponents.MinorMapObject, MajorMapObjectTypes.Trees);
                            numOfMinor++;
                        }
                        else
                        {
                            map[(int)index.x, (int)index.y].GetComponent<Hexagon>().AddMapObject(HexagonComponents.MajorMapObject, MajorMapObjectTypes.Trees);
                            numOfMajor++;
                        }
                        break;
                    case 1:
                        map[(int)index.x, (int)index.y].GetComponent<Hexagon>().AddMapObject(HexagonComponents.MajorMapObject, MajorMapObjectTypes.Trees);
                        numOfMajor++;
                        break;
                    case 2:
                        map[(int)index.x, (int)index.y].GetComponent<Hexagon>().AddMapObject(HexagonComponents.MinorMapObject, MajorMapObjectTypes.Trees);
                        numOfMinor++;
                        break;
                    case 3:
                        OpenFlatTiles.Remove(index);
                        break;
                }
            }
        }
        myLogger.AddToLogFile("MapGenerator", "CreateBiome end", true);
    }

    /// <summary>
    /// API: No
    /// Use to calculate hexagons position in the game world
    /// </summary>
    /// <param name="mapPos"></param>
    /// <returns></returns>
    private Vector3 CalculateWorldPosition(Vector3 mapPos)
    {
        float offset = 0;
        if (mapPos.z % 2 != 0)
        {
            offset = Offset.x / 2;
        }

        float x = startPos.x + mapPos.x * Offset.x + offset;
        float z = startPos.z + mapPos.z * Offset.z;

        return new Vector3(x, 0, z);
    }

    private static int CalculateDistance(Vector2 startPoint, Vector2 endPoint)
    {
        Vector3 startPointCube = OffsetToCube(startPoint);
        Vector3 endPointCube = OffsetToCube(endPoint);

        return CubeDistance(startPointCube, endPointCube);
    }

    private static int CubeDistance(Vector3 startPoint, Vector3 endPoint)
    {
        return (int)(Math.Abs(startPoint.x - endPoint.x) + Math.Abs(startPoint.y - endPoint.y) + Math.Abs(startPoint.z - endPoint.z)) / 2;
    }

    private static Vector2 CubeToOffset(Vector3 cube)
    {
        int col = (int)(cube.x + (cube.z - (cube.z % 2)) / 2);
        int row = (int)cube.z;

        return new Vector2(col, row);
    }

    private static Vector3 OffsetToCube(Vector2 hex)
    {
        int x = (int)(hex.y - (hex.x - (hex.x % 2)) / 2);
        int z = (int)hex.x;
        int y = -x - z;

        return new Vector3(x, y, z);
    }

    private void SetWaterLevelOnTiles()
    {
        myLogger.AddToLogFile("MapGenerator", "SetWaterLevelOnTiles start", true);

        List<GameObject> WaterTiles = new List<GameObject>();

        foreach (var currentTile in map)
        {
            if ( (currentTile.GetComponent<Hexagon>().TileType == TileTypes.MapEdge) || (currentTile.GetComponent<Hexagon>().TileType == TileTypes.River))
            {
                WaterTiles.Add(currentTile);
            }
        }

        foreach (var currentTile in map)
        {
            if (currentTile.GetComponent<Hexagon>().WaterLevel >= 0)
            {
                continue;
            }

            int shortestDistance = 10;

            foreach (var currentWaterTile in WaterTiles)
            {
                int distance = CalculateDistance(new Vector2(currentTile.GetComponent<Hexagon>().OffsetPosition.x, currentTile.GetComponent<Hexagon>().OffsetPosition.z), new Vector2(currentWaterTile.GetComponent<Hexagon>().OffsetPosition.x, currentWaterTile.GetComponent<Hexagon>().OffsetPosition.z));

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                }
            }

            currentTile.GetComponent<Hexagon>().WaterLevel = 10 - shortestDistance;
        }

        myLogger.AddToLogFile("MapGenerator", "SetWaterLevelOnTiles end");
    }

    #endregion
}

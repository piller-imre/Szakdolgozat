public enum TileTypes
{
    MapEdge,
    River,
    Flat,
    Mountain    
};

public enum ObjectTypes
{
    Hexagon,
    Mountain,
    MinorObject,
    MajorObject
}

public enum SelectionInfoTypes
{
    Selectable,
    ChildObject,
    NonSelectable
}

public enum MapGeneratorTypes
{
    MapWidth,
    MapHeight,
    MountainPercent,
    RiverPercent,
    StaticTemp,
    DynamicTemp,
    TempDifference,
    BiomePercent,
    CityPercent
}

public enum Seasons
{
    Spring,
    Summer,
    Fall,
    Winter
}

public enum TemperatureZones
{
    NotSet,
    Cold,
    Normal,
    Hot
}

public enum HexagonComponents
{
    MinorMapObject,
    MajorMapObject
}

public enum MajorMapObjectTypes
{
    NotSet,
    City,
    Trees
}

public enum MajorMapObjectPlaces
{
    Center = -1,
    Side1 = 0,
    Side2 = 1,
    Side3 = 2,
    Side4 = 3,
    Side5 = 4,
    Side6 = 5
}
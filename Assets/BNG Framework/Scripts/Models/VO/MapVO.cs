using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
[System.Serializable]
public class MapInfo
{
    public string path;
}

public class MapVO : BaseVO
{
    public MapVO()
    {
        LoadData("Maps");
    }

    public MapInfo GetMapInfo(int level)
    {
        JSONArray array = data["data"].AsArray;
        if (level >= array.Count) return JsonUtility.FromJson<MapInfo>(array[array.Count - 1].ToString());
        return JsonUtility.FromJson<MapInfo>(array[level].ToString());
    }
    public int MapCount
    {
        get
        {
            JSONArray array = data["data"].AsArray;
            return array.Count;
        }
    }
}

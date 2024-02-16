using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelVO : BaseVO
{
    public LevelVO()
    {
        LoadData("Level");
    }

    public int GetCurrentLevel()
    {
        return data["data"]["currentLevel"];
    }

    public Vector3 GetSpawnPoint()
    {
        Vector3 spawnPoint = new Vector3(data["data"]["spawnPointX"], data["data"]["spawnPointY"], data["data"]["spawnPointZ"]);
        //Debug.Log("X: " + data["data"]["spawnPointX"] + " Y: " + data["data"]["spawnPointY"] + " Z: " + data["data"]["spawnPointZ"]);
        return spawnPoint;
    }

    public void SaveCurrentProgress()
    {
        TextAsset text = Resources.Load<TextAsset>("Data/Level");
        data = JSON.Parse(text.text);
        data["data"]["currentLevel"] = GlobalVar.currentLevel;
        data["data"]["spawnPointX"] = GlobalVar.spawnPoint.x;
        data["data"]["spawnPointY"] = GlobalVar.spawnPoint.y;
        data["data"]["spawnPointZ"] = GlobalVar.spawnPoint.z;
        File.WriteAllText("Assets\\BNG Framework\\Resources\\Data\\Level.json", data.ToString());
    }
}

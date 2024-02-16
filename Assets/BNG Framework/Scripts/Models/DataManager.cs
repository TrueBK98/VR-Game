using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.DesignPattern;

public class DataManager : Singleton<DataManager>
{
    private bool dataLoaded = false;
    public LevelVO levelVO;
    public MapVO mapVO;

    public void LoadData()
    {
        if (dataLoaded)
        {
            return;
        }
        levelVO = new LevelVO();
        mapVO = new MapVO();

        dataLoaded = true;
    }
}

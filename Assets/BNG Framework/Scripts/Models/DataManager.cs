using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.DesignPattern;

public class DataManager : Singleton<DataManager>
{
    private bool dataLoaded = false;
    public MapVO mapVO;

    public void LoadData()
    {
        if (dataLoaded)
        {
            return;
        }
        mapVO = new MapVO();

        dataLoaded = true;
    }
}

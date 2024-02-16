using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MapInfo mapInfo = DataManager.Instance.mapVO.GetMapInfo(GlobalVar.currentLevel);
        GameObject prefabMap = Resources.Load<GameObject>(mapInfo.path);
        Instantiate(prefabMap);

        if (GlobalVar.spawnPoint != Vector3.zero)
        {
            Player.Instance.transform.position = GlobalVar.spawnPoint;
        }
    }
}

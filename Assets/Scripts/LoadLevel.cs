using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour {
    void Awake() {
        //获取当前地图
        string currentMap = PlayerPrefs.GetString("CurrentMap");
        //加载当前地图当前关卡对应的关卡场景对象
        GameObject currentLevel=Resources.Load<GameObject>("Levels/"+currentMap+"/"+PlayerPrefs.GetString("CurrentLevel"));
        Instantiate(currentLevel, Vector3.zero, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

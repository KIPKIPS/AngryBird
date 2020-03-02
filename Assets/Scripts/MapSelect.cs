using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour {
    public int unlockStarNum;//解锁所需星星数量
    public bool canSelect;//关卡是否可以被选择

    public GameObject stars;
    public GameObject locks;

    public GameObject panel;
    public GameObject map;

    public Button bt;

    public int mapNum;
    // Start is called before the first frame update
    void Start() {
        bt = GetComponent<Button>();
        bt.enabled = false;
        //默认解锁第一个地图
        if (transform.parent.GetChild(0).name==gameObject.name) {
            canSelect = true;
            
        }
        else {
            if (PlayerPrefs.GetInt("TotalNumOfStarsInMap" + (mapNum-1)) >= unlockStarNum) {
                canSelect = true;
            }
        }
        
        if (canSelect) {
            bt.enabled = true;
            locks.SetActive(false);
            stars.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select() {
        if (canSelect) {
            panel.SetActive(true);
            map.SetActive(false);
            //存储当前关卡的名字编号
            PlayerPrefs.SetString("CurrentMap", "Map" + mapNum);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour {
    public int starNum;//解锁所需星星数量
    public bool canSelect;//关卡是否可以被选择

    public GameObject stars;
    public GameObject locks;

    public GameObject panel;
    public GameObject map;

    public Button bt;
    // Start is called before the first frame update
    void Start() {
        bt = GetComponent<Button>();
        bt.enabled = false;
        if (PlayerPrefs.GetInt("totalNumOfStar", 0)>=starNum) {
            canSelect = true;
            bt.enabled = true;
        }

        if (canSelect) {
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
        }
    }
}

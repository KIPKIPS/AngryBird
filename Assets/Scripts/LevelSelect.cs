using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
    public bool canSelect = false;
    public Image image;
    public Sprite unlockSprite;
    public Button bt;
    public GameObject levelNum;

    public GameObject[] stars;
    public int totalLevel = 6;
    string currentMap;
    // Start is called before the first frame update
    void Start() {
        currentMap = PlayerPrefs.GetString("CurrentMap");
        stars = new[]
        {
            transform.Find("Star_1").gameObject,
            transform.Find("Star_2").gameObject,
            transform.Find("Star_3").gameObject
        };
        bt = GetComponent<Button>();
        bt.enabled = false;
        image = GetComponent<Image>();//获取Image组件
        //解锁第一个关卡
        if (transform.parent.GetChild(0).name == gameObject.name) {
            canSelect = true;
        }
        //非第一关关卡
        else {
            int lastLevelIndex = Convert.ToInt32(gameObject.name) - 1;//上一关关卡索引
            //上一关卡通过,解锁本关卡
            //if (PlayerPrefs.GetInt("Level" + lastLevelIndex + "Pass") == 1) {
            //   canSelect = true;
            //}
            //若上一关关卡的星数大于0,代表上一关通关
            if (PlayerPrefs.GetInt("Level" + lastLevelIndex+"Of"+currentMap)>0) {
                canSelect = true;
            }
        }
        //若可以选择关卡,将图片替换成解锁图片
        if (canSelect) {
            bt.enabled = true;
            image.sprite = unlockSprite;
            //levelNum.SetActive(true);
            transform.Find("Num").gameObject.SetActive(true);
            int count = PlayerPrefs.GetInt("Level"+gameObject.name+"Of"+currentMap);
            for (int i = 0; i < count; i++) {
                stars[i].SetActive(true);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //关卡选择存储
    public void Select() {
        if (canSelect) {
            //存储当前关卡的名字编号
            PlayerPrefs.SetString("CurrentLevel","Level"+levelNum.GetComponent<Text>().text+ "Of"+currentMap);
            //加载具体关卡场景信息
            SceneManager.LoadScene(2);
        }
    }
}

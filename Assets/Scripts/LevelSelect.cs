using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
    public bool canSelect = false;
    public Image image;
    public Sprite unlockSprite;

    public GameObject levelNum;
    // Start is called before the first frame update
    void Start() {
        image = GetComponent<Image>();//获取Image组件
        //解锁第一个关卡
        if (transform.parent.GetChild(0).name == gameObject.name) {
            canSelect = true;
        }
        //若可以选择关卡,将图片替换成解锁图片
        if (canSelect) {
            image.sprite = unlockSprite;
            //levelNum.SetActive(true);
            GameObject.Find("Num").gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
    public bool canSelect = false;
    public Image image;
    public Sprite unlockSprite;
    // Start is called before the first frame update
    void Start() {
        image = GetComponent<Image>();
        //解锁第一个关卡
        if (transform.parent.GetChild(0).name == gameObject.name) {
            canSelect = true;
        }

        if (canSelect) {
            image.sprite = unlockSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

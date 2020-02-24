using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
    private bool isClick;
    // Start is called before the first frame update
    void Start() {
        isClick = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClick) {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//将鼠标坐标转化到屏幕空间坐标系
            //1.将坐标的z轴限定为0
            //transform.position = new Vector3(transform.position.x,transform.position.y,0);
            //2.坐标减去相机的z轴坐标
            transform.position -= new Vector3(0,0, Camera.main.transform.position.z);


        }
    }
    //鼠标按下
    void OnMouseDown() {
        isClick = true;
    }
    //鼠标抬起
    void OnMouseUp() {
        isClick = false;
    }
}

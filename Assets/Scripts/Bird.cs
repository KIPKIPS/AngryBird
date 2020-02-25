using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
    private bool isClick;
    public Vector3 launchPos;
    public float maxDis;

    public SpringJoint2D sj2d;
    public Rigidbody2D r2d;

    public LineRenderer lrRight;
    public LineRenderer lrLeft;
    public Transform rightPos;
    public Transform leftPos;
    void Awake() {
        sj2d = GetComponent<SpringJoint2D>();
        r2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start() {
        isClick = false;
        launchPos = GameObject.Find("LaunchPos").transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (isClick) {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//将鼠标坐标转化到屏幕空间坐标系
            //1.将坐标的z轴限定为0
            //transform.position = new Vector3(transform.position.x,transform.position.y,0);
            //2.坐标减去相机的z轴坐标
            transform.position -= new Vector3(0, 0, Camera.main.transform.position.z);

            //位置限定
            //若对象的位置距离发射位置大于maxDis
            if (Vector3.Distance(transform.position, launchPos) > maxDis) {
                //发射位置指向对象位置的方向向量(归一化)
                Vector3 dir = (transform.position - launchPos).normalized;
                dir *= maxDis;//方向乘距离得到长度向量
                transform.position = dir + launchPos;//发射位置+长度向量得到对象被限定的位置坐标
            }
            //绘制皮筋
            DrawLine();
        }
    }
    //鼠标按下
    void OnMouseDown() {
        isClick = true;
        //接受物理影响
        r2d.isKinematic = true;
    }
    //鼠标抬起
    void OnMouseUp() {
        isClick = false;
        //不接受物理影响
        r2d.isKinematic = false;
        //延迟调用,等待物理计算完成之后再将springJoint失效
        Invoke("Fly", 0.1f);
        
    }

    void Fly() {
        //springJoint失效
        sj2d.enabled = false;
    }
    //RigidBody的Angular Drag值代表旋转衰减,阻力(空气阻力)

    //绘制橡皮筋
    void DrawLine() {
        //lrRight.SetPosition(0,rightPos.position);
        //lrRight.SetPosition(1,transform.position);
        //lrLeft.SetPosition(0,leftPos.position);
        //lrLeft.SetPosition(1,transform.position);

        lrRight.SetPositions(new[] { rightPos.position, transform.position });
        lrLeft.SetPositions(new[] { leftPos.position, transform.position });
    }
}

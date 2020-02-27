using System;
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
    public GameObject boom;
    public bool isFly;
    public bool canMove = true;

    public WeaponTrail trail;

    private float currTime=0;
    void Awake() {
        trail = GetComponent<Trails>().trail;
        sj2d = GetComponent<SpringJoint2D>();
        r2d = GetComponent<Rigidbody2D>();
        sj2d.connectedBody = GameObject.Find("Right").GetComponent<Rigidbody2D>();
        lrRight = GameObject.Find("Right").GetComponent<LineRenderer>();
        lrLeft = GameObject.Find("Left").GetComponent<LineRenderer>();
        rightPos = GameObject.Find("RightPos").transform;
        leftPos = GameObject.Find("LeftPos").transform;
    }
    // Start is called before the first frame update
    void Start() {
        isFly = false;
        isClick = false;
        launchPos = GameObject.Find("LaunchPos").transform.position;
        //默认没有拖尾
        trail.SetTime(0.0f, 0.0f, 1.0f);
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

        if (isFly) {
            currTime += Time.deltaTime;
            //Debug.Log(currTime);
            if (currTime > 4f) {
                DestroyMyself();
            }
        }
    }
    //鼠标按下
    void OnMouseDown() {
        if (canMove) {
            isClick = true;
            //接受物理影响
            r2d.isKinematic = true;
        }
        
    }
    //鼠标抬起
    void OnMouseUp() {
        if (canMove) {
            isClick = false;
            //不接受物理影响
            r2d.isKinematic = false;
            //延迟调用,等待物理计算完成之后再将springJoint失效
            Invoke("Fly", 0.1f);
            
            //禁用绘制橡皮筋
            lrRight.enabled = false;
            lrLeft.enabled = false;
            canMove = false;

            //Debug.Log(currTime);
        }
    }

    void Fly() {
        isFly = true;
        //设置拖尾时长
        trail.SetTime(0.2f, 0.0f, 1.0f);
        //开始进行拖尾
        trail.StartTrail(0.5f, 0.4f);

        //springJoint失效
        sj2d.enabled = false;
        
    }
    //RigidBody的Angular Drag值代表旋转衰减,阻力(空气阻力)

    //绘制橡皮筋
    void DrawLine() {
        
        //激活绘制橡皮筋
        lrRight.enabled = true;
        lrLeft.enabled = true;
        //绘制
        lrRight.SetPositions(new[] { rightPos.position, transform.position });
        lrLeft.SetPositions(new[] { leftPos.position, transform.position });
        //lrRight.SetPosition(0,rightPos.position);
        //lrRight.SetPosition(1,transform.position);
        //lrLeft.SetPosition(0,leftPos.position);
        //lrLeft.SetPosition(1,transform.position);
    }

    //销毁自身
    void DestroyMyself() {
        GameManager.instance.birds.Remove(this);
        Instantiate(boom, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        //下一只鸟上架
        GameManager.instance.NextBird();
    }
    //小鸟碰到物体就取消拖尾
    void OnCollisionEnter2D(Collision2D collision) {
        trail.ClearTrail();
        if (collision.transform.tag=="Enemy"|| collision.transform.tag == "Ground" && isFly) {
            Invoke("DestroyMyself", 3);
        }
    }
}

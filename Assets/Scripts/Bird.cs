using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
    public Vector3 currPos;
    public bool isClick;
    public Vector3 launchPos;//弹弓发射位置
    public float maxDis;//皮筋最大距离
    public SpriteRenderer sr;
    public SpringJoint2D sj2d;
    public Rigidbody2D r2d;

    public LineRenderer lrRight;
    public LineRenderer lrLeft;
    public Transform rightPos;//右支架
    public Transform leftPos;//左支架
    public GameObject boom;//爆炸特效
    public bool isFly;
    public bool canMove = true;

    public WeaponTrail trail;
    public float smooth = 3;

    public AudioClip select;
    public AudioClip fly;
    public Sprite yellowSpeedUp;
    public bool launch = false;

    public Sprite redHurt;
    public Sprite yellowHurt;
    public Sprite greenHurt;
    public Sprite blackHurt;
    public bool onGround = true;
    public enum BirdType {
        Red, Yellow, Black, Green
    }
    public BirdType bt;
    public void Awake() {
        onGround = true;
        sr = GetComponent<SpriteRenderer>();
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
    public void Start() {
        isFly = false;
        isClick = false;
        launchPos = GameObject.Find("LaunchPos").transform.position;
        //默认没有拖尾
        trail.SetTime(0.0f, 0.0f, 1.0f);
    }

    // Update is called once per frame
    public void Update() {
        currPos = transform.position;
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
        //相机跟随
        float posX = transform.position.x;//小鸟位置
        //Debug.Log(transform.name+" "+posX);
        //目标位置,x范围限定在0-15之间
        Vector3 tarPos = new Vector3(Mathf.Clamp(posX, 0, 15), Camera.main.transform.position.y, Camera.main.transform.position.z);
        //平滑位置
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, tarPos, Time.deltaTime * smooth);

        //小鸟技能
        if (isFly && Input.GetMouseButtonDown(0)) {
            if (bt == BirdType.Yellow) {
                DirectionalSpeedUpSkill();
            }
            if (bt == BirdType.Green) {
                BoomerangSkill();
            }
            if (bt == BirdType.Black) {
                BoomSkill();
            }
        }
    }
    //黑色小鸟的爆炸技能
    public virtual void BoomSkill() {

    }
    //绿色小鸟的回旋技能
    public void BoomerangSkill() {
        isFly = false;
        GameObject bo = Instantiate(boom, transform.position, Quaternion.identity);
        bo.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        r2d.velocity = new Vector2(-r2d.velocity.x * 1.5f, r2d.velocity.y * 0.5f);
    }

    //黄色小鸟的定向加速技能
    public void DirectionalSpeedUpSkill() {
        isFly = false;
        GameObject bo = Instantiate(boom, transform.position, Quaternion.identity);
        bo.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        sr.sprite = yellowSpeedUp;
        r2d.velocity *= 2.2f;
    }
    //鼠标按下
    public void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)&&onGround==false) {
            AudioPlay(@select);

            if (canMove) {
                isClick = true;
                //接受物理影响
                r2d.isKinematic = true;
            }
        }
    }
    //鼠标抬起
    public void OnMouseUp() {
        if (Input.GetMouseButtonUp(0)&&onGround==false) {
            launch = true;
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

    }

    public void Fly() {
        AudioPlay(fly);

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
    public void DrawLine() {

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
    public void DestroyMyself() {
        GameManager.instance.birds.Remove(this);
        Instantiate(boom, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        //下一只鸟上架
        GameManager.instance.NextBird();
    }
    //小鸟碰到物体就取消拖尾
    public void OnCollisionEnter2D(Collision2D collision) {
        trail.ClearTrail();
        if (collision.transform.tag == "Enemy" || collision.transform.tag == "Ground" && launch) {
            isFly = false;
            switch (bt) {
                case BirdType.Red: sr.sprite = redHurt; break;
                case BirdType.Yellow: sr.sprite = yellowHurt; break;
                case BirdType.Green: sr.sprite = greenHurt; break;
                case BirdType.Black: sr.sprite = blackHurt; break;
            }
            Invoke("DestroyMyself", 3);
        }
    }
    public void AudioPlay(AudioClip ac) {
        AudioSource.PlayClipAtPoint(ac, transform.position);
    }

}

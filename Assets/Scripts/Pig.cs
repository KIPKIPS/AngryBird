using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {
    //鸟的速度大于10,则绿皮猪死亡,大于5小于10则受伤
    public float maxSpeed = 10;
    public float minSpeed = 5;

    public SpriteRenderer sr;
    public Sprite pigHurtSprite;

    public bool isHurt;
    // Start is called before the first frame update
    void Start() {
        sr = GetComponent<SpriteRenderer>();
        isHurt = false;
    }

    // Update is called once per frame
    void Update() {

    }
    //碰撞检测
    private void OnCollisionEnter2D(Collision2D collision) {
        //collision.relativeVelocity表示相对速度(向量),magnitude表示该向量的模长
        //死亡
        if (collision.relativeVelocity.magnitude > maxSpeed) {
            Destroy(this.gameObject);
        }
        //受伤
        else if (collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed) {
            sr.sprite = pigHurtSprite;
            isHurt = true;
        }

    }
}

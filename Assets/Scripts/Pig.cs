using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {
    //鸟的速度大于10,则绿皮猪死亡,大于5小于10则受伤
    public float maxSpeed = 10;
    public float minSpeed = 5;

    public SpriteRenderer sr;
    public Sprite hurtSprite;

    public bool isHurt;
    public GameObject boom;
    public GameObject score;

    public bool isPig=false;

    public AudioClip dead;
    public AudioClip hurt;
    public AudioClip birdCollision;
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
            PigDead();
        }
        //受伤
        else if (collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed) {
            sr.sprite = hurtSprite;
            isHurt = true;
            AudioPlay(hurt);
        }

        if (collision.transform.tag=="Player") {
            AudioPlay(birdCollision);
        }

    }
    //绿皮猪死亡之后的操作
    public void PigDead() {
        if (isPig) {
            GameManager.instance.pigs.Remove(this);
            AudioPlay(dead);
        }
        Instantiate(boom,transform.position,Quaternion.identity);
        GameObject s=Instantiate(score, transform.position+new Vector3(0,0.8f,0), Quaternion.identity);
        Destroy(s,1f);
        Destroy(this.gameObject);
    }
    public void AudioPlay(AudioClip ac) {
        AudioSource.PlayClipAtPoint(ac, transform.position);
    }
}

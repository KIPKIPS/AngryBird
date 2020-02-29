using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird
{
    private List<Pig> blocks = new List<Pig>();
    public AudioClip ac;
    public GameObject boomBird;
    // Start is called before the first frame update
    //触发爆炸范围圈,将游戏物体的Pig脚本添加到销毁列表
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag=="Enemy") {
            blocks.Add(collision.transform.GetComponent<Pig>());
        }
    }
    void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Enemy") {
            blocks.Remove(collision.transform.GetComponent<Pig>());
        }
    }

    public override void BoomSkill() {
        Debug.Log(currPos);
        Instantiate(boomBird, currPos, Quaternion.identity);
        isFly = false;
        AudioSource.PlayClipAtPoint(ac,transform.position);
        foreach (var VARIABLE in blocks) {
            VARIABLE.PigDead();
        }
        DestroyMyself();
    }
    public new void OnCollisionEnter2D(Collision2D collision) {
        trail.ClearTrail();
        if (collision.transform.tag == "Enemy" || collision.transform.tag == "Ground" && launch) {
            isFly = false;
            switch (bt) {
                case BirdType.Red: sr.sprite = redHurt; break;
                case BirdType.Yellow: sr.sprite = yellowHurt; break;
                case BirdType.Green: sr.sprite = greenHurt; break;
                case BirdType.Black: BoomBird(); break;
            }
            Invoke("DestroyMyself", 3);
        }
    }

    void BoomBird() {
        Debug.Log("boom");
    }
}

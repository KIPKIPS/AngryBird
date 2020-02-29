using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird
{
    private List<Pig> blocks = new List<Pig>();
    public AudioClip ac;
    public GameObject boomBird;

    public Sprite exp1;
    public Sprite exp2;
    public Sprite exp3;

    private IEnumerator ie;
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
                case BirdType.Black:
                    ie = BoomBird();
                    StartCoroutine(ie);
                    Invoke("BoomSkill", 2.5f);
                    break;
            }
        }
    }

    IEnumerator BoomBird() {
        yield return new WaitForSeconds(2.5f/3f);
        sr.sprite = exp1;
        yield return new WaitForSeconds(5f / 3f);
        sr.sprite = exp2;
        yield return new WaitForSeconds(7.5f / 3f);
        sr.sprite = exp3;
    }
    public new void DestroyMyself() {
        GameManager.instance.birds.Remove(this);
        if (ie!=null) {
            StopCoroutine(ie);
        }
        Destroy(this.gameObject);
        //下一只鸟上架
        GameManager.instance.NextBird();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour {
    public Animator anim;
    public GameObject pauseButton;
    void Awake() {
        anim = GetComponent<Animator>();
        //覆盖全局的UI不与鼠标交互
        transform.Find("All").GetComponent<Image>().raycastTarget = false;
        transform.Find("All").transform.Find("LeftPopWindow").GetComponent<Image>().raycastTarget = false;
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
    }
    public void Pause() {
        anim.SetBool("isPause", true);
    }

    public void Resume() {
        Time.timeScale = 1;
        anim.SetBool("isPause", false);
        //场景中是否还有鸟
        if (GameManager.instance.birds.Count > 0) {
            //若弹弓架上的鸟还没有飞(未发射状态)
            if (GameManager.instance.birds[0].isReleased == false) {
                //不可移动
                GameManager.instance.birds[0].canMove = true;
            }
        }
    }

    public void Retry() {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }

    //动画完成事件
    //pause动画事件
    public void PauseAnimStart() {
        pauseButton.SetActive(false);
    }
    public void PauseAnimEnd() {
        //场景中是否还有鸟
        if (GameManager.instance.birds.Count > 0) {
            //若弹弓架上的鸟还没有飞(未发射状态)
            if (GameManager.instance.birds[0].isReleased == false) {
                //不可移动
                GameManager.instance.birds[0].canMove = false;
            }
        }
        Time.timeScale = 0;
    }
    //resume动画事件
    public void ResumeAnimEnd() {
        pauseButton.SetActive(true);
    }
}
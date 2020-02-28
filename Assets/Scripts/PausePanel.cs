using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour {
    public Animator anim;
    public GameObject pauseButton;
    void Awake() {
        anim = GetComponent<Animator>();
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
    }

    public void Retry() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    //动画完成事件
    //pause动画事件
    public void PauseAnimStart() {
        pauseButton.SetActive(false);
    }
    public void PauseAnimEnd() {
        Time.timeScale = 0;
    }
    //resume动画事件
    public void ResumeAnimEnd() {
        pauseButton.SetActive(true);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour {
    public Animator anim;

    void Awake() {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Pause() {
        anim.SetBool("isPause",true);
    }

    public void Continue() {
        anim.SetBool("isPause", false);
    }

    public void Retry() {
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMapAsync : MonoBehaviour {
    public GameObject progressBar;
    public RectTransform rt;
    IEnumerator ie;
    void Awake() {
        progressBar=GameObject.Find("ProgressBar");
        rt = progressBar.GetComponent<RectTransform>();
        rt.position=new Vector3(-800,0,0);
    }

    // Start is called before the first frame update
    void Start() {
        ie = LoadMapScene(2);
        StartCoroutine(ie);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadMapScene(float time) {
        int init = -800;
        for (int i = 0; i <= 100; i++) {
            rt.position = new Vector3(init, 0, 0);
            init += 8;
            yield return new WaitForSeconds(time/100);
        }
        //异步加载地图场景
        SceneManager.LoadSceneAsync(1);
        if (ie!=null) {
            StopCoroutine(ie);
        }
        
    }
}

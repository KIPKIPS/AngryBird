using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public List<Bird> birds;
    public List<Pig> pigs;
    public static GameManager instance;

    public Vector3 originPos;//初始位置
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject pausePanel;
    public int birdCount;
    public int pigCount;
    public GameObject[] stars;

    public int starCount = 0;//当前关卡星星数量得分
    private int totalLevel=4;
    void Initialized() {
        for (int i = 0; i < birds.Count; i++) {
            //第一只鸟的Bird脚本激活,SpringJoint2D组件激活
            if (i == 0) {
                //将第一个小鸟的位置置为初始位置 
                birds[i].gameObject.transform.position = originPos;
                birds[i].onGround = false;
                birds[i].enabled = true;
                birds[i].sj2d.enabled = true;
            }
            else {
                birds[i].enabled = false;
                birds[i].sj2d.enabled = false;
            }
        }
    }
    void Awake() {
        instance = this;
        //若场景的小鸟个数大于0,将索引0处的小鸟位置记录为初始位置
        if (birds.Count > 0) {
            originPos = birds[0].transform.position;
        }
    }
    // Start is called before the first frame update
    void Start() {
        Initialized();
        birdCount = birds.Count;
        pigCount = pigs.Count;
    }

    // Update is called once per frame
    void Update() {

    }

    public void NextBird() {
        if (pigs.Count > 0) {
            if (birds.Count > 0) {
                //下一只鸟上弹弓架
                Initialized();
            }
            else {
                losePanel.SetActive(true);
            }
        }
        else {
            //win
            winPanel.SetActive(true);
        }
    }

    public void DisplayStars() {
        StartCoroutine("Stars");
    }

    public IEnumerator Stars() {
        if (birds.Count==birdCount-pigCount) {
            starCount = 2;
        }
        else {
            starCount = birds.Count > birdCount - pigCount ? 3 : 1;
        }
        for (int i = 0; i < starCount; i++) {
            yield return new WaitForSeconds(0.5f);
            stars[i].SetActive(true);
        }
        //显示星星数量表示该关卡已经完成,此时存储得分
        DataSave();
    }
    //存储得分数量
    public void DataSave() {
        //保存当前正在玩的关卡的得分
        string currentLevel = PlayerPrefs.GetString("CurrentLevel");
        string index = currentLevel.Substring(5);

        PlayerPrefs.SetInt("Level"+index+"Pass",1);//设置当前关卡的通关状态

        int historyScore = PlayerPrefs.GetInt(currentLevel);//历史最高分数
        //打破记录,更新分数显示
        if (historyScore<starCount) {
            PlayerPrefs.SetInt(currentLevel, starCount);
        }
        //计算一个地图中总关卡的星数
        int sum = 0;
        for (int i = 1; i <= totalLevel; i++) {
            sum+=PlayerPrefs.GetInt(PlayerPrefs.GetString("Level" + i));
        }
        PlayerPrefs.SetInt("totalNumOfStarInMap",sum);
    }

    public void Pause() {
        pausePanel.GetComponent<Animator>().SetBool("isPause",true);
    }

    public void Retry() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Home() {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
}

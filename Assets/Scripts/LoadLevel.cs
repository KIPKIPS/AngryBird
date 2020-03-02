using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour {
    void Awake() {
        GameObject currentLevel=Resources.Load<GameObject>("Levels/"+PlayerPrefs.GetString("CurrentLevel"));
        Instantiate(currentLevel, Vector3.zero, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

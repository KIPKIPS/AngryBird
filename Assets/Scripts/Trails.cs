using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trails : MonoBehaviour
{
    public WeaponTrail trail;
    private float t = 0.033f;
    private float tempT = 0;
    private float animationIncrement = 0.003f;

    void Start() {
        // 默认没有拖尾效果
        trail.SetTime(0.0f, 0.0f, 1.0f);
    }
    void LateUpdate() {
        t = Mathf.Clamp(Time.deltaTime, 0, 0.066f);

        if (t > 0) {
            while (tempT < t) {
                tempT += animationIncrement;

                if (trail.time > 0) {
                    trail.Itterate(Time.time - t + tempT);
                }
                else {
                    trail.ClearTrail();
                }
            }

            tempT -= t;

            if (trail.time > 0) {
                trail.UpdateTrail(Time.time, t);
            }
        }
    }
}

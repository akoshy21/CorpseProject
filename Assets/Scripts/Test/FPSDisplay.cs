using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour {

    int frameCount = 0;
    float dt = 0.0f;
    float fps = 0.0f;
    float updateRate = 4.0f;  // 4 updates per sec.
    TMP_Text displayText;

    private void Start() {
        displayText = GetComponentInChildren<TMP_Text>();
    }

    void Update() {
        frameCount++;
        dt += Time.unscaledDeltaTime;
        if (dt > 1.0f / updateRate) {
            fps = frameCount / dt;
            frameCount = 0;
            dt -= 1.0f / updateRate;
        }
        displayText.text = Mathf.Round(fps).ToString() + "FPS";
    }


}

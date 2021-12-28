using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Text fpsCounter;
    void Start()
    {
        fpsCounter = GameObject.Find("Fps Counter").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        fpsCounter.text = $"FPS: {Mathf.Round(1f / Time.smoothDeltaTime)}";
    }
}

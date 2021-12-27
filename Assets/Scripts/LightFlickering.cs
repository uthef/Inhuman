using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    public float[] intensityRange = { 0f, 0.5f, 2.5f, 4f, 5.4f };

    public float FlickeringTime = 5;
    public float DarknessTime = 3;

    float countdown, countdown2;
    void Start()
    {
        countdown = FlickeringTime;
        countdown2 = DarknessTime;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown > 0)
        {
            GetComponent<Light>().intensity = intensityRange[Random.Range(0, intensityRange.Length)];
        }
        else
        {
            GetComponent<Light>().intensity = 0;
            if (countdown2 > 0) countdown2 -= Time.deltaTime;
            else
            {
                countdown2 = DarknessTime;
                countdown = FlickeringTime;
            }
        }
    }
}

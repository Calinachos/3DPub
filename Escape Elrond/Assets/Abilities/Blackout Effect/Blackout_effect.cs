using System.Collections;
using System.Collections.Generic;
using UB.Simple2dWeatherEffects.Standard;
using UnityEngine;

public class Blackout_effect : MonoBehaviour
{
    public Camera cam;
    public float timer = 5f;
    private bool enabled = false;
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            if (!enabled)
            {
                blackoutSet();
                Invoke("blackoutReset", timer);
            }
        }
    }

    void blackoutSet()
    {
        enabled = true;
        cam.GetComponent<D2FogsPE>().Density = 0.8f;
    }
    void blackoutReset()
    {
        enabled = false;
        cam.GetComponent<D2FogsPE>().Density = 0f;
    }
}

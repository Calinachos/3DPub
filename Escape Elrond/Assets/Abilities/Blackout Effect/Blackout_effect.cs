using System.Collections;
using System.Collections.Generic;
using UB.Simple2dWeatherEffects.Standard;
using UnityEngine;

public class Blackout_effect : MonoBehaviour
{
    public Camera cam;
    public float timer = 5f;
    private bool enabled = false;
    private Skills blackout;
    public AudioSource blackoutSound;

    private void Start()
    {
        GameObject tree = GameObject.Find("Skill_Tree_Canvas(Clone)").transform.GetChild(0).gameObject;
        GameObject background = tree.transform.GetChild(0).gameObject;
        GameObject content = background.transform.GetChild(1).gameObject;
        GameObject tier1 = content.transform.GetChild(0).gameObject;
        GameObject abilities = tier1.transform.GetChild(0).gameObject;
        GameObject step = abilities.transform.GetChild(0).gameObject;
        blackout = step.GetComponent<Skills>();
    }
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            if (!enabled && blackout.skillAvailable)
            {
                blackoutSound.Play();
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

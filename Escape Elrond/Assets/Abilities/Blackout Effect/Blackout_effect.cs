using System.Collections;
using System.Collections.Generic;
using UB.Simple2dWeatherEffects.Standard;
using UnityEngine;


    public class Blackout_effect : MonoBehaviour
{
    public Camera cam;
    public float timer = 5f;
    public float transitionTimeOnEntry = 2f;
    public float transitionTimeOnExit = 0.5f;
    private int effectEnabled = 0;         // 0 = no effect; 1 = apply effect; 2 = remove effect
    private float elapsedTime = 0f;
    private float darkness = 0.8f;
    private float lightness = 0f;
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
        if (blackout.skillAvailable)
        {
            if (effectEnabled == 0)
            {
                if (Input.GetKeyDown("1"))
                {
                    Entity[] enemies = GameObject.FindObjectsOfType<Entity>();
                    foreach (Entity e in enemies)
                    {
                        e.entityData.maxAgroDistance /= 2.0f;
                        e.entityData.minAgroDistance /= 2.0f;
                    }
                    blackoutSound.Play();
                    blackoutSet();
                    Invoke("blackoutReset", timer);
                }
            }
            else if (effectEnabled == 1)
            {
                if (elapsedTime + Time.deltaTime < transitionTimeOnEntry)
                {
                    elapsedTime += Time.deltaTime;
                    cam.GetComponent<D2FogsPE>().Density += Time.deltaTime * (darkness / transitionTimeOnEntry);
                }
                else
                {
                    elapsedTime = transitionTimeOnEntry;                           //transition finished
                    cam.GetComponent<D2FogsPE>().Density = darkness;            //complete darkness
                }
            }
            else
            {
                if (elapsedTime + Time.deltaTime < transitionTimeOnExit)
                {
                    elapsedTime += Time.deltaTime;
                    cam.GetComponent<D2FogsPE>().Density -= Time.deltaTime * (darkness / transitionTimeOnExit);
                }
                else
                {
                    Debug.Log("hello");
                    Entity[] enemies = GameObject.FindObjectsOfType<Entity>();
                    foreach (Entity e in enemies)
                    {
                        e.entityData.maxAgroDistance *= 2.0f;
                        e.entityData.minAgroDistance *= 2.0f;
                    }
                    elapsedTime = 0f;                                       //transition finished
                    cam.GetComponent<D2FogsPE>().Density = lightness;              //complete ligthness
                    effectEnabled = 0;                                      //effect completed
                }
            }
        }
    }

    void blackoutSet()
    {
        effectEnabled = 1;
        elapsedTime = 0f;
    }
    void blackoutReset()
    {
        effectEnabled = 2;
        elapsedTime = 0f;
    }
}

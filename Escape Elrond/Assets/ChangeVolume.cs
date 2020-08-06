using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    public Slider volume;
    // Start is called before the first frame update
    void Start()
    {
        volume.value = PlayerPrefs.GetFloat("volume");
    }
    public void changeVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("volume", newVolume);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }

    // Update is called once per frame
    void Update()
    {
        changeVolume(volume.value);
    }
}

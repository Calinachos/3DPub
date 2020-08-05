using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public AudioSource hoverSound;
    public AudioSource clickSound;

    public void OnPointerEnter(PointerEventData ped)
    {
        if (!hoverSound.isPlaying)
            hoverSound.Play();
    }
    public void OnPointerDown(PointerEventData ped)
    {
        if (!clickSound.isPlaying)
            clickSound.Play();
    }
}

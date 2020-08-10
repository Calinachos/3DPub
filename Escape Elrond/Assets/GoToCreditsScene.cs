using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToCreditsScene : MonoBehaviour
{
    private bool isTouched = false;
    public int nextLevel = 13;
    private GameObject toolTip = null;
    public AudioSource victoryMusic;
    public bool victory = false;

    void Awake()
    {
        // Find the text tooltip
        Transform toolTipTranform = gameObject.transform.Find("ToolTip");
        if (toolTipTranform != null)
        {
            toolTip = toolTipTranform.gameObject;
            toolTip.SetActive(false);
        }
        else
        {
            Debug.Log("ToolTip not found for door " + gameObject.name);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        // Door has been touched
        if (col.gameObject.tag == "Player")
        {
            isTouched = true;
            if (toolTip != null)
            {
                toolTip.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {

        // Door isn't touched anymore
        if (col.gameObject.tag == "Player")
        {
            isTouched = false;
            if (toolTip != null)
            {
                toolTip.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (isTouched && Input.GetKey(KeyCode.E) && !victory)
        {
            GameObject tree = GameObject.Find("Skill_Tree_Canvas(Clone)");
            tree.GetComponent<AudioSource>().Stop();
            victoryMusic.Play();
            victory = true;
        }
        if (victory && !victoryMusic.isPlaying)
        {
            if (nextLevel == -1)
            {
                Debug.LogWarning("Level or Door not setup");
                return;
            }
            SceneManager.LoadScene(nextLevel);
        }
    }
}

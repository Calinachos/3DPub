using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTalk : MonoBehaviour
{
    public TextAsset theText;
    public int startLine;
    public int endLine;

    public TextBoxManager theTextBox;
    public TextBoxManager theInstructionBox; 
    
    public bool destroyWhenActivated;
    public bool requireButton;
    private bool waitForButton;

    void Start()
    {
        theTextBox = FindObjectOfType<TextBoxManager>();
        theInstructionBox = FindObjectOfType<TextBoxManager>();
        if(endLine == 0)
            endLine = (theText.text.Split('\n')).Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(waitForButton && Input.GetKeyUp(KeyCode.E))
        {
            waitForButton = false;
            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.finalLine = endLine;
            theTextBox.EnableTextBox();

            if(destroyWhenActivated)
                Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            if(requireButton)
            {
                waitForButton = true;
                return;
            }
            
            theInstructionBox.ReloadScript(theText);
            theInstructionBox.currentLine = startLine;
            theInstructionBox.finalLine = endLine;
            theInstructionBox.EnableTextBox();

            if(destroyWhenActivated)
                Destroy(gameObject);
               
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            waitForButton = false;
        }
    }
    
}

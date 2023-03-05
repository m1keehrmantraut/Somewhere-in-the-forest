using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueButton;   
    public bool playerIsClose;
    [SerializeField]
    private DialogueManager manager;

    private void FixedUpdate()
    {
        if (playerIsClose)
        {
            dialogueButton.SetActive(true);
        }
        else
        {
            dialogueButton.SetActive(false);
            manager.animator.SetBool("IsOpen", false);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}

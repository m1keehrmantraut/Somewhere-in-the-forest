using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource myFx;
    [SerializeField] private AudioClip click;

    public void ClickSound()
    {
        myFx.PlayOneShot(click);
    }
}

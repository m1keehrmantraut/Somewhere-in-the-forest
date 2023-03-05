using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private LevelDisplay levelDisplay;
    public void Awake()
    {
        var levelSystem = new LevelSystem();
        levelDisplay.SetLevelSystem(levelSystem);
        
    }
}

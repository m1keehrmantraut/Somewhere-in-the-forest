using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDead : MonoBehaviour
{
    [SerializeField] private GameObject dead;

    public void Spawn(Transform enemy)
    {
        Instantiate(dead, enemy.position, enemy.rotation);
    }
}

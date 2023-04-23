using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExperiencePoints : MonoBehaviour
{
    [SerializeField] private TMP_Text PointsText;
    
    private int experience;
    //dsfasdf
    public void ChangeExperience(int count)
    {
        experience += count;
        PointsText.text = experience.ToString();
    }
}

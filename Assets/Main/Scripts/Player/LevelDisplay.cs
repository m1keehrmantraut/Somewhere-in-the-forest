using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider expBar;
    public LevelSystem levelSystem;

    private void SetExperienceBarSize(float experienceNormalized)
    {
        expBar.value += experienceNormalized;
    }

    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = (levelNumber + 1) + "lvl";
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
        
        SetLevelNumber(levelSystem.GetLevelNumber());
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());

        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, EventArgs e)
    {
        SetLevelNumber(levelSystem.GetLevelNumber());
    }

    private void LevelSystem_OnExperienceChanged(object sender, EventArgs e)
    {
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
    }
}

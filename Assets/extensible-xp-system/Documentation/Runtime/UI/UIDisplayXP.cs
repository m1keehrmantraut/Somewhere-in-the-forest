using UnityEngine;
using UnityEngine.UI;
// An example script that displays current XP, level, etc. Also creates a list of levels and their XP requirements.

namespace Saucy.Modules.XP {
  public class UIDisplayXP : MonoBehaviour {
    // The GameObject that has a reference to XP Calculation method.
    [SerializeField] private GameObject player = null;

    // UI Text reference to the XP Calculation method.
    [Space] [SerializeField] private Text xpMethodName = null;

    // UI Text references.
    [SerializeField] private Text currentXP = null;
    [SerializeField] private Text maxXP = null;
    [SerializeField] private Text acquiredXP = null;
    [SerializeField] private Text requiredXP = null;
    [SerializeField] private Text previouslyRequiredXP = null;
    [SerializeField] private Text level = null;
    [SerializeField] private Text maxLevel = null;
    [SerializeField] private Text maxLevelReached = null; // Text to display max level being reached.

    // We're using a sliders because it can easily show progress by just setting the value of the slider (values 0-1).
    [SerializeField] private Slider progress = null; // Level progress.
    [SerializeField] private Slider progressOverall = null; // Overall level progress (starting level -> max level).

    // Prefab or a list "row" we can instantiate and set the text to values that we have in playerSavedXP.
    [Space] [SerializeField] private GameObject listPrefab = null;
    // The container where to instantiate the listPrefab at.
    [SerializeField] private GameObject listContainer = null;
    // Colors for the "row". So we more easily can see them.
    [SerializeField] private Color listBackgroundColorEven = Color.black;
    [SerializeField] private Color listBackgroundColorOdd = Color.gray;

    // Reference to the XP calculation method for easy access.
    private DataXPReceive playerSavedXP;

    private void Awake () {
      // Get a reference to the XP calculation method being used.
      // We can call IXPReceive interface because every script that implements it always have a XPCalculationMethod field.
      playerSavedXP = player.GetComponent<IXPReceive>().XPCalculationMethod;
    }

    private void Start () {
      // Hide the "Max level reached!"-text.
      maxLevelReached.enabled = false;

      // Sets the XP calculation method (ScriptableObject) name.
      xpMethodName.text = playerSavedXP.name;

      // Update all values so we are in sync from the start.
      SetAllXPValues();

      // Fill the list so we can see all available levels (from starting level -> max level).
      FillList();
    }

    private void FillList () {
      // Here we list all available levels, creating a new row for each level.
      for (int _level = 1; _level <= playerSavedXP.MaxLevel; _level++) {
        // Create a prefab as a child on listContainer.
        GameObject _clone = (GameObject) Instantiate(listPrefab, listContainer.transform);
        // We want to set the values dynamically so we get a reference to the UITextSetter to more easily set them.
        UITextSetter _textScript = (UITextSetter) _clone.GetComponent<UITextSetter>();

        // Here we check if level is even or odd, and set the corresponding background color to each.
        if (_level % 2 == 0) {
          _textScript.SetImageColor(listBackgroundColorEven);
        } else {
          _textScript.SetImageColor(listBackgroundColorOdd);
        }

        // If we aren't at max level we want to set the values for the texts.
        if (_level != playerSavedXP.MaxLevel) {
          _textScript.SetText(
            _level.ToString(), // The current level
            playerSavedXP.SelectedXPFormula.StartingXP(_level, playerSavedXP).ToString(), // Then the starting XP where we start from when we just level up.
            playerSavedXP.SelectedXPFormula.RequiredXP(_level).ToString(), // The required XP to level up.
            playerSavedXP.SelectedXPFormula.DifferenceXP(_level, playerSavedXP).ToString(), // The difference between the starting XP and required XP.
            playerSavedXP.SelectedXPFormula.TotalXP(_level, playerSavedXP).ToString() // To total XP that need to be acquired to level up.
          );
        } else {
          // If we're at max level we just skip setting the values because we cannot level up further than max level.
          _textScript.SetText(
            _level.ToString(),
            "-",
            "-",
            "-",
            "-"
          );
        }
      }
    }

    private void SetAllXPValues () {
      // Sets the text fields to the corresponding XP values.
      currentXP.text = playerSavedXP.CurrentXP.ToString();
      maxXP.text = playerSavedXP.MaxXP.ToString();
      acquiredXP.text = playerSavedXP.AcquiredXP.ToString();
      requiredXP.text = playerSavedXP.RequiredXP.ToString();
      previouslyRequiredXP.text = playerSavedXP.PreviouslyRequiredXP.ToString();

      level.text = playerSavedXP.Level.ToString();
      maxLevel.text = playerSavedXP.MaxLevel.ToString();

      progress.value = playerSavedXP.Progress;
      progressOverall.value = playerSavedXP.ProgressOverall;
    }

    public void OnXPChanged () {
      // Update all values so we are in sync.
      SetAllXPValues();
    }

    public void OnLevelUp () {
      // Update the level text to the new level value.
      level.text = playerSavedXP.Level.ToString();
    }

    public void OnLevelMaxReached () {
      // Show the "Max level reached!"-text.
      maxLevelReached.enabled = true;
    }

    public void OnXPReset () {
      // Hide the "Max level reached!"-text.
      maxLevelReached.enabled = false;

      // Update the values when XP is reset, so we are always are in sync.
      SetAllXPValues();
    }
  }
}

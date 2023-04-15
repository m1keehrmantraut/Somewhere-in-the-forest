using UnityEngine;

namespace Saucy.Modules.XP {
  public abstract class DataXPReceive : ScriptableObject {
    public int CurrentXP { get; protected set; } // The current XP
    public int MaxXP { get; protected set; } // Maximum XP we can have.
    public int AcquiredXP { get; protected set; } // Total acquired XP.
    public int RequiredXP { get; protected set; } // Required XP to level up.
    public int PreviouslyRequiredXP { get; protected set; } // Previously required XP. Useful if we're doing smooth slider animations.
    public int MissingXP { get; protected set; } // The missing XP to level up.
    public int Level { get; protected set; } // Current level.
    [Tooltip("Maximum level. Default value is 10.")]
    [SerializeField] private int maxLevel = 10; // Maximum level, we can set this in the inspector.
    public int MaxLevel { get { return maxLevel; } protected set { maxLevel = value; } } // Maximum level.
    public float Progress { get; protected set; } // Current level progress, from values 0-1.
    public float ProgressPrevious { get; protected set; } // Previous current level progress, from values 0-1. Useful if we're doing smooth slider animations.
    public float ProgressOverall { get; protected set; } // Current overall level progress, from values 0-1. Based on Starting Level -> Max level.

    // A multiplier we use when adding current XP and acquired XP.
    [Tooltip("A diffuculty multiplier, a higher value means we gain more XP from the received XP. Default value is 1f.")]
    public float difficultyMultiplier = 1f;

    [Tooltip("Which XP formula to base the calculations on.")]
    [SerializeField] protected DataXPFormula selectedXPFormula = null;
    public DataXPFormula SelectedXPFormula { get { return selectedXPFormula; } }

    [Tooltip("Set progress to zero upon hitting max level (similar to how World of Warcraft does it). A FALSE value sets it to empty (zero) progress. Default value is false.")]
    public bool setProgressToZeroOnReachingMaxLevel;

    // Need a reference to the caller (XPReceiver) so the scripts can call UnityEvents on that script.
    protected XPReceiver caller;

    protected virtual void OnValidate () {
      // Required. We need an XP formula so we can calculate XP. Notify the developer if we have none slotted in the inspector.
      if (selectedXPFormula == null) {
        Debug.LogError("Asset \"" + name + "\" expects a selected XP formula.");
      }

      // We need a max level above zero, otherwise we can't calculate XP.
      if (MaxLevel <= 0) {
        Debug.LogError("Asset \"" + name + "\" needs a max level over 0.");
      }
    }

    public virtual void Init (XPReceiver _caller) {
      // Sets the reference of the caller (XPReceiver) so we can use its UnityEvents for updating values.
      caller = _caller;

      if (RequiredXP == 0) {
        ResetXP();
      }
    }

    // Main method where we receive XP. How much experience we gain and who's the granter.
    public abstract bool ReceiveXP (int _amount, GameObject _granter);

    // Sets the progress values.
    public abstract void SetProgress ();

    // Resets the XP back to zero.
    public virtual void ResetXP () {
      Level = 1;
      CurrentXP = 0;
      AcquiredXP = 0;
      RequiredXP = selectedXPFormula.RequiredXP(Level);
      PreviouslyRequiredXP = selectedXPFormula.PreviousRequiredXP(Level);
      MissingXP = RequiredXP - CurrentXP;
      MaxXP = selectedXPFormula.MaxLevelAcquiredXP(MaxLevel, this);

      // Update the progress fields.
      SetProgress();

      // Call UnityEvent on the caller (XPReceiver) so we can notify UI, etc.
      if (caller.OnXPReset != null) {
        caller.OnXPReset.Invoke();
      }
    }
  }
}

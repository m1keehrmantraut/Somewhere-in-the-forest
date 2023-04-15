using UnityEngine;

// XP resetting, "alternative", style. Similar how Horizon: Zero Dawn does it.

namespace Saucy.Modules.XP {
  [CreateAssetMenu(menuName = "Extensible XP System/Modules/XP/Receive XP/Alternative method")]
  public class DataXPReceiveAlternative : DataXPReceive {
    public override bool ReceiveXP (int _amount, GameObject _granter) {
      if (_amount <= 0) {
        // If the granted amount is zero or less we gain nothing and we don't run the method.
        Debug.LogWarning("Granted XP cannot be zero or a negative number.");
        return false;
      }
      if (AcquiredXP >= MaxXP) {
        // If we've reached the maximum XP (max level) we'll stop receiving adding more XP.
        Debug.Log("Max XP reached, stopping receiving XP.");
        return false;
      }

      // Add current XP with the added amount, multiply by difficulty multiplier. Then we clamp the value between 0 and the maximum XP.
      CurrentXP = Mathf.Clamp(Mathf.RoundToInt(CurrentXP + (_amount * difficultyMultiplier)), 0, MaxXP);
      // Add the already acquired XP with the added amount, multiply by difficulty multiplier. Then we clamp the value between 0 and the maximum XP.
      AcquiredXP = Mathf.Clamp(Mathf.RoundToInt(AcquiredXP + (_amount * difficultyMultiplier)), 0, MaxXP);

      // We run a while-loop if current XP is more than required XP (meaning we have leveled up).
      while (CurrentXP >= RequiredXP) {
        // Increase the level by one, then call the UnityEvent on the caller script.
        Level += 1;

        if (caller.OnLevelUp != null) {
          caller.OnLevelUp.Invoke();
        }

        // If we now are at or over max level, reset to max level. Then call the UnityEvent on the caller script.
        if (Level >= MaxLevel) {
          Level = MaxLevel;

          if (caller.OnLevelMaxReached != null) {
            caller.OnLevelMaxReached.Invoke();
          }

          // Break out of the loop because we don't need to run it anymore because we're at max level.
          break;
        }

        // Remove the required XP from current XP so we start from zero again.
        CurrentXP -= RequiredXP;
        // Update the required XP for the (new) current level. We stop running the while-loop if CurrentXP is less than RequiredXP.
        RequiredXP = selectedXPFormula.RequiredXP(Level);
      }

      // If we're at max level, we want to clamp the current XP again.
      if (AcquiredXP >= MaxXP) {
        CurrentXP = Mathf.Clamp(CurrentXP, 0, RequiredXP);
      }

      // Reset the previously required XP to zero because the way this calculation works is that it starts over.
      PreviouslyRequiredXP = 0;
      // Update the missing XP.
      MissingXP = (RequiredXP - CurrentXP);

      // Update our progress values.
      SetProgress();

      // Our XP values have changed, so call UnityEvent on the caller (XPReceiver) so we can notify UI, etc.
      if (caller.OnXPChanged != null) {
        caller.OnXPChanged.Invoke();
      }

      // Return true because we gained XP.
      return true;
    }

    // Sets the progress values.
    public override void SetProgress () {
      // Start by setting fill amount to zero.
      float _fillAmount = 0f;
      float _fillAmountOverall = 0f;

      if (AcquiredXP >= MaxXP) {
        // If we are at max level:
        // Check if we should set the progress to empty or full.
        _fillAmount = (setProgressToZeroOnReachingMaxLevel) ? 0f : 1f;
        _fillAmountOverall = (setProgressToZeroOnReachingMaxLevel) ? 0f : 1f;
      } else {
        // If we aren't at max level yet, set progress and progress overall values.
        _fillAmount = Mathf.Clamp01((float) CurrentXP / RequiredXP);
        _fillAmountOverall = Mathf.Clamp01((float) AcquiredXP / (float) selectedXPFormula.MaxLevelAcquiredXP(MaxLevel, this));
      }

      // Save the previous progress first. We do this because we can use UI elements to smoothly fill/deplete an animated bar.
      ProgressPrevious = Progress;
      // Update the progress and progress overall values.
      Progress = _fillAmount;
      ProgressOverall = _fillAmountOverall;
    }
  }
}

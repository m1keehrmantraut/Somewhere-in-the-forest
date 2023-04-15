using UnityEngine;
using UnityEngine.Events;

// An interface is a "contract" of which fields and methods the implementing scripts must have.

namespace Saucy.Modules.XP {
  public interface IXPReceive {
    // Fields.
    int CurrentXP { get; }
    int MaxXP { get; }
    int AcquiredXP { get; } // Total acquired XP.
    int RequiredXP { get; } // Required XP to level up.
    int PreviouslyRequiredXP { get; } // Useful if we're doing smooth slider animations.
    int MissingXP { get; } // The missing XP to level up.

    int Level { get; } // Current level.
    int MaxLevel { get; }

    float Progress { get; } // Current level progress, from values 0-1.
    float ProgressPrevious { get; } // Previous current level progress, from values 0-1. Useful if we're doing smooth slider animations.
    float ProgressOverall { get; } // Current overall level progress, from values 0-1. Based on Starting Level -> Max level.

    DataXPReceive XPCalculationMethod { get; } // Required. The XP calculation method for granting XP and calculating leveling.

    UnityEvent OnXPChanged { get; } // UnityEvent that gets called whenever XP value is changed.
    UnityEvent OnLevelUp { get; } // UnityEvent that gets called on level up.
    UnityEvent OnLevelMaxReached { get; }  // UnityEvent that gets called when max level is reached.
    UnityEvent OnXPReset { get; }  // UnityEvent that gets called when it is reset.

    // Methods.
    void ReceiveXP (int _amount); // Simple method that can be called from the editor (UnityEvent responses can't handle return values).
    bool ReceiveXP (int _amount, GameObject _granter = null); // Received amount and which object that gives the XP.
    void ResetXP (); // Resets the XP back to zero.
  }
}

using UnityEngine;
using UnityEngine.Events;

// The script that receives XP. Could be added to a player, and depending on how you are granting XP this GameObject receives if the criteria is fulfilled.

namespace Saucy.Modules.XP {
  public class XPReceiver : MonoBehaviour, IXPReceive {
    // We reference the XP calculation method instead of creating fields here, because the XP calculation method has everything stored (it's an ScriptableObject).
    public int CurrentXP { get { return xpCalculationMethod.CurrentXP; } } // Current XP.
    public int MaxXP { get { return xpCalculationMethod.MaxXP; } } // Maximum XP we can have.
    public int AcquiredXP { get { return xpCalculationMethod.AcquiredXP; } } // Total acquired XP.
    public int RequiredXP { get { return xpCalculationMethod.RequiredXP; } } // Required XP to level up.
    public int PreviouslyRequiredXP { get { return xpCalculationMethod.PreviouslyRequiredXP; } } // Previously required XP. Useful if we're doing smooth slider animations.
    public int MissingXP { get { return xpCalculationMethod.MissingXP; } } // The missing XP to level up.
    public int Level { get { return xpCalculationMethod.Level; } } // Current level.
    public int MaxLevel { get { return xpCalculationMethod.MaxLevel; } } // Maximum level.
    public float Progress { get { return xpCalculationMethod.Progress; } } // Current level progress, from values 0-1.
    public float ProgressPrevious { get { return xpCalculationMethod.ProgressPrevious; } } // Previous current level progress, from values 0-1. Useful if we're doing smooth slider animations.
    public float ProgressOverall { get { return xpCalculationMethod.ProgressOverall; } } // Current overall level progress, from values 0-1. Based on Starting Level -> Max level.

    // NOTE: Remove this if you don't want to use RuntimeSets and only want to use the XP system.
    // Reference to a RuntimeSet that this script adds itself to, so it can receive XP.
    [SerializeField] private CanReceiveXPRuntimeSet canReceiveXPSet = null;

    // Reset the acquired XP back to zero on OnEnable.
    [SerializeField] private bool resetXPOnEnable = false;

    // The XP calculation method that is going to save the XP, calculate current XP and level, calculate missing XP, etc.
    [SerializeField] private DataXPReceive xpCalculationMethod = null;
    public DataXPReceive XPCalculationMethod { get { return xpCalculationMethod; } }

    // UnityEvent for when XP value has changed (been acquired).
    [SerializeField] private UnityEvent onXPChanged = new UnityEvent();
    public UnityEvent OnXPChanged { get { return onXPChanged; } }

    // UnityEvent for when level up has been reached.
    [SerializeField] private UnityEvent onLevelUp = new UnityEvent();
    public UnityEvent OnLevelUp { get { return onLevelUp; } }

    // UnityEvent for when maximum level has been reached.
    [SerializeField] private UnityEvent onLevelMaxReached = new UnityEvent();
    public UnityEvent OnLevelMaxReached { get { return onLevelMaxReached; } }

    // UnityEvent for when XP has been reset. Useful to have for UI canvases for it to know values has been changed in the editor (also in-game).
    [SerializeField] private UnityEvent onXPReset = new UnityEvent();
    public UnityEvent OnXPReset { get { return onXPReset; } }

    private void OnValidate () {
      // NOTE: Remove this if you don't want to use RuntimeSets and only want to use the XP system.
      // The script is missing a RuntimeSet asset.
      if (canReceiveXPSet == null) {
        Debug.LogError("GameObject \"" + name + "\" doesn't have a CanRecieveXP runtime asset assigned.");
      }

      // The script is missing an XP Calculation method. The XP Calculation method is required otherwise the script won't work.
      if (xpCalculationMethod == null) {
        Debug.LogError("GameObject \"" + name + "\" doesn't have a XP Receive Method asset assigned.");
      }
    }

    private void Awake () {
      // Initialize the XP calculation method so it has a reference to the UnityEvents in this script.
      xpCalculationMethod.Init(this);
    }

    private void OnEnable () {
      // Add this script to the list that can receive XP.
      canReceiveXPSet.Add(this);

      // Reset the XP back to zero if value is true.
      if (resetXPOnEnable) {
        ResetXP();
      }
    }

    private void OnDisable () {
      // Remove this script to the list that can receive XP.
      canReceiveXPSet.Remove(this);
    }

    // Can be called from the editor and amount be chosen. An UI button's OnClick can call this for example.
    public void ReceiveXP (int _amount) {
      // Add XP to the receiver without a granter.
      // ReceiveXP() returns a bool that can be used to check if the object has added the XP to their acquired XP.
      bool _gainedXP = XPCalculationMethod.ReceiveXP(_amount, null);

      if (_gainedXP) {
        Debug.Log(name + " is granted " + _amount + " XP.");
      }
    }

    // Cannot be called by an UI button's OnClick for example, but other scripts can still call it as normal.
    // This is because OnClick doesn't support return types, only Void (which is why the other method is added).
    public bool ReceiveXP (int _amount, GameObject _granter = null) {
      // Add XP to the receiver with a granter.
      // ReceiveXP() returns a bool that can be used to check if the object has added the XP to their acquired XP.
      bool _gainedXP = XPCalculationMethod.ReceiveXP(_amount, _granter);

      if (_gainedXP) {
        Debug.Log(name + " is granted " + _amount + " XP by " + _granter.name + ".");
      }

      // Continue the chain of returning the boolean value XPCalculationMethod.ReceiveXP().
      return _gainedXP;
    }

    public void ResetXP () {
      // Calls the ResetXP on the chosen XP calculation method. Which resets the XP to zero.
      xpCalculationMethod.ResetXP();
    }
  }
}

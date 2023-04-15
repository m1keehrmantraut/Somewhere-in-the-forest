using UnityEngine;

// XP Grant methods. ScriptableObject assets that XPGranter uses to grant XP.

namespace Saucy.Modules.XP {
  public abstract class DataXPGrant : ScriptableObject {
    // Radius could be a common thing for objects that grant XP, so it is included here in the base class.
    // We don't want to be able to edit the value from other scripts, only in the asset. So a property is created for it.
    [Range(0f, 100f)] public float radius = 0f;

    // The method that will grant XP with amount of experience and who grants it.
    public abstract void GrantXP (int _experience, GameObject _granter);
  }
}

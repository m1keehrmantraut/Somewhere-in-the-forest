using UnityEngine;

// Flat XP calculation.

namespace Saucy.Modules.XP {
  [CreateAssetMenu(menuName = "Extensible XP System/Modules/XP/Formulas/Flat")]
  public class DataXPFormulaFlat : DataXPFormula {
    public override int Formula (int _level) {
      return _level * xpPerLevel;
    }
  }
}

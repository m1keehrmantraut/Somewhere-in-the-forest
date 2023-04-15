using UnityEngine;

// Exponential XP calculation.

namespace Saucy.Modules.XP {
  [CreateAssetMenu(menuName = "Extensible XP System/Modules/XP/Formulas/Exponential")]
  public class DataXPFormulaExponential : DataXPFormula {
    [SerializeField] protected int growthModifier = 100;

    public override int Formula (int _level) {
      return (_level * _level * growthModifier);
    }
  }
}

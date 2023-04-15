using UnityEngine;

// Growth XP calculation.

namespace Saucy.Modules.XP {
  [CreateAssetMenu(menuName = "Extensible XP System/Modules/XP/Formulas/Growth")]
  public class DataXPFormulaGrowth : DataXPFormula {
    [SerializeField] protected float growthModifier = 1f;

    public override int Formula (int _level) {
      return (int) ((_level * 50) * (_level * growthModifier));
    }
  }
}

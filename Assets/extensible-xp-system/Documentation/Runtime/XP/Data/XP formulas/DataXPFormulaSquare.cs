using UnityEngine;

// Square XP calculation. Credits: https://gamedev.stackexchange.com/a/13639

namespace Saucy.Modules.XP {
  [CreateAssetMenu(menuName = "Extensible XP System/Modules/XP/Formulas/Square")]
  public class DataXPFormulaSquare : DataXPFormula {
    [SerializeField] protected float growthModifier = 0.05f;

    public override int Formula (int _level) {
      return (int) (_level / growthModifier) ^ 2;
    }
  }
}

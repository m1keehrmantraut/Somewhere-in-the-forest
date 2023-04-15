using UnityEngine;

// XP calculation of Dungeons & Dragons 3.5 edition.

namespace Saucy.Modules.XP {
  [CreateAssetMenu(menuName = "Extensible XP System/Modules/XP/Formulas/DnD")]
  public class DataXPFormulaDnD : DataXPFormula {
    public override int Formula (int _level) {
      return _level * (_level - 1) * 500;
    }
  }
}

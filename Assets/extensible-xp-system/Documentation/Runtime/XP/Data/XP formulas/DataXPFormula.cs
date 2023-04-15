using UnityEngine;

namespace Saucy.Modules.XP {
  public abstract class DataXPFormula : ScriptableObject {
    // XP per level is pretty common among the XP formulas, so it is included here in the base class.
    [SerializeField] protected int xpPerLevel = 100;

    // The formula we use to calculate XP. This method is key to creating new XP formulas.
    public abstract int Formula (int _level);

    // Below are helper methods for quickly get the level and/or XP. You can override them if you need to.
    // Return the level based on amount of experience. We need a reference to DataXPReceive so we return the correct values.
    public virtual int Level (int _experience, DataXPReceive _receiver) {
      int _startingLevel = 1;
      int _requiredXP = 0;

#if UNITY_2018_4_OR_NEWER
      switch (_receiver) {
        case DataXPReceiveAlternative _alternative:
          _startingLevel = 1;
          _requiredXP = RequiredXP(_startingLevel);

          while (_experience >= _requiredXP) {
            _startingLevel += 1;

            _experience -= _requiredXP;
            _requiredXP = RequiredXP(_startingLevel);
          }

          return _startingLevel;
        case DataXPReceiveNormal _normal:
        default:
          _startingLevel = 1;
          _requiredXP = RequiredXP(_startingLevel);

          while (_experience >= _requiredXP) {
            _startingLevel += 1;

            _requiredXP = RequiredXP(_startingLevel);
          }

          return _startingLevel;
      }
#elif UNITY_5_6_OR_NEWER
      DataXPReceiveAlternative _alt = _receiver as DataXPReceiveAlternative;
      DataXPReceiveNormal _norm = _receiver as DataXPReceiveNormal;

      if (_alt != null) {
        _startingLevel = 1;
        _requiredXP = RequiredXP(_startingLevel);

        while (_experience >= _requiredXP) {
          _startingLevel += 1;

          _experience -= _requiredXP;
          _requiredXP = RequiredXP(_startingLevel);
        }

        return _startingLevel;
      }

      if (_norm != null) {
        _startingLevel = 1;
        _requiredXP = RequiredXP(_startingLevel);

        while (_experience >= _requiredXP) {
          _startingLevel += 1;

          _requiredXP = RequiredXP(_startingLevel);
        }

        return _startingLevel;
      }

      return -1;
#endif
    }

    // Return starting XP based on starting level. We need a reference to DataXPReceive so we return the correct values.
    public virtual int StartingXP (int _startingLevel, DataXPReceive _receiver) {
#if UNITY_2018_4_OR_NEWER
      switch (_receiver) {
        case DataXPReceiveAlternative _alternative:
          // NOTE: This method always starts from 0 XP.
          return 0;
        case DataXPReceiveNormal _normal:
        default:
          return Formula(_startingLevel - 1);
      }
#elif UNITY_5_6_OR_NEWER
      DataXPReceiveAlternative _alt = _receiver as DataXPReceiveAlternative;
      DataXPReceiveNormal _norm = _receiver as DataXPReceiveNormal;

      if (_alt != null) {
        return 0;
      }

      if (_norm != null) {
        return Formula(_startingLevel - 1);
      }

      return -1;
#endif
    }

    // Returns the total XP for the level. We need a reference to DataXPReceive so we return the correct values.
    public virtual int TotalXP (int _level, DataXPReceive _receiver) {
#if UNITY_2018_4_OR_NEWER
      switch (_receiver) {
        case DataXPReceiveAlternative _alternative:
          int _total = 0;

          for (int i = 1; i <= _level; i++) {
            _total += Formula(i);
          }

          return _total;
        case DataXPReceiveNormal _normal:
        default:
          return Formula(_level);
      }
#elif UNITY_5_6_OR_NEWER
      DataXPReceiveAlternative _alt = _receiver as DataXPReceiveAlternative;
      DataXPReceiveNormal _norm = _receiver as DataXPReceiveNormal;

      if (_alt != null) {
        int _total = 0;

          for (int i = 1; i <= _level; i++) {
            _total += Formula(i);
          }

          return _total;
      }

      if (_norm != null) {
        return Formula(_level);
      }

      return -1;
#endif
    }

    // Returns the total XP for max level. We need a reference to DataXPReceive so we return the correct values.
    public virtual int MaxLevelAcquiredXP (int _maxLevel, DataXPReceive _receiver) {
#if UNITY_2018_4_OR_NEWER
      switch (_receiver) {
        case DataXPReceiveAlternative _alternative:
          return TotalXP(_maxLevel - 1, _receiver);
        case DataXPReceiveNormal _normal:
        default:
          return Formula(_maxLevel - 1);
      }
#elif UNITY_5_6_OR_NEWER
      DataXPReceiveAlternative _alt = _receiver as DataXPReceiveAlternative;
      DataXPReceiveNormal _norm = _receiver as DataXPReceiveNormal;

      if (_alt != null) {
        return TotalXP(_maxLevel - 1, _receiver);
      }

      if (_norm != null) {
        return Formula(_maxLevel - 1);
      }

      return -1;
#endif
    }

    // Returns the difference for the current level and previous.  We need a reference to DataXPReceive so we return the correct values.
    public virtual int DifferenceXP (int _level, DataXPReceive _receiver) {
#if UNITY_2018_4_OR_NEWER
      switch (_receiver) {
        case DataXPReceiveAlternative _alternative:
          return Formula(_level);
        case DataXPReceiveNormal _normal:
        default:
          return Formula(_level) - Formula(_level - 1);
      }
#elif UNITY_5_6_OR_NEWER
      DataXPReceiveAlternative _alt = _receiver as DataXPReceiveAlternative;
      DataXPReceiveNormal _norm = _receiver as DataXPReceiveNormal;

      if (_alt != null) {
        return Formula(_level);
      }

      if (_norm != null) {
        return Formula(_level) - Formula(_level - 1);
      }

      return -1;
#endif
    }

    // The required XP to level up to the next level.
    public virtual int RequiredXP (int _level) {
      return Formula(_level);
    }

    // The required XP for previous level.
    public virtual int PreviousRequiredXP (int _level) {
      return Formula(_level - 1);
    }
  }
}

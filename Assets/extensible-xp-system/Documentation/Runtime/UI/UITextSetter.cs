using UnityEngine;
using UnityEngine.UI;

// Simple script that sets the text and image background color when called.

namespace Saucy.Modules.XP {
  public class UITextSetter : MonoBehaviour {
    // References to the UI elements we're gonna set the values on.
    [SerializeField] private Image backgroundImage = null;
    [SerializeField] private Text level = null;
    [SerializeField] private Text startingXP = null;
    [SerializeField] private Text requiredXP = null;
    [SerializeField] private Text differenceXP = null;
    [SerializeField] private Text totalXP = null;

    // Sets the text of the UI texts to new values.
    // "params" are used here instead of creating SetText(string arg0, string arg1, string arg2, ...). Very useful.
    public void SetText (params string[] _arg) {
      level.text = _arg[0];
      startingXP.text = _arg[1];
      requiredXP.text = _arg[2];
      differenceXP.text = _arg[3];
      totalXP.text = _arg[4];
    }

    // Sets the background color to a new color.
    public void SetImageColor (Color _color) {
      backgroundImage.color = _color;
    }
  }
}

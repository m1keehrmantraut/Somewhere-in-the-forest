using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
//
// Author: Ryan Hipple
// Date:   10/04/17
// Url:    https://github.com/roboryantron/Unite2017/blob/master/Assets/Code/Sets/RuntimeSet.cs
// ----------------------------------------------------------------------------

// Creates a list of objects at runtime, which can be used during runtime.

namespace Saucy.Modules.XP {
  public abstract class RuntimeSet<T> : ScriptableObject {
    // List of objects.
    public List<T> Items = new List<T>();

    // Add an object to the list.
    public void Add (T thing) {
      if (!Items.Contains(thing)) {
        Items.Add(thing);
      }
    }

    // Remove an object from the list.
    public void Remove (T thing) {
      if (Items.Contains(thing)) {
        Items.Remove(thing);
      }
    }

    // Remove all objects from the list.
    public void ClearAll () {
      if (Items.Count > 0) {
        Items.Clear();
      }
    }
  }
}

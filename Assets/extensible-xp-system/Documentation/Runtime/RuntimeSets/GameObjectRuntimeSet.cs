using UnityEngine;

// Creates a list of GameObjects at runtime, which can be used during runtime.

namespace Saucy.Modules.XP {
  [CreateAssetMenu(menuName = "Extensible XP System/Data/Runtime sets/Game object")]
  public class GameObjectRuntimeSet : RuntimeSet<GameObject> { }
}

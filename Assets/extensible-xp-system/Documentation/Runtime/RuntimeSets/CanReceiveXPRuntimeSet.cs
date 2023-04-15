using UnityEngine;

// Creates a list of objects at runtime that implements IXPReceive interface, which can be used during runtime.
// Unless you serialize it(?, I have no idea how to do that) or have Odin Inspector you cannot view the list because it has a reference to an interface.

namespace Saucy.Modules.XP {
  [CreateAssetMenu(menuName = "Extensible XP System/Data/Runtime sets/Can receive XP")]
  public class CanReceiveXPRuntimeSet : RuntimeSet<IXPReceive> { }
}

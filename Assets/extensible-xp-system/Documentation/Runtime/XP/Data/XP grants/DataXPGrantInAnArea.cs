using UnityEngine;

// Grants XP to all objects in an area (sphere) that has the IXPReceive interface on them.

namespace Saucy.Modules.XP {
  [CreateAssetMenu(menuName = "Extensible XP System/Modules/XP/Grant XP/In an area")]
  public class DataXPGrantInAnArea : DataXPGrant {
    // Layers to check for IXPReceive interface for.
    [SerializeField] protected LayerMask layersToCheckForReceiveXP;

    public override void GrantXP (int _experience, GameObject _granter) {
      // Create an invisiable sphere that returns all colliders inside it (based on layers we check against).
      Collider[] _hitColliders = Physics.OverlapSphere(_granter.transform.position, radius, layersToCheckForReceiveXP);

      for (int _index = 0; _index < _hitColliders.Length; _index++) {
        // Loop through all colliders and get reference to the IXPReceive interface.
        IXPReceive _receiveXP = _hitColliders[_index].GetComponentInParent<IXPReceive>();

        if (_receiveXP != null) {
          // If an object has an IXPReceive interface we can grant XP to it, passing along the granter.
          _receiveXP.ReceiveXP(_experience, _granter);
        }
      }
    }
  }
}

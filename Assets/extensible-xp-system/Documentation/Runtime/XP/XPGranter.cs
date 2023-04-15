using System.Collections;
using UnityEngine;

// The script that grants XP. Could be added to an enemy, and hooked up that when it dies it grants XP.
// Call `XPGranter.GrantXP()` to grant XP.

namespace Saucy.Modules.XP {
  public class XPGranter : MonoBehaviour, IXPGrant {
    // Amount of experience to grant.
    [SerializeField] private int experience = 10;
    public int Experience { get { return experience; } }

    // If the script should grant experience after each interval.
    [Space] [SerializeField] private bool useInterval = false;

    // Time between each interval of grant XP, in seconds.
    [SerializeField] private float interval = 1f;
    public float Interval { get { return interval; } }

    // Which XP Grant method that should be used when calling GrantXP().
    [Space] [SerializeField] private DataXPGrant xpGrantMethod = null;
    public DataXPGrant XPGrantMethod { get { return xpGrantMethod; } }

    // Reference to the IEnumerator coroutine which runs if useInterval is true.
    private Coroutine routine;

    private void OnValidate () {
      // The script is missing an XP Grant method. The XP Grant method is required otherwise the script won't work.
      if (xpGrantMethod == null) {
        Debug.LogError("GameObject \"" + name + "\" doesn't have a XP Grant Method asset assigned.");
      }

      // The script needs a positive experience number otherwise it won't work.
      if (experience <= 0) {
        Debug.LogError("GameObject \"" + name + "\" cannot have 0 experience or less.");
      }

      // If the script uses the interval field, it needs an interval of more than zero seconds.
      if (useInterval && interval <= 0f) {
        Debug.LogError("GameObject \"" + name + "\" cannot have an interval of 0 or less if interval is being used.");
      }
    }

    private void OnEnable () {
      // Start the coroutine that will grant XP after each interval.
      if (routine == null) {
        routine = StartCoroutine(GrantInterval());
      }
    }

    private void OnDisable () {
      // Stop all coroutines and reset field reference to null when the gameobject is deactivated.
      StopAllCoroutines();
      routine = null;
    }

    public void GrantXP () {
      // Calls the XP Grant method to run it's GrantXP() method. Which in turn runs the code for the chosen XP Grant method.
      XPGrantMethod.GrantXP(Experience, gameObject);
    }

    private IEnumerator GrantInterval () {
      // Used if useInterval is true. Grants XP after an interval of X seconds.
      while (useInterval) {
        // Wait a few seconds before continuing.
        yield return new WaitForSeconds(Interval);

        GrantXP();
      }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected () {
      // Displays a wireframe sphere inside the scene editor if radius is above zero.
      // Mostly used as an visual aid when you want to check how far the radius for granting XP is.
      if (xpGrantMethod != null) {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, XPGrantMethod.radius);
      }
    }
#endif
  }
}

// An interface is a "contract" of which fields and methods the implementing scripts must have.

namespace Saucy.Modules.XP {
  public interface IXPGrant {
    // Fields.
    int Experience { get; } // Amount of experience to be granted.

    float Interval { get; } // Interval in seconds.

    DataXPGrant XPGrantMethod { get; } // Required. The ScriptableObject asset that will run and check which objects that will gain XP.

    // Methods.
    void GrantXP ();
  }
}

using UnityEngine;
using System.Collections;

/**
 * Used to pass parameters between Map and Fight scene
 */
public class FightParams : Singleton<FightParams> {

	// Triggered fight scene.
    public PlayerSingleton.Fight fight;

    // Flag to indicate if last fight was won.
    public bool win = false;

    // Selected arean
    public PlayerSingleton.Arena arena;

    // Selected mercenary
    public PlayerSingleton.Mercenary selected = PlayerSingleton.Mercenary.None;

    public override string ToString()
    {
        return "Fight Params name: " + fight + "arena: " + arena + " mercenary: " + selected;
    }
}

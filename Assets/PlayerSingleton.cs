using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSingleton : Singleton<PlayerSingleton> {

    // available mercenaries
    public enum Mercenary {None, Callahan, PamGrier, Harry};

    // available arena
    public enum Arena {RoofTop, Alley, CenterPark};

    // available fights
    public enum Fight { Fight1, Fight2, Fight3, Fight4, Fight5, Fight6, Fight7, Fight8, Fight9, Fight10, Fight11, Fight12, Fight13, Fight14, Fight15 };

    // list of bonus
    public enum Bonuses { puppet, crux, radio, discoBall };

    // Currently selected mercenary
    // public Mercenary selected;

    // Store names of won combat
    public List<PlayerSingleton.Fight> victories = new List<PlayerSingleton.Fight>();

    // Reinit all singleton for new game
    public void Reset()
    {
        victories.Clear();
        InventorySingleton.Instance.Reset();
    }
}

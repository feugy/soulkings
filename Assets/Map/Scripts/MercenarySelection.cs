using UnityEngine;
using System;
using System.Collections;

public class MercenarySelection : MonoBehaviour {

    // Trigger used, containing fight details
    public CombatTrigger trigger;

    void Start()
    {
        FightParams.Instance.selected = PlayerSingleton.Mercenary.None;
    }

    void StartFight()
    {
        FightParams.Instance.fight = trigger.fight;
        FightParams.Instance.arena = trigger.arena;
        FightParams.Instance.win = false;
        // decrement mercenary
        if (FightParams.Instance.selected != PlayerSingleton.Mercenary.None)
        {
            InventorySingleton.Instance.cash -= InventorySingleton.Instance.rent[FightParams.Instance.selected];
        }
        Debug.Log("load fight: " + FightParams.Instance.ToString());
        Application.LoadLevel("Fight");
    }
}

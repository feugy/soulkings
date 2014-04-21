using UnityEngine;
using System;
using System.Collections;

public class MercenarySelection : MonoBehaviour {

    void StartFight()
    {
        // decrement mercenary
        if (FightParams.Instance.selected != PlayerSingleton.Mercenary.None)
        {
            InventorySingleton.Instance.cash -= InventorySingleton.Instance.rent[FightParams.Instance.selected];
        }
        Debug.Log("load fight: " + FightParams.Instance.ToString());
        Application.LoadLevel("Fight");
    }
}

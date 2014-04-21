using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CombatTrigger : OnClick {

    enum Buttons { Yes, No };

    // Identifies which fight to trigger.
    public PlayerSingleton.Fight fight;

    // Identifies which arena.
    public PlayerSingleton.Arena arena;

    // Combat will be activated when user reach sufficient level
    public bool enable = false;

    // Once finished, the level is not avaiable anymore
    public bool passed = false;

    // combats that will be available after this one
    public List<GameObject> nextCombats = new List<GameObject>();

    // different sprites used
    public Sprite enableSprite;
    public Sprite disableSprite;
    public Sprite passedSprite;

    // Prefab to open mercenary selection dialog
    public GameObject mercenarySelectionType;

    // Prefab to open modal dialog
    public GameObject modalType;

    // For modal description
    public string descriptionTitle;
    public string description;

    // currently displayed modal
    GameObject modal;

    void Start()
    {
        // Read from Player singleton current state
        foreach (PlayerSingleton.Fight won in PlayerSingleton.Instance.victories)
        {
            if (won == fight)
            {
                enable = true;
                passed = true;
                ActivateNext();
            }
        }
        UpdateRendering();
        if (FightParams.Instance.fight == fight)
        {
            // last fight was this one !
            if (FightParams.Instance.win)
                Victory();
            else
                Loose();
        }
    }

    void Update()
    {
        // Demo Hack: enable on F1
        if (Input.GetKeyDown(KeyCode.F1) && !passed && !enable)
        {
            enable = true;
            UpdateRendering();
        }
    }

    // update displayed sprite accordingly to the current state
    void UpdateRendering()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        render.sprite = passed ? passedSprite : enable ? enableSprite : disableSprite;
    }

    internal override void Action()
    {
        if (!enable || passed) return;
        // Display dialog
        modal = (GameObject)GameObject.Instantiate(modalType);
        modal.GetComponent<Modal>().receiver = gameObject;
        modal.GetComponent<Modal>().title = descriptionTitle;
        modal.GetComponent<Modal>().text = description;
        modal.GetComponent<Modal>().buttons.Add(Buttons.Yes.ToString());
        modal.GetComponent<Modal>().buttons.Add(Buttons.No.ToString());
    }

    void OnModalButton(string button)
    {
        if (modal == null) return;
        Debug.Log("Choose modal button " + button);
        // close modal
        Destroy(modal);
        modal = null;
        if (button == Buttons.Yes.ToString())
        {
            // Ask to select a mercenary
            GameObject selection = (GameObject)GameObject.Instantiate(mercenarySelectionType);
            selection.GetComponent<MercenarySelection>().trigger = this;
        }
    }

    // Invoke when fight was won.
    // Register fight as won, change sprite and refresh others combat
    void Victory()
    {
        Debug.Log("fight " + fight + " won !");
        PlayerSingleton.Instance.victories.Add(fight);
        passed = true;
        UpdateRendering();
        if (nextCombats.Count == 0)
        {
            Debug.Log("last stand won !!");
            // last level !! Victory
            Application.LoadLevel("Victory");
        }
        else
            ActivateNext();
    }

    // Invoke when fight was lost.
    void Loose()
    {
        Debug.Log("fight " + fight + " lost...");
    }

    // activate next combats
    void ActivateNext()
    {
        foreach (GameObject next in nextCombats)
        {
            CombatTrigger nextObj = next.GetComponent<CombatTrigger>();
            if (nextObj != null && !nextObj.enable)
            {
                nextObj.enable = true;
                nextObj.UpdateRendering();
                Debug.Log("fight " + nextObj.fight + " now available");
            }
        }
    }
}

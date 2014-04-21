using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class Item : OnClick {

    enum Buttons { Buy, Close };

    // Is either a mercenary or a bonus
    public PlayerSingleton.Mercenary mercenary;
    public PlayerSingleton.Bonuses bonus;
    public bool isMercenary = false;

    // Details title and description
    public string title;
    public string description;

    // Prefab to open modal dialog
    public GameObject modalType;

    GameObject modal;
    bool bought = false;
    int price = 0;

    public void Start()
    {
        // update price
        price = InventorySingleton.Instance.prices[isMercenary ? mercenary.ToString() : bonus.ToString()];
        transform.Find("Price").GetComponent<TextMesh>().text = price.ToString() + "$";
    }

    public void FixedUpdate ()
    {
        if (isMercenary)
            bought = InventorySingleton.Instance.mercenaries.Contains(mercenary);
        else
            bought = InventorySingleton.Instance.bonuses.Contains(bonus);
        UpdateRendering();
    }

    // On click, trigger confirmation and awaits for validation
    override internal void Action()
    {
        // Do not buy unless enought money and not already bought
        if (InventorySingleton.Instance.cash >= price && !bought)
        {
            // Display dialog
            modal = (GameObject)GameObject.Instantiate(modalType);
            modal.GetComponent<Modal>().z = -90f;
            modal.GetComponent<Modal>().receiver = gameObject;
            modal.GetComponent<Modal>().title = title;
            modal.GetComponent<Modal>().text = description;
            modal.GetComponent<Modal>().buttons.Add(Buttons.Buy.ToString());
            modal.GetComponent<Modal>().buttons.Add(Buttons.Close.ToString());
        }
    }

    void OnModalButton(string button)
    {
        if (modal == null) return;
        Debug.Log("Choose modal button " + button);
        // close modal
        Destroy(modal);
        modal = null;
        if (button == Buttons.Buy.ToString())
            Purchase();
    }

    public void Purchase()
    {
        Debug.Log("bought item " + getDisplayName() + " for " + price);
        InventorySingleton.Instance.cash -= price;
        bought = true;
        if (isMercenary)
        {
            InventorySingleton.Instance.mercenaries.Add(mercenary);
            switch (mercenary)
            {
                case PlayerSingleton.Mercenary.Callahan:
                    InventorySingleton.Instance.Callahan = true;
                    break;
                case PlayerSingleton.Mercenary.Harry:
                    InventorySingleton.Instance.Harry = true;
                    break;
                case PlayerSingleton.Mercenary.PamGrier:
                    InventorySingleton.Instance.PamGrier = true;
                    break;
            }
        }
        else
        {
            InventorySingleton.Instance.bonuses.Add(bonus);
            switch (bonus)
            {
                case PlayerSingleton.Bonuses.puppet:
                    InventorySingleton.Instance.puppet = true;
                    break;
                case PlayerSingleton.Bonuses.crux:
                    InventorySingleton.Instance.crux = true;
                    break;
                case PlayerSingleton.Bonuses.radio:
                    InventorySingleton.Instance.radio = true;
                    break;
                case PlayerSingleton.Bonuses.discoBall:
                    InventorySingleton.Instance.discoBall = true;
                    break;
            }
        }
        UpdateRendering();
    }

    void UpdateRendering()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        TextMesh textRender = transform.Find("Price").GetComponent<TextMesh>();
        if (bought)
        {
            sprite.material.color = InventorySingleton.itemBoughtColor;
            textRender.color = InventorySingleton.textBoughtColor;
        }
        else if (InventorySingleton.Instance.cash < price)
        {
            sprite.material.color = InventorySingleton.itemUnavailableColor;
            textRender.color = InventorySingleton.textUnavailableColor;
        }
        else
        {
            sprite.material.color = Color.white;
            textRender.color = Color.white;
        }
    }

    string getDisplayName() 
    {
        return isMercenary ? mercenary.ToString() : bonus.ToString();
    }

}

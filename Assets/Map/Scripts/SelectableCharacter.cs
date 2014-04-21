using UnityEngine;
using System.Collections;

public class SelectableCharacter : OnClick {

    // Current mercenary
    public PlayerSingleton.Mercenary mercenary;

    bool disabled = false;

    void Start()
    {
        disabled = false;
        // hide unavailable mercenaries
        if (!InventorySingleton.Instance.mercenaries.Contains(mercenary))
            transform.gameObject.SetActive(false);
        else
        {
            // update its price
            int rent = InventorySingleton.Instance.rent[mercenary];
            TextMesh priceText = transform.Find("Price").gameObject.GetComponent<TextMesh>();
            priceText.text = rent + "$";
            if (InventorySingleton.Instance.cash < rent)
            {
                // make it unavailable.
                disabled = true;
                renderer.material.color = InventorySingleton.mercenaryUnaffordableColor;
                priceText.color = InventorySingleton.textUnaffordableColor;
            }
        }
    }

	// Activate or not depending on the selected mercenary
	void Update () {
        // activate or disable highlight
        if (FightParams.Instance.selected == mercenary)
            transform.scaleTo(0.5f,1.1f).loopsInfinitely(GoLoopType.PingPong);
        else
            transform.killTweening();
	}

    internal override void Action() 
    {
        if (disabled) return;
        FightParams.Instance.selected = mercenary;
        Debug.Log("choose mercenary " + mercenary);
    }
}

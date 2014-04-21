using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class InventorySingleton : Singleton<InventorySingleton>
{
    //le cash que detient le joueur
    public int cash;

    // Bought bonus, use either list or boolean
    public bool puppet = false;
    public bool crux = false;
    public bool radio = false;
    public bool discoBall = false;
    public List<PlayerSingleton.Bonuses> bonuses = new List<PlayerSingleton.Bonuses>();

    // Bought mercenaries, use either list or boolean
    public bool Callahan = false;
    public bool PamGrier = false;
    public bool Harry = false;
    public List<PlayerSingleton.Mercenary> mercenaries = new List<PlayerSingleton.Mercenary>();

    // shop prices
    public Dictionary<String, int> prices = new Dictionary<String, int>();

    // mercenaries fees
    public Dictionary<PlayerSingleton.Mercenary, int> rent = new Dictionary<PlayerSingleton.Mercenary, int>();

    // color constants
    public static Color itemBoughtColor = new Color(73 / 255.0F, 255 / 255.0F, 151 / 255.0F);
    public static Color itemUnavailableColor = new Color(125 / 255.0F, 125 / 255.0F, 125 / 255.0F);
    public static Color mercenaryUnaffordableColor = new Color(125 / 255.0F, 125 / 255.0F, 125 / 255.0F);
    public static Color textBoughtColor = new Color(65 / 255.0F, 229 / 255.0F, 0 / 255.0F);
    public static Color textUnavailableColor = new Color(125 / 255.0F, 125 / 255.0F, 125 / 255.0F);
    public static Color textUnaffordableColor = new Color(255 / 255.0F, 50 / 255.0F, 0F);

    public void Awake()
    {
        // init prices
        prices.Add(PlayerSingleton.Bonuses.puppet.ToString(), 500);
        prices.Add(PlayerSingleton.Bonuses.crux.ToString(), 1000);
        prices.Add(PlayerSingleton.Bonuses.radio.ToString(), 2000);
        prices.Add(PlayerSingleton.Bonuses.discoBall.ToString(), 5000);
        prices.Add(PlayerSingleton.Mercenary.Callahan.ToString(), 1000);
        prices.Add(PlayerSingleton.Mercenary.PamGrier.ToString(), 3500);
        prices.Add(PlayerSingleton.Mercenary.Harry.ToString(), 10000);

        rent.Add(PlayerSingleton.Mercenary.Callahan, 200);
        rent.Add(PlayerSingleton.Mercenary.PamGrier, 500);
        rent.Add(PlayerSingleton.Mercenary.Harry, 1000);
        Reset();
    }

    public void Reset()
    {
		Debug.Log ( "Reset" );
        cash = 200;
        puppet = false;
        crux = false;
        radio = false;
        discoBall = false;
        Callahan = false;
        PamGrier = false;
        Harry = false;
        mercenaries.Clear();
        bonuses.Clear();
    }

}

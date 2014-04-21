using UnityEngine;
using System.Collections;

public class Godric : NonPlayableCharacterHud
{
	protected override void CreateDefaultCharacter ()
	{
		base.CreateDefaultCharacter ();

		Character.MaxHP = 200;
		Character.HP = Character.MaxHP;
		Character.MaxMana = 100;
		Character.Mana = Character.MaxMana;
		Character.Name = "Godric";
		Character.CriticalChance = 0.1f;
	}
}
using UnityEngine;
using System.Collections;

public class Player : PlayableCharacterHud
{
	public GameObject ShieldButton;
	public GameObject ShieldEffect;
	public GameObject MercenaryButton;

	protected override void CreateDefaultCharacter ()
	{
		base.CreateDefaultCharacter ();

		Character.CriticalChance = 0.25f;
		Character.MaxHP = 100;

		if ( InventorySingleton.Instance.puppet )
			Character.DamageFactor *= 1.5f;

		if ( InventorySingleton.Instance.crux )
			Character.MaxHP *= 1.5f;

		if ( InventorySingleton.Instance.radio )
			Character.DamageFactor *= 1.5f;

		if ( InventorySingleton.Instance.discoBall )
		{
			Character.CriticalChance += 0.20f;
			Character.DamageFactor *= 1.5f;
			Character.MaxHP *= 1.5f;
		}

		Character.HP = Character.MaxHP;
		

		ShieldEffect.SetActive ( false );
		ShieldButton.GetComponent<Shield> ().OnShieldActivation += OnShieldActivation;

		PlayerSingleton.Mercenary mercenary = PlayerSingleton.Mercenary.None;
		if ( FightParams.Exists )
			mercenary = FightParams.Instance.selected;

		switch ( mercenary )
		{
			case PlayerSingleton.Mercenary.PamGrier:
				MercenaryButton.GetComponent<SpriteRenderer> ().sprite = GameResources.Instance.Prefabs.PamGrier;
				break;
			case PlayerSingleton.Mercenary.Callahan:
				MercenaryButton.GetComponent<SpriteRenderer> ().sprite = GameResources.Instance.Prefabs.Callahan;
				break;
			case PlayerSingleton.Mercenary.Harry:
				MercenaryButton.GetComponent<SpriteRenderer> ().sprite = GameResources.Instance.Prefabs.Harry;
				break;
			default:
				MercenaryButton.SetActive ( false );
				break;
		}
	}

	void OnShieldActivation ( bool activated )
	{
		if ( activated )
		{
			ShieldEffect.SetActive ( true );
			ShieldEffect.alphaTo ( 0.25f, 1 );
			ShieldEffect.transform.localScale = new Vector3 ( 2,2,2 );
			ShieldEffect.transform.scaleTo ( 0.25f, 2.25f ).loopsInfinitely ( GoLoopType.PingPong );
		}
		else
		{
			ShieldEffect.renderer.material.alphaTo ( 0.25f, 0 ).setOnCompleteHandler ( c => ShieldEffect.SetActive ( false ) );
		}
	}
}
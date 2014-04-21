using UnityEngine;
using System.Collections;

public class NonPlayableCharacterHud : CharacterHud
{
	public float HP;
	public float DamageFactor = 1;
	public float ArmorFactor = 1;
	public int price;

	protected override void CreateDefaultCharacter ()
	{
		Character = new Character ();
		Character.OnHPChanged += OnHPChanged;
		Character.MaxHP = HP;
		Character.HP = Character.MaxHP;
		Character.DamageFactor = DamageFactor;
		Character.ArmorFactor = ArmorFactor;

		GameScreen.Instance.RegisterNonPlayerCharacter ( this );
	}

	void OnHPChanged ()
	{
		if ( Character.HP <= 0 )
		{
			GameScreen.Instance.UnregisterNonPlayerCharacter ( this );
			Destroy ( gameObject );

			GameScreen.Instance.OnGhostDead ( price );
				
// 			for ( int i = 0; i < 5; i++ )
// 			{
// 				GameObject coin = GameObject.Instantiate ( GameResources.Instance.Prefabs.Coin ) as GameObject;
// 				coin.transform.position = transform.position;
// 			}
		}
	}

	void OnDestroy ()
	{
		if ( GameScreen.InstanceCreated )
			GameScreen.Instance.UnregisterPlayerCharacter ( this );
	}
}
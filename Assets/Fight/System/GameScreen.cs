using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameScreen : SceneSingleton<GameScreen>
{
	internal int GhostCount;
	internal int EarnedDollars;

	private TargetedActionButton activeButton;
	internal TargetedActionButton ActiveActionButton
	{
		get { return activeButton; }
		set
		{
			TargetedActionButton previousButton = activeButton;

			activeButton = value;

			if ( previousButton != null )
				previousButton.OnUnengaged ();

			if ( activeButton != null )
				activeButton.OnEngaged ();
		}
	}

	internal List<CharacterHud> Players = new List<CharacterHud> ();

	internal List<CharacterHud> NonPlayers = new List<CharacterHud> ();

	public Character RandomPlayerCharacter
	{
		get
		{
			if ( Players.Count > 0 )
			{
				int index = RandomInt.Range ( 0, Players.Count - 1 );
				return Players[index].Character;
			}
			else
				return null;
		}
	}

	internal void RegisterPlayerCharacter ( CharacterHud player )
	{
		Players.Add ( player );
	}

	internal void UnregisterPlayerCharacter ( CharacterHud player )
	{
		Players.Remove ( player );
	}

	internal void RegisterNonPlayerCharacter ( CharacterHud player )
	{
		NonPlayers.Add ( player );
	}

	internal void UnregisterNonPlayerCharacter ( CharacterHud player )
	{
		NonPlayers.Remove ( player );
	}

	void Update ()
	{
		if ( Input.GetKeyDown ( KeyCode.C ) )
		{
			GameObject coin = GameObject.Instantiate ( GameResources.Instance.Prefabs.Coin ) as GameObject;
			coin.transform.position = Vector3.zero;
		}
	}

	internal void OnPlayerDead ()
	{
		if ( GameResources.Instance.SoundBank.playerDead != null )
			AudioSource.PlayClipAtPoint ( GameResources.Instance.SoundBank.playerDead, Vector3.zero );

		GameObject screen = GameObject.Instantiate ( GameResources.Instance.LostScreen ) as GameObject;
		screen.transform.position = new Vector3 ( 0, 0, -50 );
		screen.FindChildByName ( "Earned" ).GetComponent<TextMesh> ().text = EarnedDollars.ToString ();

		FightParams.Instance.win = false;
		InventorySingleton.Instance.cash += EarnedDollars;
	}

	internal void OnGhostDead ( int price )
	{
		EarnedDollars += price;

		GhostCount--;
		if ( GhostCount == 0 )
		{
			if ( GameResources.Instance.SoundBank.playerDead != null )
				AudioSource.PlayClipAtPoint ( GameResources.Instance.SoundBank.playerDead, Vector3.zero );

			GameObject screen = GameObject.Instantiate ( GameResources.Instance.WonScreen ) as GameObject;
			screen.transform.position = new Vector3 ( 0, 0, -50 );
			screen.FindChildByName ( "Earned" ).GetComponent<TextMesh> ().text = EarnedDollars.ToString ();

			FightParams.Instance.win = true;
			InventorySingleton.Instance.cash += EarnedDollars;
		}
	}
}

using UnityEngine;
using System.Collections;

public abstract class CharacterHud : MonoBehaviour
{
	private Character character;
	internal Character Character
	{
		get
		{
			if ( character == null )
				CreateDefaultCharacter ();
			return character;
		}
		set { character = value; }
	}

	private CharacterFace face;
	internal CharacterFace Face
	{
		get
		{
			if ( face == null )
				face = gameObject.FindChildByName ( "Face" ).GetComponent<CharacterFace> ();
			return face;
		}
	}

	abstract protected void CreateDefaultCharacter ();

	void FixedUpdate ()
	{
		Character.Update ( Time.fixedDeltaTime );
	}
}

public class PlayableCharacterHud : CharacterHud
{
	public ShortcutsConfiguration ShortcutsConfig;

	protected override void CreateDefaultCharacter ()
	{
		Character = new Character ();
		Character.OnHPChanged += OnHPChanged;

		Character.MaxHP = 50;
		Character.HP = Character.MaxHP;
//		Character.CriticalChance = 0.1f;

		GameScreen.Instance.RegisterPlayerCharacter ( this );
	}

	void OnHPChanged ()
	{
		if ( Character.HP <= 0 )
		{
			GameScreen.Instance.UnregisterPlayerCharacter ( this );
			Destroy ( gameObject );

			GameScreen.Instance.OnPlayerDead ();
		}
	}

	void OnDestroy ()
	{
		if ( GameScreen.InstanceCreated )
			GameScreen.Instance.UnregisterPlayerCharacter ( this );
	}

	void Start ()
	{
		ActionButton[] buttons = GetComponentsInChildren<ActionButton> ();
		foreach ( var b in buttons )
			AssociateShortcut ( b, ActionButtonsShortcuts.From ( ShortcutsConfig ) );
	}

	void AssociateShortcut ( ActionButton button, ActionButtonsShortcuts shortcuts )
	{
		switch ( button.Action )
		{
			case Action.Primary: button.SetShortcutKey ( shortcuts.First ); break;
			case Action.Secondary: button.SetShortcutKey ( shortcuts.Second ); break;
			case Action.Inventory1: button.SetShortcutKey ( shortcuts.Third ); break;
			case Action.Inventory2: button.SetShortcutKey ( shortcuts.Fourth ); break;
			case Action.Inventory3: button.SetShortcutKey ( shortcuts.Fifth ); break;
		}
	}
}

public enum ShortcutsConfiguration
{
	AZER,
	QSDF,
	WXCV
}

public class ActionButtonsShortcuts
{
	internal KeyCode First;
	internal KeyCode Second;
	internal KeyCode Third;
	internal KeyCode Fourth;
	internal KeyCode Fifth;

	internal ActionButtonsShortcuts ( KeyCode first, KeyCode second, KeyCode third, KeyCode fourth, KeyCode fifth )
	{
		First = first;
		Second = second;
		Third = third;
		Fourth = fourth;
		Fifth = fifth;
	}

	static private ActionButtonsShortcuts Azer = new ActionButtonsShortcuts ( KeyCode.A, KeyCode.Z, KeyCode.E, KeyCode.R, KeyCode.T );
	static private ActionButtonsShortcuts Qsdf = new ActionButtonsShortcuts ( KeyCode.Q, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G );
	static private ActionButtonsShortcuts Wxcv = new ActionButtonsShortcuts ( KeyCode.W, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B );

	internal static ActionButtonsShortcuts From ( ShortcutsConfiguration kind )
	{
		switch ( kind )
		{
			case ShortcutsConfiguration.AZER: return Azer;
			case ShortcutsConfiguration.QSDF: return Qsdf;
			case ShortcutsConfiguration.WXCV: return Wxcv;
			default: return null;
		}
	}
}
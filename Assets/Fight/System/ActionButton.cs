using UnityEngine;
using System.Collections;


public abstract class ActionIcon : HudWidget
{
	private float timeBeforeUp;
	protected float TimeBeforeUp
	{
		get { return timeBeforeUp; }
		private set
		{
			bool isUp = IsUp; // get before changing timeBeforeUp value

			timeBeforeUp = value;

			if ( ( value > 0 ) && isUp )
				OnNonUp ();

			if ( ( value <= 0 ) && !isUp )
				OnUp ();
		}
	}

	protected bool IsUp { get { return timeBeforeUp <= 0; } }

	internal void FixedUpdate ()
	{
		if ( Character.IsPlaying )
			if ( TimeBeforeUp > 0 )
				TimeBeforeUp -= Time.fixedDeltaTime;
	}

	internal void ResetCoolDown ( float coolDown )
	{
		TimeBeforeUp = coolDown;
	}

	virtual protected void DoAction ()
	{
		OnAction ();
	}

	public virtual void OnUp ()
	{
		OnStateChanged ();
	}

	public virtual void OnNonUp ()
	{
		OnStateChanged ();
	}

	public abstract void OnAction ();
	public abstract void OnStateChanged ();
}







public abstract class BehaviourIcon : ActionIcon
{
	internal float MinTimeBeforeAttack = 0.5f;
	internal float MaxTimeBeforeAttack = 1.0f;

	internal float TimeActionIsShown = 1;

	private float timeBeforeAction;
	protected float TimeBeforeNextAction
	{
		get { return timeBeforeAction; }
		set
		{
			bool wasAboutTo = IsAboutTo;

			timeBeforeAction = value;

			if ( wasAboutTo && !IsAboutTo ) // ( value > TimeActionIsShown ) )
				OnNoMoreAboutTo ();

			if ( !wasAboutTo && IsAboutTo ) // ( value <= TimeActionIsShown ) )
				OnIsAbout ();
		}
	}

	protected bool IsAboutTo { get { return ( timeBeforeAction > 0 ) && ( timeBeforeAction <= TimeActionIsShown ); } }

	internal new void FixedUpdate ()
	{
		base.FixedUpdate ();

		if ( Character.IsPlaying )
			if ( IsUp )
				if ( TimeBeforeNextAction > 0 )
				{
					TimeBeforeNextAction -= Time.fixedDeltaTime;

					if ( TimeBeforeNextAction <= 0 )
						DoAction ();
				}
	}

	public virtual void OnIsAbout ()
	{
		OnStateChanged ();
	}

	public virtual void OnNoMoreAboutTo ()
	{
		OnStateChanged ();
	}

	protected override void DoAction ()
	{
		base.DoAction ();
		TimeBeforeNextAction = Random.Range ( MinTimeBeforeAttack, MaxTimeBeforeAttack );
	}
}







public enum Action
{
	Primary,
	Secondary,
	Inventory1,
	Inventory2,
	Inventory3,
	Inventory4,
	Inventory5
}

public abstract class ActionButton : ActionIcon
{
	public Action Action;
	private KeyCode ShortcutKey = KeyCode.None;

	internal void SetShortcutKey ( KeyCode key )
	{
		ShortcutKey = key;

		TextMesh text = GetComponentInChildren<TextMesh> ();
		if ( text != null )
			text.text = key.ToString ();
	}

	private bool shortcutKeyDown;

	internal void Update ()
	{
		if ( Character.IsPlaying )
			if ( Input.GetKeyDown ( ShortcutKey ) )
				shortcutKeyDown = true;
	}
	
	internal new void FixedUpdate()
	{
		base.FixedUpdate ();

		/* if ( Character.IsPlaying ) */
		if ( shortcutKeyDown )
		{
			if ( IsUp )
				DoAction ();
			shortcutKeyDown = false;
		}
	}

	void OnMouseDown ()
	{
		if ( Character.IsPlaying )
			if ( IsUp )
				DoAction ();
	}

}



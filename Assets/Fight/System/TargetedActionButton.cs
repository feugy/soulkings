using UnityEngine;
using System.Collections;

public abstract class TargetedActionButton : ActionButton
{
	internal bool IsEngaged { get { return GameScreen.Instance.ActiveActionButton == this; } }

	public override void OnAction ()
	{
		if ( ! IsEngaged )
			GameScreen.Instance.ActiveActionButton = this;
		else
			GameScreen.Instance.ActiveActionButton = null;
	}

	public virtual void OnEngaged ()
	{
		OnStateChanged ();
	}

	public virtual void OnUnengaged ()
	{
		OnStateChanged ();
	}

	public abstract void OnHitTarget ( CharacterHud target );

	public override void OnStateChanged ()
	{
		if ( !IsUp )
			gameObject.renderer.material.color = Color.gray;
		else
			if ( !IsEngaged )
				gameObject.renderer.material.color = Color.white;
			else
				gameObject.renderer.material.color = Color.green;
	}
}



abstract class ImmediateActionButton : ActionButton
{
	public override void OnStateChanged ()
	{
		if ( !IsUp )
			gameObject.renderer.material.color = Color.gray;
		else
			gameObject.renderer.material.color = Color.white;
	}
}




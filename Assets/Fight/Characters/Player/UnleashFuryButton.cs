using UnityEngine;
using System.Collections;

class UnleashFuryButton : ImmediateActionButton
{
	private float rageConsumedPerSec;
	private bool isActive;

	internal void OnStart ()
	{
		isActive = true;
		
		Character.OnHitProduced += OnHitProduced;
		rageConsumedPerSec = 10;
		gameObject.renderer.material.color = Color.yellow;

		Heal heal = new Heal ( Character, Character, ( Character.MaxHP - Character.HP ) * 0.20f );
		heal.Apply ();
	}

	internal void OnEnd ()
	{
		isActive = false;
		
		Character.OnHitProduced -= OnHitProduced;
		gameObject.renderer.material.color = Color.white;

		ResetCoolDown ( 2.0f );
	}

	internal new void FixedUpdate ()
	{
		base.FixedUpdate();

		if ( isActive )
		{
			Character.Rage -= ( rageConsumedPerSec * Time.fixedDeltaTime );
			if ( Character.Rage <= 0 )
				OnEnd ();
		}
	}

	public override void OnAction ()
	{
		if ( !isActive )
			OnStart ();
		else
			OnEnd ();
	}

	void OnHitProduced ( Hit hit )
	{
		hit.Points *= 0.5f;

		Heal heal = new Heal ( Character, Character, hit.Points * 0.5f );
		heal.Apply ();
	}
}

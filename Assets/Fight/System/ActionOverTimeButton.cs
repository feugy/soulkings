using UnityEngine;
using System.Collections;

abstract class ActionOverTimeButton : ImmediateActionButton
{
	private float timeActive;
	protected float TimeActive
	{
		get { return timeActive; }
		set
		{
			if ( ( value > 0 ) && !IsActive )
				OnStart ();

			if ( ( value <= 0 ) && IsActive )
				OnEnd ();

			timeActive = value;
		}
	}

	internal bool IsActive { get { return timeActive > 0; } }

	internal new void FixedUpdate ()
	{
		base.FixedUpdate ();
		TimeActive -= Time.fixedDeltaTime;
	}

	internal abstract void OnStart ();
	internal abstract void OnEnd ();
}
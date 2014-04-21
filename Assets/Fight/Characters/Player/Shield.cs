using UnityEngine;
using System.Collections;


class Shield : ActionOverTimeButton
{
	internal delegate void ShieldActivation ( bool activated );
	internal event ShieldActivation OnShieldActivation;

	void Start ()
	{
		Character.OnReduceHit += OnReduceHit;
	}

	internal new void FixedUpdate ()
	{
		base.FixedUpdate ();
	}

	void OnReduceHit ( Hit hit )
	{
		if ( IsActive )
		{
			GameObject effect = GameObject.Instantiate ( GameResources.Instance.Prefabs.ShieldAbsorbEffect ) as GameObject;
			effect.transform.position = transform.position + new Vector3 ( 0, 0, -1 );
			effect.GetComponent<ShieldAbsorbEffect> ().Play ( (int)hit.Points );
			TimeActive = 0;

			hit.Points = 0;

			if ( GameResources.Instance.SoundBank.contreAttack != null )
				AudioSource.PlayClipAtPoint ( GameResources.Instance.SoundBank.contreAttack, Vector3.zero );
		}
	}

	internal override void OnStart ()
	{
		gameObject.renderer.material.color = Color.yellow;
		if ( OnShieldActivation != null )
			OnShieldActivation ( true );

		if ( GameResources.Instance.SoundBank.shield != null )
			AudioSource.PlayClipAtPoint ( GameResources.Instance.SoundBank.shield, Vector3.zero );
	}

	internal override void OnEnd ()
	{
		gameObject.renderer.material.color = Color.white;
		ResetCoolDown ( 2 );
		if ( OnShieldActivation != null )
			OnShieldActivation ( false );
	}

	public override void OnAction ()
	{
		TimeActive = 2.0f;
	}
}
using UnityEngine;
using System.Collections;

public class NonPlayableCharacterFace : CharacterFace
{
	new void Start ()
	{
		base.Start ();
	}

	new void OnDelete ()
	{
		base.OnDelete ();
	}

	internal override void OnHitProduced ( Hit hit )
	{
		float factor =  System.Math.Min ( 1, System.Math.Max ( 0.25f, hit.DamagePoints / 20 ) );

		transform.positionTo ( 0.125f, new Vector3 ( -50 * factor, 0, 0 ), true ).loops ( 2, GoLoopType.PingPong );
	}
}
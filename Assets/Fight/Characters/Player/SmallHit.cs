using UnityEngine;
using System.Collections;


public class SmallHit : TargetedActionButton
{
	public override void OnHitTarget ( CharacterHud target )
	{
		if ( target.Character.IsAlive )
		{
			Hit hit = new Hit ( Character, target.Character, 3 );
			hit.Apply ();

			Character.Rage += 2;

			GameScreen.Instance.ActiveActionButton = null;
			ResetCoolDown ( 1.5f );

			if ( GameResources.Instance.SoundBank.player_shot_single != null )
				AudioSource.PlayClipAtPoint ( GameResources.Instance.SoundBank.player_shot_single, Vector3.zero );
		}
	}
}

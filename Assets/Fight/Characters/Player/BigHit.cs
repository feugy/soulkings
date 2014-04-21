using UnityEngine;
using System.Collections;


public class BigHit : TargetedActionButton
{
	public override void OnHitTarget ( CharacterHud target )
	{
		if ( target.Character.IsAlive )
		{
			Hit hit = new Hit ( Character, target.Character, 8 );
			hit.Apply ();

			Character.Rage += 4;

			GameScreen.Instance.ActiveActionButton = null;
			ResetCoolDown ( 2.5f );

			if ( GameResources.Instance.SoundBank.player_shot_burst != null )
				AudioSource.PlayClipAtPoint ( GameResources.Instance.SoundBank.player_shot_burst, Vector3.zero );
		}
	}
}
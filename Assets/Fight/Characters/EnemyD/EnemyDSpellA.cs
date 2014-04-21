using UnityEngine;
using System.Collections;

public class EnemyDSpellA : BaseBehaviourIcon
{
	void Start ()
	{
		MinTimeBeforeAttack = 2;
		MaxTimeBeforeAttack = 4;
		TimeActionIsShown = 1.0f;
		TimeBeforeNextAction = Random.Range ( MinTimeBeforeAttack, MaxTimeBeforeAttack );
	}

	public override void OnAction ()
	{
		ResetCoolDown ( 2.0f );

		Character targetPlayer = GameScreen.Instance.RandomPlayerCharacter;
		if ( targetPlayer != null )
		{
			Hit hit = new Hit ( Character, targetPlayer, 15 );
			hit.Apply ();

			if ( GameResources.Instance.SoundBank.small_attack != null )
				AudioSource.PlayClipAtPoint ( GameResources.Instance.SoundBank.small_attack, Vector3.zero );
		}
	}
}

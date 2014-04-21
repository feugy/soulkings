using UnityEngine;
using System.Collections;

public class EnemyASpellA : BaseBehaviourIcon
{
	void Start ()
	{
		MinTimeBeforeAttack = 1;
		MaxTimeBeforeAttack = 2;
		TimeActionIsShown = 1.0f;
		TimeBeforeNextAction = Random.Range ( MinTimeBeforeAttack, MaxTimeBeforeAttack );
	}

	public override void OnAction ()
	{
		ResetCoolDown ( 1.0f );

		Character targetPlayer = GameScreen.Instance.RandomPlayerCharacter;
		if ( targetPlayer != null )
		{
			Hit hit = new Hit ( Character, targetPlayer, 2 );
			hit.Apply ();

			if ( GameResources.Instance.SoundBank.small_attack != null )
				AudioSource.PlayClipAtPoint ( GameResources.Instance.SoundBank.small_attack, Vector3.zero );
		}
	}
}

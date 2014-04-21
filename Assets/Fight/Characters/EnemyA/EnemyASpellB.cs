using UnityEngine;
using System.Collections;

public class EnemyASpellB : BaseBehaviourIcon
{
	void Start ()
	{
		MinTimeBeforeAttack = 2;
		MaxTimeBeforeAttack = 5;
		TimeActionIsShown = 1.0f;
		TimeBeforeNextAction = Random.Range ( MinTimeBeforeAttack, MaxTimeBeforeAttack );
	}

	public override void OnAction ()
	{
		ResetCoolDown ( 3 );

		Character targetPlayer = GameScreen.Instance.RandomPlayerCharacter;
		if ( targetPlayer != null )
		{
			Hit hit = new Hit ( Character, targetPlayer, 5 );
			hit.Apply ();

			if ( GameResources.Instance.SoundBank.big_attack_shot != null )
				AudioSource.PlayClipAtPoint ( GameResources.Instance.SoundBank.big_attack_shot, Vector3.zero );
		}
	}
}

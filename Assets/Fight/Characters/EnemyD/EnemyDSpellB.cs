using UnityEngine;
using System.Collections;

public class EnemyDSpellB : BaseBehaviourIcon
{
	void Start ()
	{
		MinTimeBeforeAttack = 3;
		MaxTimeBeforeAttack = 5;
		TimeActionIsShown = 2.0f;
		TimeBeforeNextAction = Random.Range ( MinTimeBeforeAttack, MaxTimeBeforeAttack );
	}

	public override void OnAction ()
	{
		ResetCoolDown ( 6 );

		Character targetPlayer = GameScreen.Instance.RandomPlayerCharacter;
		if ( targetPlayer != null )
		{
			Hit hit = new Hit ( Character, targetPlayer, 30 );
			hit.Apply ();

			if ( GameResources.Instance.SoundBank.big_attack_boss != null )
				AudioSource.PlayClipAtPoint ( GameResources.Instance.SoundBank.big_attack_boss, Vector3.zero );
		}
	}
}

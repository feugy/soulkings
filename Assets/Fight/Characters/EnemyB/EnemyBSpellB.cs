using UnityEngine;
using System.Collections;

public class EnemyBSpellB : BaseBehaviourIcon
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
		ResetCoolDown ( 4 );

		Character targetPlayer = GameScreen.Instance.RandomPlayerCharacter;
		if ( targetPlayer != null )
		{
			Hit hit = new Hit ( Character, targetPlayer, 15 );
			hit.Apply ();

			if ( GameResources.Instance.SoundBank.big_attack_ice != null )
				AudioSource.PlayClipAtPoint ( GameResources.Instance.SoundBank.big_attack_ice, Vector3.zero );
		}
	}
}

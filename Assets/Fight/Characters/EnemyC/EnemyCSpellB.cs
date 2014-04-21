using UnityEngine;
using System.Collections;

public class EnemyCSpellB : BaseBehaviourIcon
{
	void Start ()
	{
		MinTimeBeforeAttack = 3;
		MaxTimeBeforeAttack = 8;
		TimeActionIsShown = 1.0f;
		TimeBeforeNextAction = Random.Range ( MinTimeBeforeAttack, MaxTimeBeforeAttack );
	}

	public override void OnAction ()
	{
		ResetCoolDown ( 5 );

		Character targetPlayer = GameScreen.Instance.RandomPlayerCharacter;
		if ( targetPlayer != null )
		{
			Hit hit = new Hit ( Character, targetPlayer, 20 );
			hit.Apply ();

			if ( GameResources.Instance.SoundBank.big_attack_fire != null )
				AudioSource.PlayClipAtPoint ( GameResources.Instance.SoundBank.big_attack_fire, Vector3.zero );
		}
	}
}

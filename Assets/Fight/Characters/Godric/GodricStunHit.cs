using UnityEngine;
using System.Collections;

public class GodricStunHit : BaseBehaviourIcon
{
	void Start ()
	{
		MinTimeBeforeAttack = 0.25f;
		MaxTimeBeforeAttack = 2;
		TimeBeforeNextAction = Random.Range ( MinTimeBeforeAttack, MaxTimeBeforeAttack );
		TimeActionIsShown = 0.5f;
	}

	public override void OnAction ()
	{
		ResetCoolDown ( 4 );

		Character targetPlayer = GameScreen.Instance.RandomPlayerCharacter;
		if ( targetPlayer != null )
		{
			Stun stun = new Stun ( Character, targetPlayer, 5.0f );
			stun.Apply ();
		}
	}
}

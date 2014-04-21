using UnityEngine;
using System.Collections;

public class GodricLifeSteal : BaseBehaviourIcon
{
	void Start ()
	{
		MinTimeBeforeAttack = 3;
		MaxTimeBeforeAttack = 5;
		TimeActionIsShown = 1.0f;
		TimeBeforeNextAction = Random.Range ( MinTimeBeforeAttack, MaxTimeBeforeAttack );
	}

	public override void OnAction ()
	{
		ResetCoolDown ( 3 );

		foreach ( CharacterHud hud in GameScreen.Instance.Players )
		{
			if ( hud.Character != null )
			{
				DamageOverTime dot = new DamageOverTime ( Character, hud.Character, 6, 2 );
				dot.Start ();
			}
		}

		HealOverTime hot = new HealOverTime ( Character, Character, GameScreen.Instance.Players.Count * 5, 2 );
		hot.Start ();
	}
}

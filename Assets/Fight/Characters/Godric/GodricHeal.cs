using UnityEngine;
using System.Collections;

public class GodricHeal : BaseBehaviourIcon
{
	void Start ()
	{
		MinTimeBeforeAttack = 5;
		MaxTimeBeforeAttack = 8;
		TimeActionIsShown = 2;
		TimeBeforeNextAction = Random.Range ( MinTimeBeforeAttack, MaxTimeBeforeAttack );
	}

	public override void OnAction ()
	{
		ResetCoolDown ( 10 );

		HealOverTime hot = new HealOverTime ( Character, Character, 20, 5 );
		hot.Start ();
	}
}

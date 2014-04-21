using UnityEngine;
using System.Collections;

public class PrepareFights : MonoBehaviour
{
	void Start ()
	{
		if ( FightParams.Exists )
		{
			int fightIndex = (int)FightParams.Instance.fight;
			string fightName = "Fight " + fightIndex;
			Transform fightGo = transform.Find ( fightName );
			if ( fightGo == null )
				Debug.LogError ( "No fight found : no child named '" + fightName + "'" );
			else
			{
				fightGo.gameObject.SetActive ( true );
				GameScreen.Instance.GhostCount = fightGo.childCount;
			}
		}
	}
}

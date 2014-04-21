using UnityEngine;
using System.Collections;

public class HealEffect : MonoBehaviour
{
	GameObject text;
	GameObject sprite;

	void Awake ()
	{
		text = transform.Find ( "Text" ).gameObject;
		sprite = transform.Find ( "Sprite" ).gameObject;
	}

	internal void Play ( Heal heal )
	{
		int points = (int)( heal.Points + 0.5f ); // round to nearest superior integer
		text.GetComponent<TextMesh> ().text = "+" + points.ToString ();
		text.transform.positionTo ( 1, new Vector3 ( 0, 100, 0 ), true ).setOnCompleteHandler ( c => GameObject.Destroy ( gameObject ) );
		text.transform.scaleTo ( 1, 1.5f );
		sprite.transform.scaleTo ( 1, 1.25f );
		sprite.renderer.material.alphaTo ( 1, 0 );
	}
}

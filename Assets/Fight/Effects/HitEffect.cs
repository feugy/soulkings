using UnityEngine;
using System.Collections;

public class HitEffect : MonoBehaviour
{
	GameObject text;
	GameObject textShadow;
	GameObject sprite;

	void Awake ()
	{
		text = transform.Find ( "Text" ).gameObject;
		textShadow = transform.Find ( "TextShadow" ).gameObject; 
		sprite = transform.Find ( "Sprite" ).gameObject;
	}

	internal void Play ( Hit hit )
	{
		int points = (int)( hit.DamagePoints + 0.5f ); // round to nearest superior integer

		text.GetComponent<TextMesh> ().text = "-" + points.ToString ();
		text.transform.positionTo ( 1, new Vector3 ( 0, hit.IsCritical ? 200 : 100, 0 ), true ).setOnCompleteHandler ( c => GameObject.Destroy ( gameObject ) );
		text.transform.scaleTo ( 1, hit.IsCritical ? 3 : 1.5f );

		textShadow.GetComponent<TextMesh> ().text = "-" + points.ToString ();
		textShadow.transform.positionTo ( 1, new Vector3 ( 0, hit.IsCritical ? 200 : 100, 0 ), true ).setOnCompleteHandler ( c => GameObject.Destroy ( gameObject ) );
		textShadow.transform.scaleTo ( 1, hit.IsCritical ? 3 : 1.5f );

		sprite.transform.scaleTo ( 1, hit.IsCritical ? 2 : 1.25f );
		sprite.renderer.material.alphaTo ( 1, 0 );
	}
}

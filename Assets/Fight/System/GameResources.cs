using UnityEngine;
using System.Collections;

public class GameResources : SceneSingleton<GameResources>
{
	public Prefabs Prefabs;
	public SoundBank SoundBank;
	public GameObject WonScreen;
	public GameObject LostScreen;
}

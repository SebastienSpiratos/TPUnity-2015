using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	Text text;

	void Awake ()
	{
		text = GetComponent <Text> ();
	}

	// Fait apparaitre le texte de level courant dans le HUD du joueur
	void Update ()
	{
		text.text = "Level " + EnemyManager.levelMultiplier;
	}
}

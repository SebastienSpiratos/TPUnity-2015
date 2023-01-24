using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DiffTextColorManager : MonoBehaviour {

	Text diffLabel;
	public int targetValue;
	public Slider difficultySlider;

	// Script qui gere la couleur du texte dans le menu
	// de difficulté et met le texte de la difficulté actuelle 
	// a rouge
	void Awake () {
		diffLabel = GetComponent<Text> ();
	}

	void Start () {
		if (targetValue == difficultySlider.normalizedValue )
			diffLabel.color = Color.red;
		else
			diffLabel.color = Color.white;
	}

	void Update () {
		if (targetValue == difficultySlider.normalizedValue )
			diffLabel.color = Color.red;
		else
			diffLabel.color = Color.white;
	}
}

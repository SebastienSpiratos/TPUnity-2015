using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;


    Text text;


    void Awake ()
    {
        text = GetComponent <Text> ();
        score = 0;
    }

	// Fait apparaite le score dans le HUD du joueur
    void Update ()
    {
        text.text = "Score: " + score;
    }
}

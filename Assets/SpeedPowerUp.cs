using UnityEngine;
using System.Collections;

public class SpeedPowerUp : MonoBehaviour {
	public static bool isSpedUp;
	GameObject player;
	
	float speed = 15f;
	bool powerUpUsed;
	AudioSource soundEffect;
	
	void Awake ()
	{
		powerUpUsed = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		soundEffect = GetComponent<AudioSource> ();
		
	}

	// Fonction qui gere la collision avec le powerup
	void OnTriggerEnter (Collider other)
	{
		// S'assure qu'un zombie ne volera pas le powerup
		if(other.gameObject == player)
		{
			// S'assure que le powerup ne peut etre pris plus d'une fois
			if(!powerUpUsed)
			{
				powerUpUsed = true;
				// Active la fonction du powerup
				StartCoroutine(SpeedUp()); 
				// Rend le powerup invisible jusqu'a sa destruction
				GetComponent<MeshRenderer>().enabled = false;
				GetComponent<BoxCollider>().enabled = false;
				GetComponentInChildren<Light>().enabled = false;
			}
		}
	}

	// Fonction d'effet du powerup
	IEnumerator SpeedUp()
	{
		// S'assure que l'effet du powerup ne s'additionne pas
		if(!isSpedUp) {
			ThirdPersonCharacter.m_AnimSpeedMultiplier = ThirdPersonCharacter.m_AnimSpeedMultiplier  * 1.5f;
			ThirdPersonCharacter.m_AnimSpeedMultiplier = ThirdPersonCharacter.m_AnimSpeedMultiplier  * 1.5f;
		}
		soundEffect.Play ();
		isSpedUp = true;
		// Le powerup dure 30 secondes
		yield return new WaitForSeconds(30);
		// Enleve l'effet du powerup apres 30 secondes
		if(isSpedUp) {
			ThirdPersonCharacter.m_AnimSpeedMultiplier = ThirdPersonCharacter.m_AnimSpeedMultiplier  / 1.5f;
			ThirdPersonCharacter.m_AnimSpeedMultiplier = ThirdPersonCharacter.m_AnimSpeedMultiplier  / 1.5f;
		}
		isSpedUp = false;
		// Detruit le powerup
		Destroy(gameObject);
	}
	
	void Update () {
		// Fait tourner le powerup sur lui meme
		transform.Rotate (Vector3.up, speed * Time.deltaTime);
		
		
	}
}


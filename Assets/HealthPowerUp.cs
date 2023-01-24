using UnityEngine;
using System.Collections;

public class HealthPowerUp : MonoBehaviour {

	PlayerHealth playerHealth;
	GameObject player;

	float speed = 15f;

	void Awake ()
	{

		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();

	}

	// Gere la collection du powerup
	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == player)
		{
			// Donne 40 de vie si le player est blessé
			if(playerHealth.currentHealth < 100)
				playerHealth.HealDamage (40);
			else
				playerHealth.HealDamage (0);
			Destroy(gameObject);
		}
	}
	// Fait tourner le powerup sur lui meme
	void Update () {
		transform.Rotate (Vector3.up, speed * Time.deltaTime);
	
	}
}

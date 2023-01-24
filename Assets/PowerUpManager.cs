using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
	public Transform PowerUp;
	public PlayerHealth playerHealth;
	public float spawnTime = 3f;
	public Transform[] spawnPoints;

	// Commence le spawn des powerups
	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	// spawn les powerups a travers les points spécifiés
	void Spawn ()
	{
		if(playerHealth.currentHealth <= 0f)
		{
			return;
		}
		
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		Instantiate (PowerUp, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

	}
}

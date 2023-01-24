using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
	public Slider difficultySlider;
    public Transform[] spawnPoints;

	public static int levelMultiplier;

    void Start ()
    {
		// multiplie les spawns d'ennemis basé sur le level courant
		levelMultiplier = 1;
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }

	void Update()
	{
		// Décide le level courant
		if (ScoreManager.score >= 250 && ScoreManager.score < 500)
			levelMultiplier = 2;
		if (ScoreManager.score >= 500)
			levelMultiplier = 3;
		if (ScoreManager.score < 250)
			levelMultiplier = 1;
	}

	// Fait spawner les ennemis parmis les points spécifiés
    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length); // spawn random entre les différents spawnpoint
		if(difficultySlider.normalizedValue == 0)
			for(int i = 0; i < 1 * levelMultiplier; ++i)
        		Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		else
			for(int i = 0; i < 2 * levelMultiplier; ++i)
				Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}

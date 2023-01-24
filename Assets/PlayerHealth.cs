using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
	public AudioClip playerHealed;
    public AudioClip deathClip;
	public AudioClip HurtClip;
	public AudioClip HurtClip2;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	public Text timerLabel;	
	private double time;


    Animator anim;
	AudioSource  playerHealthPowerUp;
	AudioSource playerPain;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead = false;
    bool damaged;
	
    void Awake ()
    {
        anim = GetComponent <Animator> ();
		playerHealthPowerUp = GetComponent <AudioSource> ();
        playerAudio = GetComponent <AudioSource> ();
		playerPain = GetComponent <AudioSource> ();
		playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
		// Sert a empecher les powerups de rester actifs sur le personnage lors d'une nouvelle partie
		if(SpeedPowerUp.isSpedUp) {
			ThirdPersonCharacter.m_AnimSpeedMultiplier = ThirdPersonCharacter.m_AnimSpeedMultiplier  / 1.5f;
			ThirdPersonCharacter.m_AnimSpeedMultiplier = ThirdPersonCharacter.m_AnimSpeedMultiplier  / 1.5f;
			SpeedPowerUp.isSpedUp = false;
		}
		// Sert a empecher les powerups de rester actifs sur le personnage lors d'une nouvelle partie

		PlayerShooting.damagePerShot = 34;
		PlayerShooting.damagePerMelee = 75;
		DamagePowerUp.isPowered = false;

    }


    void Update ()
    {
		// Affiche le timer
		ShowTimer ();

		// Empeche la vie du joueur d'exceder 100
		if (currentHealth > 100)
			currentHealth = 100;

		// Joue l'animation de playerIsHit et fait un flash rouge
        if(damaged)
        {
			anim.SetBool("IsHit", true);
            damageImage.color = flashColour;
        }
		// Stop l'animation et cache le flash rouge
        else
        {
			anim.SetBool("IsHit", false);
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

	// Fonction qui fait prendre des dommages au joueur
    public void TakeDamage (int amount)
    {
		// Pour activer les effets
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

		// Alterne les sons de douleur
		int randomSound = UnityEngine.Random.Range(0, 2); 

		if (randomSound == 0)
			playerPain.PlayOneShot (HurtClip);
		if (randomSound == 1)
			playerPain.PlayOneShot (HurtClip2);
		else // Default
			playerPain.PlayOneShot (HurtClip);

		// Mort
        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }

	// Pour guérir la vie du joueur
	public void HealDamage (int amount)
	{
		
		currentHealth += amount;	
		healthSlider.value = currentHealth;
		playerHealthPowerUp.volume = 0.1f;
		playerHealthPowerUp.PlayOneShot (playerHealed);

	}

	// Fonction qui gere la mort du joueur
    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

		// Empeche le mouvement apres la mort
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

	// Recommence le niveau apres la mort
    public void RestartLevel ()
    {
		// Le manque de parametre fait "reloader" le meme niveau
		// Pour retourner au start menu du jeu, il faudrait faire Application.loadedLevel(0)
		// mais nous avons préféré le laisser ainsi pour ne pas prendre trop longtemps entre
		// les parties.
        Application.LoadLevel (Application.loadedLevel); 
    }

	// Renvoit true is le player est mort
	public bool GetPlayerState() {
		return isDead;
	}

	// Fonction qui gere le timer
	public void ShowTimer()
	{
		
		if(!isDead) 		
			time += Time.deltaTime;
		
		double minutes = Math.Floor(time / 60.0);
		double seconds = time % 60.0f;

		timerLabel.text = string.Format ("{0:00} : {1:00}", minutes, seconds);
	}
}

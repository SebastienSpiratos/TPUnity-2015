using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
	
	public AudioClip attackSound;
	public AudioClip attackScream;
    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;
	AudioSource zombieAttackSound;
	AudioSource zombieAttackScream;


    void Awake ()
    {
		zombieAttackSound = GetComponent<AudioSource> ();
		zombieAttackScream = GetComponent<AudioSource> ();
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }

	// Gere le range de l'ennemi
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }

	// Gere le range de l'ennemi
    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
		// Compte le temps qui passe pour le temps entre les attaques
        timer += Time.deltaTime;

		// Attaque si possible
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0) {
			anim.SetBool("IsAttacking", true);
			Attack ();
		} else
			anim.SetBool("IsAttacking", false);

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }

	// Fonction d'attaque
    void Attack ()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
			anim.Play(GetRandomAnimation());
			zombieAttackSound.PlayOneShot(attackSound);
			zombieAttackScream.PlayOneShot(attackScream);
            playerHealth.TakeDamage (attackDamage);
        }
    }

	// Change l'animation d'attaque du zombie
	string GetRandomAnimation()
	{
		int randomAnimation = Random.Range(0, 3); 
		if (randomAnimation == 0)
			return "attack1";
		if (randomAnimation == 1)
			return "attack2";
		if (randomAnimation == 2)
			return "attack3";
		else
			return "attack1";
	}
}

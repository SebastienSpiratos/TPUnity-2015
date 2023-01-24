using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
	
    Animator anim;
    AudioSource enemyAudio;  
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
	ParticleSystem hitParticles;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
		hitParticles = GetComponentInChildren<ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
    	currentHealth = startingHealth;

    }


    void Update ()
    {
		// Fait couler les zombies dans le sol
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

	// Boolean qui controle l'animation de douleur du zombie
	void LateUpdate()
	{
		anim.SetBool ("isHit", false);
	}

	// Gere la perte de vie des ennemis
    public void TakeDamage (int amount, Vector3 hitPoint)
    {
		int meleeAmount;
		meleeAmount = PlayerShooting.GetMeleeDamageAmount ();
        if(isDead)
            return;
        enemyAudio.Play ();
		anim.SetBool ("isHit", true);
        currentHealth -= amount;
   		
		// Ne joue le particle effect que quand c'est un tir de laser qui frappe
		if(amount != meleeAmount){
	        hitParticles.transform.position = hitPoint;
			// Controle la couleur du laser
			if(DamagePowerUp.isPowered)
				hitParticles.startColor = Color.blue;
			else
				hitParticles.startColor = Color.green;
			hitParticles.Play ();
		}
		// Mort
        if(currentHealth <= 0)
        {
            Death ();
        }
    }

	// Gere la mort de l'ennemi
    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }

	// Donne les points et fait couleur l'ennemi dans le sol
    public void StartSinking ()
    {
        GetComponent <NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }


}

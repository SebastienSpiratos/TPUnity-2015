using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	public AudioClip ShootClip;
	public AudioClip meleeSoundClip;
	public AudioClip meleeSwingClip;
	public static int damagePerShot = 34;
	public static int damagePerMelee = 75;
	public float timeBetweenBullets = 0.15f;
	public float range = 100f;
	public float meleeRange = 1f;
	public float meleeStaminaCost = 15f;

	private float timer;
	private Ray shootRay;
	private RaycastHit shootHit;
	private int shootableMask;
	private ParticleSystem gunParticles;
	private LineRenderer gunLine;
	private AudioSource meleeSound;
	private AudioSource meleeSwing;
	private AudioSource gunAudio;
	private Light gunLight;
	private float effectsDisplayTime = 0.2f;
	Animator anim;
	
	private void Awake()
	{
		shootableMask = LayerMask.GetMask("Shootable");
		gunParticles = GetComponent<ParticleSystem>();
		gunLine = GetComponent<LineRenderer>();
		gunAudio = GetComponent<AudioSource>();
		gunLight = GetComponent<Light>();
		anim = GetComponentInParent<Animator>();
		meleeSound = GetComponentInParent<AudioSource>();
		meleeSwing = GetComponentInParent<AudioSource>();
	}
	
	private void Update()
	{
		// Calcule le temps qui passe entre les attaques
		timer += Time.deltaTime;
		// Tir du joueur
		if (Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0) {
			Shoot ();
		} 
		// Attaque corps a corps du joueur
		else if (Input.GetButton ("Fire2") && timer >= timeBetweenBullets && Time.timeScale != 0) {
			ThirdPersonCharacter player = GetComponentInParent<ThirdPersonCharacter>();
			// Cout de l'attaque en fatigue
			if(player.GetStaminaAmount() > 19){
				player.LoseStaminaAfterMeleeSwing(meleeStaminaCost);
				meleeSwing.clip = meleeSwingClip;
				meleeSwing.volume = 1;
			    meleeSwing.Play ();
				Melee();
			}
		}
		// Stop les animations
		else {
			anim.SetBool("IsShooting", false);
			anim.SetBool("IsMeleeing", false);
		}

		if (timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects();
		}
	}
	// Gere le laser et la lumiere du tir
	public void DisableEffects()
	{
		gunLine.enabled = false;
		gunLight.enabled = false;
	}
	// Gere l'attaque corps a corps
	private void Melee()
	{
		timer = 0f;
		
		anim.SetBool ("IsMeleeing", true);

		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;
		
		if (Physics.Raycast(shootRay, out shootHit, meleeRange, shootableMask))
		{
			EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

			if (enemyHealth != null)	{
				meleeSound.clip = meleeSoundClip;
				meleeSound.volume = 1;
				enemyHealth.TakeDamage(damagePerMelee, shootHit.point);
				meleeSound.Play();
			}
		}
	}
	// Retourne les degats fait par l'attaque au corps a corps
	public static int GetMeleeDamageAmount()
	{
		return damagePerMelee;
	}
	// Retourne les degats fait par l'attaque de tir
	public static int GetShootDamageAmount()
	{
		return damagePerShot;
	}
	// Change les degats de l'attaque de tir
	public static void SetShootDamageAmount(int amount)
	{
		damagePerShot = amount;
	}

	// Gere le tir du joueur et ses effets
	private void Shoot()
	{
		timer = 0f;

		anim.SetBool ("IsShooting", true);
		gunAudio.clip = ShootClip;
		gunAudio.volume = 0.5f;
		gunAudio.Play();
		
		gunLight.enabled = true;
		
		gunParticles.Stop();
		gunParticles.Play();
		
		gunLine.enabled = true;
		gunLine.SetPosition(0, transform.position);
		
		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;
		
		if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
		{
			EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

			if (enemyHealth != null)		
				enemyHealth.TakeDamage(damagePerShot, shootHit.point);

			gunLine.SetPosition(1, shootHit.point);
		}
		else
		{
			gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
		}
	}
}
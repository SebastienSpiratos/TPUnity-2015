using UnityEngine;
using System.Collections;

public class DamagePowerUp : MonoBehaviour {
	public static bool isPowered;
	public Texture normalLaser;
	public Texture poweredLaser;
	
	GameObject player;
	
	float speed = 15f;
	int damage;
	bool powerUpUsed;
	ParticleSystem laserOrigin;
	LineRenderer laser;
	AudioSource soundEffect;
	GameObject laserDot;
	Light laserDotColor;
	Light laserExplosionColor;
	GameObject laserCannon;
	
	void Awake ()
	{
		powerUpUsed = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		soundEffect = GetComponent<AudioSource> ();
		
	}
	// Gere la collection du powerup
	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == player)
		{
			if(!powerUpUsed)
			{
				powerUpUsed = true;
				StartCoroutine(PowerUpDamage());
				// Cache le powerup pendant son effet
				GetComponent<MeshRenderer>().enabled = false;
				GetComponent<BoxCollider>().enabled = false;
				GetComponentInChildren<Light>().enabled = false;
			}
		}
	}
	
	IEnumerator PowerUpDamage()
	{
		// N'applique que l'effet si le player n'a pas deja le powerup
		
		PlayerShooting.damagePerShot = 100;
		// 101 car les dégats du corps a corps doivent etre différents de celui du tir 
		PlayerShooting.damagePerMelee = 101; 
		
		// Change la couleur du laser de vert a bleu
		laserOrigin = player.GetComponentInChildren<ParticleSystem> ();
		laser = player.GetComponentInChildren<LineRenderer> ();
		laserDot = GameObject.FindGameObjectWithTag ("laserDot");
		laserCannon = GameObject.FindGameObjectWithTag ("laserCannon");
		laserDotColor = laserDot.GetComponent<Light> ();
		laserExplosionColor = laserCannon.GetComponent<Light>();
		laserExplosionColor.color = Color.blue;
		laserDotColor.color = Color.blue;
		soundEffect.Play ();
		laserOrigin.startColor = Color.blue;
		laser.material.mainTexture = poweredLaser;
		isPowered = true;
		// Duree du powerup
		yield return new WaitForSeconds(30);
		// Enleve l'effet du powerup
		isPowered = false;
		
		PlayerShooting.damagePerShot = 34;
		PlayerShooting.damagePerMelee = 75;
		
		// Remet sa couleur a vert
		laserOrigin.startColor = Color.green;
		laser.material.mainTexture = normalLaser;
		laserDotColor.color = Color.green;
		laserExplosionColor.color = Color.green;
		Destroy(gameObject);
	}
	// Fait tourner le powerup
	void Update () {
		transform.Rotate (Vector3.up, speed * Time.deltaTime);
	}
	
}

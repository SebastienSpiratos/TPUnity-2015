using UnityEngine;
using System.Collections;

public class ObjectHitParticleManager : MonoBehaviour {

	//AudioSource objectHitAudio; 
	//ParticleSystem hitParticles;

	void Awake ()
	{
		//objectHitAudio = GetComponent <AudioSource> ();
		//hitParticles = GetComponent<ParticleSystem> ();
	}

	public void HitObject (Vector3 hitPoint)
	{
		//objectHitAudio.Play ();

		//hitParticles.transform.position = hitPoint;
		//hitParticles.Play ();
	}
}

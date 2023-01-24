using UnityEngine;
using System.Collections;

// maintains position offset while orbiting around target

public class OrbitCamera : MonoBehaviour {
	// Target est l'objet autour duquel on veut que la caméra tourne
	[SerializeField] private Transform target;

	public float rotSpeed = 1.5f;

	private float _rotY;
	private Vector3 _offset;

	// Use this for initialization
	void Start() {
		_rotY = transform.eulerAngles.y;
		// _offset : la différence de position de départ entre la caméra et la cible
		// on veut maintenir cette meme distance tout le long ...
		_offset = target.position - transform.position;
	}
	
	// Update is called once per frame
	// LateUpdate est aussi appelé a chaque frame mais apres le Update
	// On l'utilise ici pour s'assurer que la caméra se positionne APRES
	// les update de la cible (target) que la caméra suit
	void LateUpdate() {
		float horInput = Input.GetAxis("Horizontal");
		// on tourne lentement autour de la cible avec les fleches horizontales (horinput !=0) ou
		// rapidement avec la souris (else si aucun bouton n'est appuyé)
		if (horInput != 0) {
			_rotY += horInput * rotSpeed;
		} else {
			_rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
		}
		// l'angle de rotation est convertit en Quaternion avec Quaternion.Euler
		Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
		// rotation * offset : une position * quaternion résulte en une nouvelle position
		// résultante apres la rotation 
		// target.position - (rotation * _offset) positionne donc la caméra derriere cette nouvelle position
		transform.position = target.position - (rotation * _offset);
		// peu importe ou se trouve la caméra par rapport a la cible
		// on veut faire face a la cible
		// sans cette étape, la caméra serait bien positionnée mais ne pointerait pas 
		// nécessairement dans la direction de la cible
		transform.LookAt(target);
	}
}

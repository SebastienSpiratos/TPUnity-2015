using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {
	//[SerializeField] expose la variable dans l'inspecteur mais ne permet pas a d'autres scripts
	// de la modifier
	public GameObject enemyPrefab;
	private GameObject _enemy;
	public int offsetw;
	public int offseth;
	public int divide = 1;

	void Start() {

		for(int i = 0; i != 1; ++i) {
			_enemy = Instantiate(enemyPrefab) as GameObject;
			_enemy.transform.position = new Vector3(1187, 13, 1103);
			float angle = Random.Range(0, 360);
			_enemy.transform.Rotate(0, angle, 0);
		}
		
	}
	void Update() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
		// on spawn seulement un ennemi s'il n'y en a pas déja six
		if (enemies.Length < 6) {
			// instancie le prefab qui sert d'ennemi
			_enemy = Instantiate(enemyPrefab) as GameObject;
			_enemy.transform.position = new Vector3(1187, 13, 1103);
			float angle = Random.Range(0, 360);
			_enemy.transform.Rotate(0, angle, 0);
		}
	}
}

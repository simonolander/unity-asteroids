using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public float startDelay;
	public float spawnDelay;

	public List<Asteroid> asteroids;
	public Text scoreText;
	public GameObject gameOverText;

	private bool gameOver;
	private int score;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnAsteroid ());
		gameOverText.SetActive(false);
	}

	IEnumerator SpawnAsteroid() {
		yield return new WaitForSeconds (startDelay);
		while (true) {
			Vector3 position = new Vector3 (
				Random.Range (-10, 10),
				0,
				16
			);
			Vector3 target = new Vector3 (
				Random.Range(-7, 7),
				0,
				-16
			);
			Vector3 velocity = (target - position).normalized * Random.Range (1, 10);
			Asteroid asteroid = asteroids [Random.Range (0, asteroids.Count)];
			Asteroid instance = Instantiate (asteroid, position, Quaternion.identity);
			instance.velocity = velocity;
			yield return new WaitForSeconds (spawnDelay);
		}
	}

	void Update () {
		if (!gameOver) {
			UpdateScore ();
		} else {
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex, LoadSceneMode.Single);
			}
		}
	}

	void UpdateScore () {
		score += Mathf.RoundToInt(Time.deltaTime * 1000);
		scoreText.text = score.ToString();
	}

	public void OnGameOver() {
		gameOver = true;
		gameOverText.SetActive(true);
	}
}

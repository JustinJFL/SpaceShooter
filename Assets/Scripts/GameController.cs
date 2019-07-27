using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text scoreText;
    private int score;
    public Text restartText;
    public Text gameOverText;
    public GameObject player;

    bool gameOver;
    bool restart;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        Screen.SetResolution(600, 900, true);
    }

    private void Update()
    {

        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }




    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
                if (gameOver == true)
                {
                    restart = true;
                    restartText.text = "Press \"Spacebar\" to restart.";
                    break;
                }
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
        scoreText.text = "";
    }
    
    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;

        if(score >= 200)
        {
            gameOverText.text = "You Win! \n Game Created By: Justin Snell";
            scoreText.text = "";
            gameOver = true;
        }
        else if(gameOver == true)
        {
            scoreText.text = "";
            gameOver = true;
        }
    }
}



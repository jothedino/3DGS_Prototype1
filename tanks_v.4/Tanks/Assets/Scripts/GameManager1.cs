using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour {

    public int kill;
    public int kill2;
    public int goal = 3;
    public Text killText, killText2, player1Text, player2Text, winText, winText2;
    public AudioSource backgroundMusic, gameStateSounds;
    public string startScene;

    public GameObject Player1;
    public GameObject Player2;

    private bool gameOver;
    private float gameOverDelay = 5f;
    public bool ready;

    // Use this for initialization
    void Start () {
        kill = 0;
        kill2 = 0;
        gameOver = false;


    }
    public void Kill(int points)
    {
        // kill += points;

        kill++;
        
        killText.text = "Points: " + kill.ToString("00"); // "00" Formats the score to two digits when displayed
        //killText2.text = "Kills: " + kill.ToString("00");

        // Only allow winning if the goal is achieved and the game has not already ended (if you've lost, for instance!)
        if (kill >= goal && !gameOver)
        {

            Win();
        }
    }
    public void Kill2(int points)
    {
        // kill2 += points;

        kill2++;
        killText2.text = "Points: " + kill2.ToString("00"); // "00" Formats the score to two digits when displayed


        // Only allow winning if the goal is achieved and the game has not already ended (if you've lost, for instance!)
        if (kill2 >= goal && !gameOver)
        {
            Win2();
        }
    }



    // Update is called once per frame
    void Update () {
	
	}
    private void Win()
    {
        // End the game if it hasn't already been ended. This ensures that the sounds and coroutines are not run multiple times
        if (!gameOver)
        {
            gameOver = true;
            winText.gameObject.SetActive(true);
            StartCoroutine(GameOver());
        }


    }

    private void Win2()
    {
        // End the game if it hasn't already been ended. This ensures that the sounds and coroutines are not run multiple times
        if (!gameOver)
        {
            gameOver = true;
            winText2.gameObject.SetActive(true);
            StartCoroutine(GameOver());
        }


    }
    // Handles a brief delay before restarting level
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(gameOverDelay);     // Delay before resetting
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   // Restart current level. Be sure to add: using UnityEngine.SceneManagement; at top
        SceneManager.LoadScene(startScene);
    }
}

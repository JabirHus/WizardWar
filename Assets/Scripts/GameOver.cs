using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOver : MonoBehaviour
{

    //references to buttons 
    public Button playAgain;
    public Button mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        //Adding button listeners
        playAgain.onClick.AddListener(restartGame);
        mainMenu.onClick.AddListener(GoToMainMenu);
    }

    public void GoToMainMenu(){
        Debug.Log("Going to Main Menu");
        Time.timeScale = 1f; // Reset time scale to normal
        SceneManager.LoadScene("MainMenu"); 
    }

    public void restartGame(){
       Debug.Log("Restarting the game");
       Time.timeScale = 1f; // Reset time scale to normal
       SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads current scene
    }

}

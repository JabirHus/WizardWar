using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  

public class MainMenu : MonoBehaviour
{
    //References 
    public GameObject difficultyPanel; //loads when play button clicked
    public Button playGame;
    public Button quitGame;
    public Button MediumButton; //to load gameplay

    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("MainMenu");

        //listeners for buttons and functions to do
        playGame.onClick.AddListener(OpenDifficultyPanel);
        quitGame.onClick.AddListener(QuitGame);
        MediumButton.onClick.AddListener(LoadMediumGame);

        difficultyPanel.SetActive(false); //hidden until play game button pressed
    }

    public void OpenDifficultyPanel()
    {
        difficultyPanel.SetActive(true); //hidden until play game button pressed
    }

    
    public void LoadMediumGame()
    {
        SceneManager.LoadScene("Medium");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

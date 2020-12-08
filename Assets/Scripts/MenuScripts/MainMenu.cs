using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Level Variables
    [SerializeField]
    private int FirstLevel;
    [SerializeField]
    private string PracticeArena = null;

    // in menu pages/changes:
    [SerializeField]
    private GameObject Menu = null;
    [SerializeField]
    private GameObject SettingsMenu = null;
    [SerializeField]
    private GameObject QuitPrompt = null;

    //Start the Game things:
    public void StartGame(){
        SceneManager.LoadScene(FirstLevel);
    }

    //Go to: practice Area
    public void PracticeArea(){
        SceneManager.LoadScene(PracticeArena);
    }

    //Settings Menus:
    //turn Settings menu on
    public void SettingsOn(){
        SettingsMenu.SetActive(true);
        Menu.SetActive(false);
    }
    //turn settings menu off
    public void SettingsOff(){
        SettingsMenu.SetActive(false);
        Menu.SetActive(true);
    }

    




    //Quit things:

    //when selecting the quit button bring up the Quit Yes/no option
    public void QuitPromptOn()
    {
        QuitPrompt.SetActive(true);
        Menu.SetActive(false);
    }

    //selecting No and closing the quit prompt
    public void QuitPromptOff()
    {
        QuitPrompt.SetActive(false);
        Menu.SetActive(true);
    }
    
    //selecting yes and quitting the game
    public void QuitGame()
    {
        Application.Quit();
    }

















}

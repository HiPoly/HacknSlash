using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //Level Variables
    [SerializeField]
    private string FirstLevel;




    // in menu pages/changes:

    [SerializeField]
    private GameObject SettingsMenu;

    [SerializeField]
    private GameObject QuitPrompt;








    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Start the Game things:

    public void StartGame()
    {
        SceneManager.LoadScene(FirstLevel);
    }




    //Settings Menus:

    //turn Settings menu on
    public void SettingsOn()
    {
        SettingsMenu.SetActive(true);
    }

    //turn settings menu off
    public void SettingsOff()
    {
        SettingsMenu.SetActive(false);
    }






    //Quit things:

    //when selecting the quit button bring up the Quit Yes/no option
    public void QuitPromptOn()
    {
        QuitPrompt.SetActive(true);
    }

    //selecting No and closing the quit prompt
    public void QuitPromptOff()
    {
        QuitPrompt.SetActive(false);
    }
    
    //selecting yes and quitting the game
    public void QuitGame()
    {
        Application.Quit();
    }

















}

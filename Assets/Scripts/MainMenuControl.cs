using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject GameModeUI;
    public GameObject LoadGameUI;
    public GameObject NetworkUI;
    public GameObject CustomizationUI;
    public GameObject SettingsUI;

    private void Start()
    {
        OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        changeUI(MainMenuUI);
    }

    public void changeUI(GameObject UI)
    {
        MainMenuUI.SetActive(false);
        GameModeUI.SetActive(false);
        LoadGameUI.SetActive(false);
        NetworkUI.SetActive(false);
        CustomizationUI.SetActive(false);
        SettingsUI.SetActive(false);

        UI.SetActive(true);
    }

    public void exitGame()
    {
        Application.Quit();
    }
    public void newGame()
    {
        SceneManager.LoadScene("Test_Stage");
    }

    public void tutorial()
    {
        SceneManager.LoadScene("tutorial.scene");
    }

    public void training()
    {
        SceneManager.LoadScene("training.scene");
    }

    public void playerCustomization()
    {
        SceneManager.LoadScene("PlayerCustomization.scene");
    }

    public void shipCustomization()
    {
        SceneManager.LoadScene("ShipCustomization.scene");
    }

    public void bunkCustomization()
    {
        SceneManager.LoadScene("BunkCustomization.scene");
    }
}

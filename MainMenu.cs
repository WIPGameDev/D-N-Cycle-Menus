using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IPersist
{
    [SerializeField] Button newGameButton;
    [SerializeField] Button continueButton;
    [SerializeField] GameObject warningBox;
    [SerializeField] GamePersist gamePersist;
    private bool newGameWarning = false;

    public void Load(GameData gameData)
    {
        newGameWarning = gameData.newGameWarning;

        ToggleContinueButton();
    }

    public void Save(GameData gameData)
    {
        gameData.newGameWarning = newGameWarning;
    }

    private void Awake()
    {
        gamePersist = GetComponent<GamePersist>();
    }

    private void ToggleContinueButton()
    {
        if (newGameWarning)
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }

    public void NewGame()
    {
        if(newGameWarning)
        {
            warningBox.SetActive(true);
        }
        else
        {
            newGameWarning = true;
            gamePersist.Save();

            SceneManager.LoadScene(1);
        }
    }

    public void Continue()
    {
        SceneManager.LoadScene(1);
    }

    public void DeleteSaveGame()
    {
        gamePersist.FullWipe();
        Destroy(gamePersist);
        Debug.Log(gamePersist);
        gameObject.AddComponent<GamePersist>();      
        gamePersist = GetComponent<GamePersist>();
    }
}

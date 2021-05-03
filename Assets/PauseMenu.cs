using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public int maxHistoryLenght = 5;

    private GameObject activeMenu;
    private int currentHistory = 0;
    private GameObject[] menuHistory;

    // Start is called before the first frame update
    void Start()
    {
        menuHistory = new GameObject[maxHistoryLenght];
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: make it with events
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }    

    /// <summary>
    /// Swaps the active menu with the given new Menu
    /// </summary>
    /// <param name="newMenu">The new Active menu</param>
    void SwapActiveMenu(GameObject newMenu)
    {
        activeMenu.SetActive(false);
        newMenu.SetActive(true);
        activeMenu = newMenu;
    }

    /// <summary>
    /// Sets a new Menu and recalls it in the history
    /// </summary>
    /// <param name="newMenu">The new Active Menu</param>
    public void ClimbMenu(GameObject newMenu)
    {
        if (currentHistory + 1 <= maxHistoryLenght)
        {
            menuHistory[++currentHistory] = newMenu;
            SwapActiveMenu(newMenu);
        }
        else
        {
            Debug.LogWarning("Menu history is too small please increase Menu history size");
        }
    }

    /// <summary>
    /// Sets the menu before the current one as active menu
    /// </summary>
    public void DescendMenu()
    { 
        if (currentHistory - 1 >= 0)
        {
            menuHistory[currentHistory] = null;
            SwapActiveMenu(menuHistory[--currentHistory]);
        }
        else
        {
            Debug.LogWarning("Reached Menu history minimum but tried to go smaller");
        }
    }

    /// <summary>
    /// Opens the Menu
    /// </summary>
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        menuHistory[0] = activeMenu = pauseMenuUI;        
        GameIsPaused = true;        
    }

    /// <summary>
    /// Either decends one Menu or exits all menus if the current menu is the pause menu
    /// </summary>
    public void Resume()
    {
        //decides if it should leave the menu or of it should just decend one menu
        if (activeMenu == pauseMenuUI)
        {
            menuHistory = new GameObject[maxHistoryLenght];
            activeMenu.SetActive(false);
            activeMenu = null;
            currentHistory = 0;
            GameIsPaused = false;
        }
        else
        {
            DescendMenu();
        }

    }

    /// <summary>
    /// Gets called by the end button over Unity events
    /// </summary>
    public void clickEnd()
    {
        Application.Quit();
    }
}

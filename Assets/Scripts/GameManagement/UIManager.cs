using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<GameObject> menus;
    private bool isOpen = false;

    private void ToggleMenu(GameObject menu)
    {
        isOpen = !menu.activeSelf;
        CloseAllMenus();
        menu.SetActive(isOpen);
        Time.timeScale = isOpen ? 0 : 1;
    }

    private void CloseAllMenus()
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) ToggleMenu(menus[0]);
        if (Input.GetKeyDown(KeyCode.G)) ToggleMenu(menus[1]);
        if (Input.GetKeyDown(KeyCode.I)) ToggleMenu(menus[2]);
        if (Input.GetKeyDown(KeyCode.Q)) ToggleMenu(menus[3]);
    }
}

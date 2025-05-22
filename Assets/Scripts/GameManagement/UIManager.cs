using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public List<GameObject> menus;
    private bool isOpen = false;

    [SerializeField] private GameObject menuPrefab;
    private GameObject UI;

    [SerializeField] private GameObject dialogUIPrefab;
    private GameObject dialogUI;

    [SerializeField] private GameObject statUIPrefab;
    private GameObject statUI;
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            UI = Instantiate(menuPrefab);
            menus = new List<GameObject>();

            for (var i = 0; i < UI.transform.childCount; i++)
            {
                menus.Add(UI.transform.GetChild(i).gameObject);
            }
            
            DontDestroyOnLoad(UI);
            
            dialogUI = Instantiate(dialogUIPrefab);
            dialogUI.SetActive(false);
            DontDestroyOnLoad(dialogUI);
            
            statUI = Instantiate(statUIPrefab);
            DontDestroyOnLoad(statUI);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DialogManager.Instance.dialogUI = dialogUI;
        DialogManager.Instance.playerUI = statUI;
        DialogManager.Instance.dialogText = dialogUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        
        Player.Player.Instance.healthText = statUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        Player.Player.Instance.levelText = statUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        Player.Player.Instance.manaText = statUI.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        Player.Player.Instance.moneyText = statUI.transform.GetChild(6).GetComponent<TextMeshProUGUI>();

        AchievementManager.Instance.achievementText = statUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        
        Inventory.Instance.potionText = statUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        Inventory.Instance.weaponText = statUI.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        
        QuestManager.Instance.questText = statUI.transform.GetChild(8).GetComponent<TextMeshProUGUI>();
        
        TimeManager.Instance.timeText = statUI.transform.GetChild(7).GetComponent<TextMeshProUGUI>();
    }
    
    public void ToggleMenu(GameObject menu)
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
        if (Input.GetKeyDown(KeyCode.P)) ToggleMenu(menus[3]);
        if (Input.GetKeyDown(KeyCode.G)) ToggleMenu(menus[0]);
        if (Input.GetKeyDown(KeyCode.I)) ToggleMenu(menus[1]);
        if (Input.GetKeyDown(KeyCode.Q)) ToggleMenu(menus[5]);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAllMenus();
            Time.timeScale = 1;
        }
    }
}

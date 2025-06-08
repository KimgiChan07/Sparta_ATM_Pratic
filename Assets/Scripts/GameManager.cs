using System.Collections;
using TMPro;
using UnityEngine;
using File = System.IO.File;

[System.Serializable]
public class UserData
{
    public string name;
    public int cash;
    public int balance;

    public string id;
    public string password;

    public UserData(string _name, int _cash, int _balance, string _id, string _password)
    {
        name = _name;
        cash = _cash;
        balance = _balance;
        id = _id;
        password = _password;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI curCash;
    public TextMeshProUGUI curBalance;
    public TextMeshProUGUI cureName;

    public GameObject popupBank;
    public GameObject popupLogin;

    public GameObject loginFailPanel;
    public GameObject bankFailPanel;
    public GameObject remitFailPanel;

    public UserData userData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        popupBank.SetActive(false);
        popupLogin.SetActive(true);
    }

    public void SaveUserData()
    {
        if (userData == null) return;

        string path = Application.persistentDataPath + $"/userdata_{userData.name}.json"; 
        string json = JsonUtility.ToJson(userData, true);
        File.WriteAllText(path, json);
    }

    public void Refresh(string _name, int _cash, int _balance)
    {
        if (userData == null) return;

        userData.name = _name;
        userData.cash = _cash;
        userData.balance = _balance;

        UpdateUI();
        SaveUserData();
    }

    public void UpdateUI()
    {
        if (userData == null) return;

        curCash.text = userData.cash.ToString("N0");
        curBalance.text = userData.balance.ToString("N0");
        cureName.text = userData.name;
    }

    public void ShowFailPanel(string _type)
    {
        if (_type == "login")
        {
            if (bankFailPanel != null)
            {
                bankFailPanel.SetActive(false);
                remitFailPanel.SetActive(false);
            }

            loginFailPanel?.SetActive(true);
            
            StartCoroutine(HidePanelAfterDelay(loginFailPanel));
        }
        else if (_type == "bank")
        {
            if (loginFailPanel != null)
            {
                loginFailPanel.SetActive(false);
                remitFailPanel.SetActive(false);
            }

            bankFailPanel?.SetActive(true);
            
            StartCoroutine(HidePanelAfterDelay(bankFailPanel));
        }else if (_type == "remit")
        {
            if (loginFailPanel != null)
            {
                loginFailPanel.SetActive(false);
                bankFailPanel.SetActive(false);
                remitFailPanel.SetActive(true);
            }

            remitFailPanel?.SetActive(true);
            
            StartCoroutine(HidePanelAfterDelay(remitFailPanel));
        }
    }

    private IEnumerator HidePanelAfterDelay(GameObject panel)
    {
        yield return new WaitForSeconds(1.5f);
        if (panel != null) panel.SetActive(false);
    }
}
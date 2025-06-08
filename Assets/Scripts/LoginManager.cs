using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputId;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private GameObject loginFailPanel;
    [SerializeField] private TextMeshProUGUI loginFailText;

    private void Start()
    {
        loginFailPanel.SetActive(false);
    }
    
    private string GetSavePath(string _name)
    {
        return Application.persistentDataPath + $"/userdata_{_name}.json";
    }

    public void TryLogin()
    {
        if (string.IsNullOrWhiteSpace(inputId.text) || string.IsNullOrWhiteSpace(inputPassword.text))
        {
            loginFailText.text = "ID와 비밀번호를 입력하세요!";
            GameManager.Instance.ShowFailPanel("login");
            return;
        }
        string path = GetSavePath(inputName.text);
        
        if (!File.Exists(path))
        {
            loginFailText.text = "로그인 정보가 없습니다.";
            GameManager.Instance.ShowFailPanel("login");
            return;
        }
        
        string json = File.ReadAllText(path);
        UserData saveData = JsonUtility.FromJson<UserData>(json);

        if (saveData.id == inputId.text && saveData.password == inputPassword.text)
        {
            GameManager.Instance.userData = saveData;
            GameManager.Instance.Refresh(saveData.name,saveData.cash, saveData.balance);
            GameManager.Instance.popupLogin.gameObject.SetActive(false);
            GameManager.Instance.popupBank.gameObject.SetActive(true);
        }
        else
        {
            loginFailText.text = "id 또는 패스워드가 틀렸습니다.";
        }
    }
    
    public void TryRegister()
    {
        if (string.IsNullOrWhiteSpace(inputId.text) || string.IsNullOrWhiteSpace(inputPassword.text)
                                                    || string.IsNullOrWhiteSpace(inputName.text))
        {
            loginFailText.text = "모든 항목을 입력하세요!";
            GameManager.Instance.ShowFailPanel("login");
            return;
        }
        
        string[] files = Directory.GetFiles(Application.persistentDataPath, "userdata_*.json");
        
        foreach (var file in files)
        {
            string json = File.ReadAllText(file);
            UserData user = JsonUtility.FromJson<UserData>(json);

            if (user.id ==  inputId.text)
            {
                loginFailText.text = "이미 사용 중인 ID입니다.";
                GameManager.Instance.ShowFailPanel("login");
                return;
            }
            
            if (user.name == inputName.text)
            {
                loginFailText.text = "이미 사용 중인 이름입니다.";
                GameManager.Instance.ShowFailPanel("login");
                return;
            }
        }
        
        UserData newUserData= new UserData(inputName.text,100000,50000,inputId.text,inputPassword.text);
        string newJson = JsonUtility.ToJson(newUserData);
        string path = GetSavePath(inputName.text);
        File.WriteAllText(path, newJson);
        
        GameManager.Instance.userData = newUserData;
        GameManager.Instance.Refresh(newUserData.name, newUserData.cash, newUserData.balance);
        
        loginFailText.text = "회원가입 성공!";
        GameManager.Instance.ShowFailPanel("login");
    }

}

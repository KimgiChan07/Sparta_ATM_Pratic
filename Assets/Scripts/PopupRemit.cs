using System.IO;
using TMPro;
using UnityEngine;

public class PopupRemit : MonoBehaviour
{
    public TMP_InputField inputTargetNameInputField;
    public TMP_InputField inputAmountInputField;
    public TextMeshProUGUI remitFailText;

    public void Remit()
    {
        string targetName = inputTargetNameInputField.text.Trim();
        string amountStr = inputAmountInputField.text.Trim();
        
        if (string.IsNullOrEmpty(targetName) || string.IsNullOrEmpty(amountStr))
        {
            ShowFail("모든 항목을 입력하세요.");
            return;
        }

        if (!int.TryParse(amountStr, out int amount) || amount <= 0)
        {
            ShowFail("송금액은 0보다 큰 숫자여야 합니다.");
            return;
        }

        var data = GameManager.Instance.userData;

        if (data.name == targetName)
        {
            ShowFail("본인에게 송금해서 머합니까.");
            return;
        }

        if (data.balance <= amount)
        {
            ShowFail("잔액이 부족합니다.");
            return;
        }
        
        string targetPath = Application.persistentDataPath + $"/userdata_{targetName}.json"; //파일이 존재하면 이걸 읽어오고 없으면 생성

        if (!File.Exists(targetPath))
        {
            ShowFail("존재하지 않는 대상입니다.");
            return;
        }
        string json = File.ReadAllText(targetPath);
        UserData targetData= JsonUtility.FromJson<UserData>(json);

        data.balance -= amount;
        targetData.balance += amount;
        
        GameManager.Instance.SaveUserData();
        File.WriteAllText(targetPath, JsonUtility.ToJson(targetData,true));
        
        GameManager.Instance.UpdateUI();
        
    }

    private void ShowFail(string message)
    {
        remitFailText.text = message;
        GameManager.Instance.ShowFailPanel("remit");
    }
    
}

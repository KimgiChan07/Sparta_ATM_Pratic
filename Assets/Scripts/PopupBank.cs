using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    public Button withdrawalInfo_Btn;
    public Button depositInfo_Btn;

    public TextMeshProUGUI failText;

    public GameObject withdrawalInfo;
    public GameObject depositInfo;
    public GameObject remitInfo;
    public GameObject buttonsInfo;
    public GameObject loginPanel;

    public TMP_InputField WithdrawalInfo_txt;
    public TMP_InputField DepositInfo_txt;

    private void Start()
    {
        withdrawalInfo.gameObject.SetActive(false);
        depositInfo.gameObject.SetActive(false);
        remitInfo.gameObject.SetActive(false);
        buttonsInfo.gameObject.SetActive(true);
        
        withdrawalInfo_Btn.onClick.AddListener(Show_WithdrawalInfo);
        depositInfo_Btn.onClick.AddListener(Show_DepositInfo);
    }

    public void Show_WithdrawalInfo()
    {
        buttonsInfo.SetActive(false);
        remitInfo.SetActive(false);
        withdrawalInfo.SetActive(true);
    }

    public void Show_RemitInfo()
    {
        buttonsInfo.SetActive(false);
        remitInfo.SetActive(true);
        depositInfo.SetActive(false);
    }
    public void Show_DepositInfo()
    {
        buttonsInfo.SetActive(false);
        remitInfo.SetActive(false);
        depositInfo.SetActive(true);
    }

    public void Show_Buttons()
    {
        buttonsInfo.gameObject.SetActive(true);
        remitInfo.gameObject.SetActive(false);
        withdrawalInfo.gameObject.SetActive(false);
        depositInfo.gameObject.SetActive(false);
    }

    public void Show_LoginPanel()
    {
        loginPanel.SetActive(true);
        gameObject.SetActive(false);
    }
    
    public void Withdrawal(int _amount) //출금
    {
        Debug.Log("출금");
        
        var data = GameManager.Instance.userData;
        if (GameManager.Instance.userData.balance < _amount)
        {
            failText.text = "잔액 부족!";
            GameManager.Instance.ShowFailPanel("bank");
            return;
        }
        
        data.balance -= _amount;
        data.cash += _amount;
        GameManager.Instance.Refresh(data.name, data.cash, data.balance);
    }

    public void Deposit(int _amount) //입금
    {
        Debug.Log("입금");
        
        var data = GameManager.Instance.userData;
        
        if (GameManager.Instance.userData.cash < _amount)
        {
            failText.text = "현금 부족!";
            GameManager.Instance.ShowFailPanel("bank");
            return;
        }
        
        data.balance += _amount;
        data.cash -= _amount;
        GameManager.Instance.Refresh(data.name, data.cash, data.balance);
    }

    public void InputDeposit()
    {        
        string input = DepositInfo_txt.text;
        
        if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int inputAmount))
        {
            failText.text = "숫자를 입력하세요!";
            GameManager.Instance.ShowFailPanel("bank");
            return;
        }
        Debug.Log("입금");
        if (GameManager.Instance.userData.cash < inputAmount)
        {
            failText.text = "현금 부족!";
            GameManager.Instance.ShowFailPanel("bank");
            return;
        }
        Deposit(inputAmount);
    }

    public void InputWithdrawal()
    {
        string input = WithdrawalInfo_txt.text;
        
        if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int inputAmount))
        {
            failText.text = "숫자를 입력하세요!";
            GameManager.Instance.ShowFailPanel("bank");
            return;
        }
        
        if (GameManager.Instance.userData.cash < inputAmount)
        {
            failText.text = "현금 부족!";
            GameManager.Instance.ShowFailPanel("bank");
            return;
        }
        Withdrawal(inputAmount);
    }
}
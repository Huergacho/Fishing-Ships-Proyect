using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class QuestController : MonoBehaviour
{
    #region Serialized Fields

    [Header("Visuals")]

    [SerializeField] private GameObject model;
    [SerializeField] private Image itemNeededImage;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private TextMeshProUGUI itemQuantityText;
    [SerializeField] private TextMeshProUGUI timeLapsedText;
    [SerializeField] private Button deliverButton;
    [SerializeField] private Button cancelMissionButton;
    [SerializeField] private Button closeButton;
    public event Action<int> onDeliver;
    [Header("Dialogue Properties")]
    [SerializeField] private float readSpeed;
    #endregion
    private QuestZone lastQuestZone = null;

    private void Start()
    {
        closeButton.onClick.AddListener(HideQuest);
        deliverButton?.onClick.AddListener(SuscribeToButtons);

        model.SetActive(false);
    } 
    public void Deliver(int value)
    {
        onDeliver?.Invoke(value);
        deliverButton.GetComponentInChildren<TextMeshProUGUI>().text = "Delivered";
        ShowEmptyQuest();
        deliverButton.interactable = false;
    }
    public void SuscribeToButtons()
    {
        lastQuestZone.Deliver();
    }
    public void GetQuest(string currentDialogue, int reward, Sprite itemIcon,int quantityNeeded, bool hasFinished, QuestZone lastQuest)
    {
        questText.text = string.Empty;
        itemNeededImage.enabled = true;
        itemNeededImage.sprite = itemIcon;
        Color newColor = itemNeededImage.color;
        newColor.a = 1f;
        rewardText.text = $"{reward}";
        itemQuantityText.text = $"{quantityNeeded}";
        lastQuestZone = lastQuest;
        questText.text = currentDialogue;
        deliverButton.GetComponentInChildren<TextMeshProUGUI>().text = "Deliver";
        deliverButton.interactable = true;
    }
    public void ShowQuest()
    {
        model.SetActive(true);
    }
    public void HideQuest()
    {
        model.SetActive(false);
    }
    public void ShowEmptyQuest()
    {
        WipeInfo();
        model.SetActive(true);
    }
    private void WipeInfo()
    {
        itemQuantityText.text = string.Empty;
        itemNeededImage.enabled = false;
        rewardText.text = string.Empty;
        questText.text = "Wait for a new assigment";
    }
}

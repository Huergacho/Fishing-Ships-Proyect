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
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private Image itemNeededImage;
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
        deliverButton.interactable = false;
    }
    public void SuscribeToButtons()
    {
        lastQuestZone.Deliver();
    }
    public void GetQuest(string currentDialogue, int reward, Sprite itemIcon,int quantityNeeded, bool hasFinished, QuestZone lastQuest)
    {
        questText.text = string.Empty;
        itemNeededImage.sprite = itemIcon;
        rewardText.text = $"{reward}";
        itemQuantityText.text = $"{quantityNeeded}";
        lastQuestZone = lastQuest;
        questText.text = currentDialogue;
        model.SetActive(true);
        deliverButton.GetComponentInChildren<TextMeshProUGUI>().text = "Deliver";
        deliverButton.interactable = true;
    }

    public void HideQuest()
    {
        model.SetActive(false);
    }

}

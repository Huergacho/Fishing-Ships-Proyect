using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
public class QuestZone : MonoBehaviour
{
    [SerializeField] private QuestScriptableObject quests;
    [SerializeField]bool hasFinished;
    [SerializeField] private string currentDialogue;
    [SerializeField] private int quantityNeeded;
    [SerializeField] private ItemScriptableObject itemNeeded;
    [SerializeField] private int reward;
    [SerializeField] private float questCooldown;
    private float currentTime;
    bool startTimer;
    private void Start()
    {
        hasFinished = true;
        startTimer = false;
        OnQuest();
    }
    private void Update()
    {
        if(startTimer == false)
        {
            return;
        }
        else
        {
            currentTime += Time.deltaTime;
            if(currentTime >= questCooldown)
            {
                OnQuest();
                currentTime = 0;
                startTimer = false;
            }
        }
    }
    private void OnQuest()
    {
        if (hasFinished)
        {
            
            GenerateQuest();
            HudManager.Instance.QuestController.GetQuest(currentDialogue, reward, itemNeeded.ShowImage, quantityNeeded, hasFinished, this);
        }
        else
        {
            return;
        }
    }
    private void GenerateQuest()
    {
        var randomDialogue = Random.Range(0, quests.QuestDialogue.Length);
        currentDialogue = quests.QuestDialogue[randomDialogue];

        var randomQuantity = Random.Range(1, quests.QuantityNeeded);
        quantityNeeded = randomQuantity;

        var randomItem = Random.Range(0, quests.ItemsNeeded.Length);
        itemNeeded = quests.ItemsNeeded[randomItem];

        var randomReward = Random.Range(10, quests.MaxReward + 1);
        reward = randomReward;
        hasFinished = false;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == GameManager.instance.player.gameObject.layer)
        {
            OnQuest();
            HudManager.Instance.QuestController.GetQuest(currentDialogue, reward, itemNeeded.ShowImage, quantityNeeded, hasFinished, this);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == GameManager.instance.player.gameObject.layer)
        {
            HudManager.Instance.QuestController.HideQuest();

        }
    }
    public void Deliver()
    {
        if (HudManager.Instance.Inventory.GetItemQuantity(itemNeeded) >= quantityNeeded)
        {     
            
                HudManager.Instance.Inventory.RemoveFromInventory(itemNeeded, quantityNeeded);
                HudManager.Instance.QuestController.Deliver(reward);
                hasFinished = true;
                startTimer = true;
                //HudManager.Instance.QuestController.GetQuest(currentDialogue, reward, itemNeeded.ShowImage, quantityNeeded, hasFinished, this);
         
        }
        else
        {
            print("VOCE NO TEIM SUFICEMTE ITEM CARALHO");
        }
    }
}

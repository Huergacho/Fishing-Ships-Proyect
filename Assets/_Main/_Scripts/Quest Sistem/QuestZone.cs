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
    [SerializeField] private Transform interactImagePos;
    private void Start()
    {
        hasFinished = true;
        startTimer = false;
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
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer == GameManager.instance.player.gameObject.layer)
    //    {
    //        OnQuest();
    //        HudManager.Instance.QuestController.GetQuest(currentDialogue, reward, itemNeeded.ShowImage, quantityNeeded, hasFinished, this);
    //    }
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.layer == GameManager.instance.player.gameObject.layer)
    //    {
    //        HudManager.Instance.QuestController.HideQuest();

    //    }
    //}
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == GameManager.instance.player.gameObject.layer)
        {
            HudManager.Instance.InteractImage.Show(interactImagePos);
            if (Input.GetKeyDown(KeyCode.F))
            {
                OnQuest();
                HudManager.Instance.QuestController.GetQuest(currentDialogue, reward, itemNeeded.ShowImage, quantityNeeded, hasFinished, this);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == GameManager.instance.player.gameObject.layer)
        {
            HudManager.Instance.QuestController.HideQuest();
            HudManager.Instance.InteractImage.Hide();

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

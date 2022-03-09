using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using TMPro;
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
    [SerializeField] private Transform interactImagePos;
    [SerializeField] private TextMeshProUGUI _timeText;
    private void Start()
    {
        currentTime = questCooldown;
        GenerateQuest();
    }

    private void Update()
    {
        if (hasFinished)
        {
            if (!CanStartNewMission())
            {
                _timeText.text = ((int)currentTime).ToString();
                return; 
            }
            else
            {
                GenerateQuest();
            } 
            
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
            
        HudManager.Instance.QuestController.GetQuest(currentDialogue, reward, itemNeeded.ShowImage, quantityNeeded, hasFinished, this);
        hasFinished = false;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == GameManager.instance.player.gameObject.layer)
        {
           
            HudManager.Instance.InteractImage.Show(interactImagePos);
            if (Input.GetKey(KeyCode.F))
            {
                if (!hasFinished)
                {
                    HudManager.Instance.QuestController.GetQuest(currentDialogue, reward, itemNeeded.ShowImage, quantityNeeded, hasFinished, this);
                    HudManager.Instance.QuestController.ShowQuest();
                }
                else
                {
                    HudManager.Instance.QuestController.ShowEmptyQuest();
                }
                
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
        }
        else
        {
            print("VOCE NO TEIM SUFICEMTE ITEM CARALHO");
        }
    }

    private bool CanStartNewMission()
    {

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {

            currentTime = questCooldown;
            return true;
        }
        else
        {
            return false;
        }
    }
}

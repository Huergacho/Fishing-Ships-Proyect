using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName ="Quest List",menuName ="Quests",order = 0)]
class QuestScriptableObject : ScriptableObject
{
    [SerializeField] private string[] questDialogue;
    public string[] QuestDialogue => questDialogue;

    [SerializeField] private int quantityNeeded;
    public int QuantityNeeded => quantityNeeded;

    [SerializeField] private ItemScriptableObject[] itemsNeeded;
    public ItemScriptableObject[] ItemsNeeded => itemsNeeded;

    [SerializeField] private int maxReward = 20;
    public int MaxReward => maxReward;
}

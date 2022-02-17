using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "LootTable", menuName = "Loot Table")]
class LootTable:ScriptableObject
{
    [System.Serializable] public class RewardItems
    {
        //Stats
        public string itemName;
        public float weight;
    }


    [SerializeField] private List<RewardItems> itemsToRoll;

    [System.NonSerialized] private bool _isRunning = false;

    private float _totalWeight;

    private void Initialize()
    {
        if (!_isRunning)
        {
            _totalWeight = itemsToRoll.Sum(item => item.weight);
            _isRunning = true;
        }
    }
    public RewardItems GetRandomItem()
    {
        Initialize();
        float diceRoll = Random.Range(0, _totalWeight);
        foreach (var item in itemsToRoll)
        {
            if(diceRoll <= item.weight)
            {
                return item;
            }
            diceRoll -= item.weight;
        }
        throw new System.Exception("Generation Failed");
    }

}

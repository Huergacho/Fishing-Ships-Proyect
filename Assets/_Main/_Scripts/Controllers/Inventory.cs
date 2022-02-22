using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Inventory : MonoBehaviour
{
    [SerializeField] private List<IStorable> itemList = new List<IStorable>();
    [SerializeField] private int _maxItems;
    [SerializeField] private int _itemCount;
    public bool isFull { get; private set;}

    private void Start()
    {
        isFull = false;
    }
    public void AddToInventory(IStorable itemToAdd)
    {
        if(_maxItems <= itemList.Count -1)
        {
            isFull = true;
            return;
        }
        else
        {
            itemList.Add(itemToAdd);
            _itemCount++;
        }
    }
    public void RemoveFromInventory(IStorable itemToRemove)
    {
        if(itemList.Count - 1 <= -1)
        {
            return;
        }
        else if (itemList.Count - 1 >= 0 && itemList.Contains(itemToRemove))
        {
            itemList.Remove(itemToRemove);
            isFull = false;
        }
    }


}

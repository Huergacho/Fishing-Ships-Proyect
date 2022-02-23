using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FishCounter : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI counterText;
    [SerializeField] private TextMeshProUGUI hoverText;
    private Animator _animator;
    private int _actualFishes;
    private int _maxFishes;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        counterText.text = "";
    }
    private void Start()
    {
        HudManager.Instance.Inventory.OnRemoveItem += RemoveFishes;
        GameManager.instance.player.UpdateFishCount += UpdateFishesCount;
    }
    public void UpdateFishesCount(int fishes, int maxFishes)
    {
        _actualFishes = fishes;
        _maxFishes = maxFishes;
        if(counterText.text.Length == 0)
        {
            UpdateCounter();
        }
    }
    public void AddFishes()
    {
        hoverText.text = "+" + 1;
        _animator.Play("AddFish");
    }
    public void RemoveFishes()
    {
        hoverText.text = "-" + 1;
        _animator.Play("RemoveFish");
    }
    //Llamado por animacion
    public void UpdateCounter()
    {
        counterText.text = _actualFishes + "/" + _maxFishes;
    }

}

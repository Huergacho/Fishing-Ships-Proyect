using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FishCounter : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI text;
    private Animator _animator;
    private int _actualFishes;
    private int _maxFishes;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        text.text = "";
    }
    private void Start()
    {
        
    }
    public void UpdateFishesCount(int fishes, int maxFishes)
    {
        _actualFishes = fishes;
        _maxFishes = maxFishes;
        if(text.text.Length == 0)
        {
            UpdateCounter();
        }
        if(fishes != 0)
        {
        _animator.Play("AddFish");
        }
    }
    //Llamado por animacion
    public void UpdateCounter()
    {
        text.text = _actualFishes + "/" + _maxFishes;
    }

}

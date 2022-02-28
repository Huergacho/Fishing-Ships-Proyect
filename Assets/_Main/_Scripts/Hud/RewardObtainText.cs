using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RewardObtainText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private string[] presetTexts;

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void TextAppear()
    {
        animator.Play("TextAppear");
    }
    public void SetObtainedItem(string itemName)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("TextAppear"))
        {
            var textIndex = Random.Range(0, presetTexts.Length);
            text.text = presetTexts[textIndex] + $"<color=blue>{itemName}</color>";
            TextAppear();
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("TextAppear"))
        {
            animator.SetTrigger("Continue");
            text.text += " And a " + $"<color=blue>{itemName}</color>";
        }

       
    }
}

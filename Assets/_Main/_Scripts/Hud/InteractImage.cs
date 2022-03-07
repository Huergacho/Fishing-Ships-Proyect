using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class InteractImage : MonoBehaviour
{
    [SerializeField] private GameObject interactImage;

    private void Start()
    {
        interactImage.SetActive(false);
    }
    public void Show(Transform posToShow)
    {
        if (interactImage.activeInHierarchy)
        {
            return;
        }
        else
        {
            interactImage.SetActive(true);
            transform.position = posToShow.position;
        }
    }
    public void Hide()
    {
        if (!interactImage.activeInHierarchy)
        {
            return;
        }
        else
        {
            interactImage.SetActive(false);
        }
    }


}

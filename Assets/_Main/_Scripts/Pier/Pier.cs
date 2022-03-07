using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pier : MonoBehaviour
{
    [SerializeField] private GameObject shopCanvas => HudManager.Instance.PierShop.gameObject;
    [SerializeField] private Transform interactImagePos;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == GameManager.instance.player.gameObject.layer)
        {
            HudManager.Instance.InteractImage.Show(interactImagePos);
            if (Input.GetKeyDown(KeyCode.F))
            {
                shopCanvas.SetActive(true);
                HudManager.Instance.Inventory.ForceInventoryToShow(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == GameManager.instance.player.gameObject.layer)
        {
            shopCanvas.SetActive(false);
            HudManager.Instance.InteractImage.Hide();
            HudManager.Instance.Inventory.ForceInventoryToShow(false);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pier : MonoBehaviour
{
    [SerializeField] private GameObject shopCanvas => HudManager.Instance.PierShop.gameObject;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == GameManager.instance.player.gameObject.layer)
        {
            shopCanvas.SetActive(true);
            HudManager.Instance.Inventory.gameObject.SetActive(true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == GameManager.instance.player.gameObject.layer)
        {
            shopCanvas.SetActive(false);
            HudManager.Instance.Inventory.gameObject.SetActive(false);
        }
    }
}

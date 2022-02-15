using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pier : MonoBehaviour
{
    [SerializeField] private GameObject shopCanvas;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("AAAAAA");
        if (collision.gameObject.layer == GameManager.instance.player.gameObject.layer)
        {

            Debug.Log("AAAAAA");
            shopCanvas.SetActive(true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == GameManager.instance.player.gameObject.layer)
        {
            shopCanvas.SetActive(false);
        }
    }
}

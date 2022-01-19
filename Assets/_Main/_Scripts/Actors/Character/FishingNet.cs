using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FishingNet : MonoBehaviour
{
    [SerializeField]private String contactLayers;
    [SerializeField]private bool isFishing;
    private void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(contactLayers))
        {
            if (Input.GetMouseButton(0))
            {
                other.gameObject.GetComponent<FishController>().GetFish();
                isFishing = true;
            }
            else
            {
                isFishing = false;
            }
        }
    }
    
}

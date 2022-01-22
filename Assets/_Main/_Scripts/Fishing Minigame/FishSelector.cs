using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSelector : MonoBehaviour
{
    [SerializeField] private LayerMask fishLayers;
    [SerializeField] private Transform upBarrier;
    [SerializeField] private Transform bottomBarrier;
    private bool hasFished = false;
    [SerializeField] private float lerpTime;
    private void Start()
    {
    }
    private void Update()
    {
        PingPong();        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DetectFishes();
        }
        Debug.DrawRay(transform.position, -transform.TransformDirection(Vector3.right), Color.green);
        
    }
    void DetectFishes()
    {
        print("Detecting");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.TransformDirection(Vector3.right), out hit,3f, fishLayers))
        {
            print("GetFish");
            FishController.Instance.GetFish();
        }
        else
        {
            FishController.Instance.End();
            print("End");
        }
    }
    void PingPong()
    {
        var dist = Vector3.Distance(upBarrier.localPosition, bottomBarrier.localPosition);
        transform.localPosition = new Vector3(transform.localPosition.x,Mathf.PingPong(Time.time * lerpTime / 2, dist) - dist / 2f, transform.localPosition.z);
    }
}

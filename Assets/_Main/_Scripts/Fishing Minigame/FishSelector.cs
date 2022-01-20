using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSelector : MonoBehaviour
{
    [SerializeField] private ProbabilitySquare probabilitySquare;
    [SerializeField] private LayerMask fishLayers;
    [SerializeField] private Transform[] barriers;
    private Vector3 destiny;
    private float distanceBetweenBarriers;
    [SerializeField] private float lerpSpeed;
    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DetectFishes();
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f, fishLayers))
        {
            Debug.DrawLine(transform.position, -transform.right);
        }
    }
    void DetectFishes()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.right, out hit,Mathf.Infinity, fishLayers))
        {
            FishController.Instance.GetFish();
            probabilitySquare.DeleteSquare(hit.collider.gameObject);
        }
        else
        {
            FishController.Instance.End();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSelector : MonoBehaviour
{
    [SerializeField] private LayerMask fishLayers;
    [SerializeField] private Transform upBarrier;
    [SerializeField] private Transform bottomBarrier;
    [SerializeField] private float lerpTime;
    [SerializeField] private SpriteRenderer detectionLine;
    private bool hasFinished;

    private void Start()
    {
        FishController.IsMinigameRunning += MinigameEnd;
    }
    private void Update()
    {
        if(hasFinished == true)
        {
        ActionsInMinigame();
        }
    }

    void DetectFishes()
    {

            RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.TransformDirection(Vector3.right), out hit, 3f, fishLayers))
        {
            FishController.Instance.GetFish();
            //Mat Verde
            detectionLine.color = new Color(0, 1, 0);
        }
        else
        {
            FishController.Instance.End();
            detectionLine.color = new Color(1, 0, 0);
            hasFinished = true;
            //Mat Rojo
        }
    }

    void PingPong()
    {
             var dist = Vector3.Distance(upBarrier.localPosition, bottomBarrier.localPosition);
            transform.localPosition = new Vector3(transform.localPosition.x,Mathf.PingPong(Time.time * lerpTime / 2, dist) - dist / 2f, transform.localPosition.z);
    }
    void ActionsInMinigame()
    {
            detectionLine.color = new Color(1, 1, 0.5f);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DetectFishes();
            }
        PingPong();
    }
    void MinigameEnd(bool minigameState)
    {
        hasFinished = minigameState;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FishSelector : MonoBehaviour
{
    [SerializeField] private LayerMask fishLayers;
    [SerializeField] private Transform[] barriers;
    [SerializeField] private float baseLerpTime;
    [SerializeField] private Image detectionLine;
    [SerializeField] private float detectionDistance;
    private bool hasStarted;
    private float calculatedLerpTime;
    [SerializeField] private GameObject pez;
    //[SerializeField]private Color[] colorBucket = new Color[3];
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        FishMinigameController.Instance.IsMinigameRunning += ActionsInMinigame;
        Initialize();
    }
    private void Update()
    {
        if (!hasStarted)
        {
            return;
        }
        else
        {
            CheckForFishInLine();
            PingPong();
        }
    }

    void CheckForFishInLine()
    {
        var distance = Vector3.Distance(transform.position, pez.transform.position);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (distance <= detectionDistance)
            {
                // detectionLine.color = colorBucket[1];
                _animator.Play("Fish");
                FishMinigameController.Instance.Fish();
            }

            else
            {
                //detectionLine.color = colorBucket[2];
                _animator.Play("NoFish");
                FishMinigameController.Instance.OnEnd();
            }
        }
    }

    void PingPong()
    {
        var dist = Vector3.Distance(barriers[0].localPosition, barriers[1].localPosition);
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.PingPong(Time.time * calculatedLerpTime / 2, dist) - dist / 2f, transform.localPosition.z);
    }

    void ActionsInMinigame(bool isMinigameRunning)
    {
        if (isMinigameRunning)
        {
            Initialize();
        }
        else
        {
            OnEnd();
        }

    }
    void Initialize()
    {
        // detectionLine.color = colorBucket[0];
        calculatedLerpTime = baseLerpTime / GameManager.instance.player.FishSkill ;
        hasStarted = true;
    }
    void OnEnd()
    {
        hasStarted = false;
        
    }

}

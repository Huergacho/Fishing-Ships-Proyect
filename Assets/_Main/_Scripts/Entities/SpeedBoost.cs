using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour, IPickeable
{
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private float boostTime;
    private float _currentBoostTime;
    private bool _canBoost;
    private void Start()
    {
        _currentBoostTime = boostTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((playerLayerMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            _canBoost = true;
            _currentBoostTime = boostTime;
        }
    }
    private void Update()
    {
        if (_canBoost)
        {
            _currentBoostTime -= Time.deltaTime;
            if(_currentBoostTime > 0)
            {
                DoEffect();
            }
            else
            {
                StopBoost();
            }
        }
    }
    private void StopBoost()
    {
        _canBoost = false;
        _currentBoostTime = boostTime;
        GameManager.instance.player.GetBoosted(false);
    }

    public void DoEffect()
    {
        GameManager.instance.player.GetBoosted(true);
    }
}

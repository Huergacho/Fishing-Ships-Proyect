using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilitySquare : MonoBehaviour
{
    [SerializeField] private Transform[] barriers;
    [SerializeField] private GameObject indicator;
    [SerializeField] private float lerpSpeed;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Initialize()
    {
        float spawn = Random.Range(barriers[0].localPosition.y, barriers[barriers.Length - 1].localPosition.y);
        indicator.transform.position = new Vector3(indicator.transform.position.x, spawn, indicator.transform.position.z);
        indicator.SetActive(true);

    }
    private void Update()
    {
        
    }
    //public void DeleteSquare(GameObject indicatorToDelete)
    //{
    //    indicatorToDelete.SetActive(false);
    //}
}

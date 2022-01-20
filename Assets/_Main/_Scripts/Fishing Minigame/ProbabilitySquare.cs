using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilitySquare : MonoBehaviour
{
    [SerializeField] private Transform[] barriers;
    
    [SerializeField] private GameObject indicator;
    [SerializeField] private List<GameObject> indicatorList = new List<GameObject>();
    [SerializeField] private int indicatorQuantity;
    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            var indicatorClone = Instantiate(indicator,transform.parent);
            indicatorClone.transform.SetParent(this.transform);
            indicatorList.Add(indicatorClone);
            indicatorClone.SetActive(false);
        }
        gameObject.SetActive(false);
    }
    public void Initialize()
    {
        float spawn = Random.Range(barriers[0].localPosition.y, barriers[barriers.Length - 1].localPosition.y);
        
        indicatorQuantity = FishController.Instance.GetActualFishPond().GetActualFishes();
        print(indicatorQuantity);
        for (int i = 0; i < indicatorQuantity; i++)
        {
           var indicatorSelected = Random.Range(0, indicatorList.Count);
            if (indicatorList[indicatorSelected].activeInHierarchy == false)
            {
                indicatorList[indicatorSelected].SetActive(true);
                indicatorList[indicatorSelected].transform.localPosition = Vector3.up * spawn;
            }
        }
            

    }
    public void DeleteSquare(GameObject indicatorToDelete)
    {
        indicatorToDelete.SetActive(false);
    }
}

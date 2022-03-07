using System.Collections;
using UnityEngine;
using System;

public class PlayerInputs : MonoBehaviour, Iinput
{
    [SerializeField] private LayerMask contactLayers;
    [SerializeField] private LayerMask fishLayerMask;
    [SerializeField] private LayerMask speedBoostLayer;
    [SerializeField] private string waterTag;
    //private bool _canMove;
    public bool isOnMenue { get; private set;}
    private void Start()  
    {
        HudManager.Instance.isOnMenue += SetMenueState;
    }
    public bool IsMoving()
    {
        //if (_canMove)
        //{
            return Input.GetMouseButton(1);
        //}
        //else
        //{
        //    return false;
        //}

    }
    public bool isInteracting()
    {
        return Input.GetMouseButtonDown(0);
    }
    public void UpdateInputs()
    {
    }
    private void SetMenueState(bool state)
    {
        isOnMenue = state;
    }
    public FishPond DetectFishPond(Vector3 mousePos, float distanceToFish)
    {

        var hit = Physics.SphereCastAll(mousePos, 0.1f, mousePos, 3f, fishLayerMask);
        var distance = Vector3.Distance(transform.position, mousePos);
        FishPond actualFishPond;
        if (distance <= distanceToFish && hit.Length > 0)
        {
            foreach (var item in hit)
            {
                actualFishPond = item.collider.gameObject.GetComponent<FishPond>();
                return actualFishPond;
            }
            return null; 
        }
        else return null;
    }
    Ray CalculateMousePos()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    public Vector3 UpdateMousePosition()
    {

        //if (Physics.Raycast(CalculateMousePos(), out RaycastHit hitInfo, Mathf.Infinity))
        //{
        //    if ((contactLayers.value & (1 << hitInfo.transform.gameObject.layer)) > 0)
        //    {
        //        _canMove = true;

        //        return hitInfo.point;
        //    }
        //    else
        //    {
        //        _canMove = false;
        //        return hitInfo.point;
        //    }
        //}
        //else
        //{
        //    _canMove = false;

        //    return hitInfo.point;
        //}
        if (Physics.Raycast(CalculateMousePos(), out RaycastHit hitInfo, Mathf.Infinity, contactLayers))
        {
            return hitInfo.point;
        }
        else return Vector3.zero;

    }
    public bool WantShowInventory()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}
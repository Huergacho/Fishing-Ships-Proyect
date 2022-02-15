using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HudManager : MonoBehaviour
{
    [SerializeField] private FishCounter fishCounter;
    public FishCounter FishCounter => fishCounter;

    [SerializeField] private PierShop pierShop;

    public event Action<bool> isOnMenue;
    public PierShop PierShop => pierShop;
    private static HudManager instance;
    public static HudManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }
    public void EnterInMenu()
    {
        isOnMenue.Invoke(true);
    }
    public void ExitMenue()
    {
        isOnMenue.Invoke(false);
    }

}

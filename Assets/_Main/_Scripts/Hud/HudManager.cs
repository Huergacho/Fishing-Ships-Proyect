using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField] private FishCounter fishCounter;
    public FishCounter FishCounter => fishCounter;
    private static HudManager instance;
    public static HudManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }

}

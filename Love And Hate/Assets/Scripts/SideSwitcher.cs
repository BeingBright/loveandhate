using System;
using UnityEngine;

public class SideSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject neutral;
    [SerializeField] private GameObject cupid;
    [SerializeField] private GameObject devil;

    private void Awake()
    {
        devil.SetActive(false);
        cupid.SetActive(false);
        neutral.SetActive(true);
    }

    public void OnSideChange(Side newSide)
    {
        devil.SetActive(false);
        cupid.SetActive(false);
        neutral.SetActive(false);
        switch (newSide)
        {
            case Side.Neutral:
                neutral.SetActive(true);
                break;
            case Side.Cupid:
                cupid.SetActive(true);
                break;
            case Side.Devil:
                devil.SetActive(true);
                break;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnobChangeRail : MonoBehaviour
{
    public GameObject thisRail;
    public GameObject nextRail;

    public void ToggleRail()
    {
        thisRail.gameObject.SetActive(false);
        nextRail.gameObject.SetActive(true);
    }
}

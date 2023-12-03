using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIalogueTrigger : MonoBehaviour
{
    public GameObject talkUI;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        talkUI.SetActive(true);
    }
}

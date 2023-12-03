using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayText : MonoBehaviour
{
    public GameObject talkUI;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        Canvas myCanvas = talkUI.GetComponent<Canvas>();
        myCanvas.sortingLayerName = "top";
        myCanvas.sortingOrder = 100;
        talkUI.SetActive(true);
    }
}

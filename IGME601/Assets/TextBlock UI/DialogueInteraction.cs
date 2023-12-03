using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    public GameObject talkUI;
    private Player player;

    private void Update()
    {
        
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Canvas myCanvas = talkUI.GetComponent<Canvas>();
                myCanvas.sortingLayerName = "top";
                myCanvas.sortingOrder = 100;
            }
            
    }
}

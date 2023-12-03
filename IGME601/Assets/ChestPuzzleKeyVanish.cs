using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPuzzleKeyVanish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Doorknob")
        {
            Destroy(collision);
        }
    }
}

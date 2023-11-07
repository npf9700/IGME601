using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzleTrigger : MonoBehaviour
{
    public Player player;
    public GameObject doorknob;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Triggered");
            doorknob.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}

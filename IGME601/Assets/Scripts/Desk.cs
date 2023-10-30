using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    private bool hasActivated;
    private bool isPlayerNear;
    [SerializeField]
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (this.GetComponent<SpriteRenderer>().sprite.bounds.Intersects(player.GetComponent<SpriteRenderer>().sprite.bounds))
        //{

        //}
    }
}

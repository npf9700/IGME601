using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private ChestPuzzleSequence cps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckOverlap(player))
        {
            cps.BeginPuzzle();
            Destroy(this.gameObject);
        }
    }

    public bool CheckOverlap(Player p)
    {
        float x = p.transform.position.x;
        float y = p.transform.position.y;
        float width = p.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
        float height = p.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
        if (2.5f + this.transform.position.x < x - width)
            return false;
        if (2.5f + this.transform.position.y < y - height)
            return false;
        if (this.transform.position.x - 2.5f > x + width)
            return false;
        if (this.transform.position.y - 2.5f > y + height)
            return false;
        return true;
    }
}

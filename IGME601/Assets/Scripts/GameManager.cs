using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<Cabinet> cabinets;

    [SerializeField]
    private Player player;

    [SerializeField]
    private List<Door> doors;

    [SerializeField]
    private GameObject key;

    [SerializeField]
    private Transform keySpawn;

    private List<GameObject> keys;

    

    // Start is called before the first frame update
    void Start()
    {
        keys = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //Door Collision Checks
        for(int i = 0; i < doors.Count; i++)
        {
            if (doors[i].CheckOverlap(player))
            {
                if(Input.GetKeyDown(KeyCode.Space) && doors[i].IsLocked == false)
                {
                    if (doors[i].IsLeft)
                    {
                        player.ScreenTransition(-1);
                    }
                    else
                    {
                        player.ScreenTransition(1);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Space) && player.HasKey && doors[i].IsLocked)
                {
                    player.HasKey = false;
                    doors[i].IsLocked = false;
                }
            }
        }
        //Cabinet Collision Checks
        for (int i = 0; i < cabinets.Count; i++)
        {
            if (CheckOverlap(player, cabinets[i].gameObject))
            {
                if (Input.GetKeyDown(KeyCode.Space) && player.HeldPaperColor == cabinets[i].CabinetColor)
                {
                    player.IsHoldingItem = false;
                    cabinets[i].StoredSuccess = true;
                    cabinets[i].GetComponent<SpriteRenderer>().color = Color.yellow;
                    player.HeldPaperColor = Color.white;
                    Debug.Log("Stored!");
                    if (CheckCabinets())
                    {
                        keys.Add(Instantiate(key, keySpawn));
                    }
                    
                }
            }
        }

        if (keys.Count > 0 && keys[0] != null)
        {
            if (keys[0].GetComponent<Key>().CheckOverlap(player) && Input.GetKeyDown(KeyCode.Space))
            {
                player.HasKey = true;
                Destroy(keys[0].gameObject);
                keys[0] = null;
            }
        }
    }

    private bool CheckCabinets()
    {
        for(int i = 0; i < cabinets.Count; i++)
        {
            if(!cabinets[i].StoredSuccess)
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckOverlap(Player p, GameObject other)
    {
        float x = p.transform.position.x;
        float y = p.transform.position.y;
        float width = p.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
        float height = p.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
        if (other.GetComponent<SpriteRenderer>().sprite.bounds.size.x + other.transform.position.x < x - width)
            return false;
        if (other.GetComponent<SpriteRenderer>().sprite.bounds.size.y + other.transform.position.y < y - height)
            return false;
        if (other.transform.position.x - other.GetComponent<SpriteRenderer>().sprite.bounds.size.x > x + width)
            return false;
        if (other.transform.position.y - other.GetComponent<SpriteRenderer>().sprite.bounds.size.y > y + height)
            return false;
        return true;
    }
}

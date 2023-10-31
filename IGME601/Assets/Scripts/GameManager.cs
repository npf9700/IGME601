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
    private GameObject door;

    [SerializeField]
    private GameObject key;

    [SerializeField]
    private Transform keySpawn;

    private bool isDoorActive;
    private List<GameObject> keys;

    public bool IsDoorActive
    {
        get { return isDoorActive; }
        set { isDoorActive = value; }
    }

    public GameObject Door
    {
        get { return door; }
    }

    // Start is called before the first frame update
    void Start()
    {
        keys = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(door != null)
        {
            isDoorActive = true;
        }
        else
        {
            isDoorActive = false;
        }
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
            if (CheckOverlap(player, keys[0]))
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
        if (other.GetComponent<SpriteRenderer>().sprite.bounds.size.x*0.2f + other.transform.position.x < x - width)
            return false;
        if (other.GetComponent<SpriteRenderer>().sprite.bounds.size.y*0.2f + other.transform.position.y < y - height)
            return false;
        if (other.transform.position.x - other.GetComponent<SpriteRenderer>().sprite.bounds.size.x*0.2f > x + width)
            return false;
        if (other.transform.position.y - other.GetComponent<SpriteRenderer>().sprite.bounds.size.y*0.2f > y + height)
            return false;
        return true;
    }
}

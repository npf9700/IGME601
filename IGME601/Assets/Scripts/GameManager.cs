using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private GameObject doorknob;
    [SerializeField]
    private GameObject paper;
    [SerializeField]
    private GameObject specialPaper;

    [SerializeField]
    private List<Transform> paperSpots;
    private List<Color> paperColors;

    [SerializeField]
    private Transform keySpawn;

    [SerializeField]
    private InventoryUI inventory;

    private List<GameObject> keys;

    private int fileCount;

    private bool firstPuzzleSolved;


    // Start is called before the first frame update
    void Start()
    {
        keys = new List<GameObject>();
        fileCount = 0;
        paperColors = new List<Color>();
        paperColors.Add(Color.green);
        paperColors.Add(Color.red);
        paperColors.Add(Color.blue);
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
                    player.RemoveInventoryItem(key.GetComponent<SpriteRenderer>().sprite);
                    player.RemoveInventoryItem(doorknob.GetComponent<SpriteRenderer>().sprite);
                }
            }
        }
        //Cabinet Collision Checks
        for (int i = 0; i < cabinets.Count; i++)
        {
            if (CheckOverlap(player, cabinets[i].gameObject))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    for(int j = 0; j < inventory.UIItems.Count; j++)
                    {
                        if(inventory.UIItems[j].GetComponent<Image>().sprite == cabinets[i].DesiredSprite &&
                            inventory.UIItems[j].GetComponent<Image>().color == cabinets[i].CabinetColor)
                        {
                            player.RemoveInventoryItem(cabinets[i].DesiredSprite, cabinets[i].CabinetColor);
                            cabinets[i].StoreFile();
                            Debug.Log("Stored!");
                            if (CheckCabinets())
                            {
                                //keys.Add(Instantiate(key, keySpawn));
                                SpawnPapers();
                                ResetCabinets();
                                fileCount++;
                            }
                        }
                    }
                }
            }
        }

        //Key Collision Check
        if (keys.Count > 0 && keys[0] != null)
        {
            if (keys[0].GetComponent<Key>().CheckOverlap(player) && Input.GetKeyDown(KeyCode.Space))
            {
                player.HasKey = true;
                player.AddInventoryItem(keys[0]);
                keys[0].gameObject.SetActive(false);
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

    private void SpawnPapers()
    {
        if (fileCount < 2)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject pap = Instantiate(paper, paperSpots[i + (3 * fileCount)]);
                if (i == 0)
                {
                    pap.GetComponent<SpriteRenderer>().color = Color.red;
                }
                else if (i == 1)
                {
                    pap.GetComponent<SpriteRenderer>().color = Color.green;
                }
                else
                {
                    pap.GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }
        }
        else
        {
            GameObject pap = Instantiate(specialPaper, paperSpots[6]);
            pap.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    private void ResetCabinets()
    {
        for (int i = 0; i < cabinets.Count; i++)
        {
            cabinets[i].ResetCabinet();
        }
    }
}

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
    private List<GameObject> changeObjects;
    [SerializeField]
    private GameObject dreamTree;
    [SerializeField]
    private GameObject coffin;
    [SerializeField]
    private List<ChestPuzzleColor> chests;

    [SerializeField]
    private List<Transform> paperSpots;
    private List<Color> paperColors;

    [SerializeField]
    private Transform keySpawn;

    [SerializeField]
    private InventoryUI inventory;

    [SerializeField]
    private List<GameObject> keys;

    private int fileCount;

    private int knobInteractCount;

    private bool firstPuzzleSolved;

    public int KnobInteractCount
    {
        get { return knobInteractCount; }
        set { knobInteractCount = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //keys = new List<GameObject>();
        fileCount = 0;
        paperColors = new List<Color>();
        paperColors.Add(Color.green);
        paperColors.Add(Color.red);
        paperColors.Add(Color.blue);
        KnobInteractCount = 0;
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

                        if (fileCount == 1 && changeObjects[0].gameObject.activeInHierarchy == true)
                        {
                            Vector2 newVec = new Vector2(changeObjects[0].transform.position.x, changeObjects[0].transform.position.y - 1);
                            Instantiate(dreamTree, newVec, Quaternion.identity);
                            changeObjects[0].gameObject.SetActive(false);
                        }

                        if (fileCount == 2 && changeObjects[1].gameObject.activeInHierarchy == true)
                        {
                            Vector2 newVec = new Vector2(changeObjects[1].transform.position.x, changeObjects[1].transform.position.y - 1);
                            Instantiate(dreamTree, newVec, Quaternion.identity);
                            changeObjects[1].gameObject.SetActive(false);

                            Vector2 newVec2 = new Vector2(changeObjects[2].transform.position.x, changeObjects[2].transform.position.y);
                            Instantiate(coffin, newVec2, Quaternion.identity);
                            changeObjects[2].gameObject.SetActive(false);
                        }
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
                    if (fileCount == 2)
                    {
                        cabinets[i].AlterSprite();
                    }
                    for (int j = 0; j < inventory.UIItems.Count; j++)
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
        for (int i = 0; i < keys.Count; i++)
        {
            if (keys[i] != null)
            {
                if (keys[i].GetComponent<Key>().CheckOverlap(player) && Input.GetKeyDown(KeyCode.Space))
                {
                    player.HasKey = true;
                    player.AddInventoryItem(keys[0]);
                    keys[i].gameObject.SetActive(false);
                    keys[i] = null;
                }
            }
        }

        for(int i = 0; i < chests.Count; i++)
        {
            if (chests[i].CheckOverlap(player) && Input.GetKeyDown(KeyCode.Space))
            {
                if (chests[i].hasKey)
                {
                    keys.Add(Instantiate(key, keySpawn));
                }
                chests[i].RevealContents();
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
                    pap.GetComponent<SpriteRenderer>().color = new Color((52f / 255f), (105f / 255f), (235f / 255f), 1f);
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

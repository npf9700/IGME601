using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzleTrigger : MonoBehaviour
{
    public Player player;
    public GameObject doorknobController;
    public GameObject doorVar;

    public GameObject talkUI;
    public GameObject Panel;
    public TextAsset textfile;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update() 
    {
        if (CheckOverlap(player))
        {
            Debug.Log("Triggered");
            StartCoroutine(DoorknobPuzzleBegin());
            if (talkUI != null)
            {
                DialogeSystem Dialoge = Panel.GetComponent<DialogeSystem>();
                if (Dialoge != null)
                {
                    Dialoge.SetTextFile(textfile);
                    Canvas myCanvas = talkUI.GetComponent<Canvas>();
                    myCanvas.sortingLayerName = "top";
                    myCanvas.sortingOrder = 100;
                }
            }
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

    IEnumerator DoorknobPuzzleBegin()
    {
        yield return new WaitForSeconds(1f);
        Door doorLock = doorVar.gameObject.GetComponent<Door>();
        doorLock.IsLocked = true;

        doorknobController.gameObject.SetActive(true);
        Destroy(this.gameObject);
    }
}

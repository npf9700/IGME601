using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{

    [SerializeField]
    private Player player;
    public GameObject talkUI;
    public GameObject Panel;
    public TextAsset textfile;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (CheckOverlap(player))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
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
                player.AddInventoryItem(this.gameObject);
                this.gameObject.SetActive(false);
            }
        }
    }

    private bool CheckOverlap(Player p)
    {
        float x = p.transform.position.x;
        float y = p.transform.position.y;
        float width = p.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
        float height = p.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
        if (this.GetComponent<SpriteRenderer>().sprite.bounds.size.x + this.transform.position.x < x - width)
            return false;
        if (this.GetComponent<SpriteRenderer>().sprite.bounds.size.y + this.transform.position.y < y - height)
            return false;
        if (this.transform.position.x - this.GetComponent<SpriteRenderer>().sprite.bounds.size.x > x + width)
            return false;
        if (this.transform.position.y - this.GetComponent<SpriteRenderer>().sprite.bounds.size.y > y + height)
            return false;
        return true;
    }

    

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPuzzleColor : MonoBehaviour
{
    public Sprite closeSprite;
    public Sprite openSprite;
    public bool hasKey;

    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        //sr = this.gameObject.GetComponent<SpriteRenderer>();
        //ChangeColor();
        //ChestOpen();
    }

    public void ChestOpen()
    {
        StartCoroutine(OpenAnimation());
    }

    public void ChangeColor()
    {
        if(hasKey == true)
        {
            sr.color = Color.blue;
        }
    }

    public void RevertColor()
    {
        sr.color = Color.white;
    }

    public IEnumerator OpenAnimation()
    {
        sr.sprite = openSprite;
        yield return new WaitForSeconds(3);
        sr.sprite = closeSprite;
    }

    public void RevealContents()
    {
        sr.sprite = openSprite;
        if (hasKey)
        {
            sr.color = Color.blue;
        }
        else
        {
            sr.color = Color.red;
        }
    }

    public void OpenChest()
    {
        sr.sprite = openSprite;
    }
    public void CloseChest()
    {
        sr.sprite = closeSprite;
    }

    public bool CheckOverlap(Player p)
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

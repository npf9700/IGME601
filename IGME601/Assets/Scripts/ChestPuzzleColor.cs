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
        ChestOpen();
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
}

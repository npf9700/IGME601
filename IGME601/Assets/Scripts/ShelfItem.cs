using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfItem : MonoBehaviour
{
    /// <summary>
    /// Noah Flanders - 10/15/23
    /// ShelfItems will be a major part of the base game mechanics in which the player is supposed to be stocking
    /// shelves. The initial concept is for the player to click and drag these items from a box on the lower part of
    /// the screen to the shelves on the upper part of the screen.
    /// </summary>

    private Vector2 position;
    [SerializeField]
    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)/* && IsMouseOver()*/)
        {
            position = Input.mousePosition;
            position = Camera.main.ScreenToWorldPoint(position);
        }
       
        this.transform.position = position;
    }

    //private bool IsMouseOver()
    //{
    //    if (Input.mousePosition.x > this.transform.position.x + (float)renderer.bounds.x)
    //        return false;
    //    if (Input.mousePosition.y > this.transform.position.y + 2.38f)
    //        return false;
    //    if (Input.mousePosition.x < this.transform.position.x - 2.38f)
    //        return false;
    //    if (Input.mousePosition.y < this.transform.position.y - 2.38f)
    //        return false;
    //    return true;
    //}

}

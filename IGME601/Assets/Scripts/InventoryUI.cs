using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private Canvas invenUI;

    [SerializeField]
    private Player player;

    private List<SpriteRenderer> inventory;

    private List<GameObject> uIItems;

    public List<GameObject> UIItems
    {
        get { return uIItems; }
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = new List<SpriteRenderer>();
        uIItems = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        SetupUI();
    }

    private void SetupUI()
    {
        if(inventory.Count != player.Inventory.Count)
        {
            for(int i = 0; i < uIItems.Count; i++)
            {
                Destroy(uIItems[i].gameObject);
            }
            uIItems.Clear();
            inventory.Clear();
            for(int i = 0; i < player.Inventory.Count; i++)
            {
                inventory.Add(player.Inventory[i].GetComponent<SpriteRenderer>());
                GameObject imageUI = new GameObject("InventoryItem");
                RectTransform trans = imageUI.AddComponent<RectTransform>();
                trans.transform.SetParent(invenUI.transform);
                trans.localScale = Vector3.one;
                trans.anchoredPosition = new Vector2(-650 + (i * 30), -300);
                trans.sizeDelta = new Vector2(20, 25);

                Image image = imageUI.AddComponent<Image>();
                image.sprite = inventory[i].sprite;
                image.color = inventory[i].color;
                imageUI.transform.SetParent(invenUI.transform);
                uIItems.Add(imageUI);
            }
        }
    }
}

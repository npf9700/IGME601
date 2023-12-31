using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogeSystem : MonoBehaviour
{
    // Get UI
    public TextMeshProUGUI textlabel;
    public GameObject talkUI;
    // Get Textfile
    public TextAsset textfile;
    public int index;
    private List<string> textlist = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        GetTextFromFile(textfile);
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && index == textlist.Count)
        {
            Canvas myCanvas = talkUI.GetComponent<Canvas>();
            myCanvas.sortingLayerName = "Default";
            myCanvas.sortingOrder = -10;
            index = 0;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            textlabel.text = textlist[index];
            index++;
        }
    }

    void GetTextFromFile(TextAsset file)
    {
        textlist.Clear();
        textlabel.text=string.Empty;
        index = 0;
        var lineData = file.text.Split('\n');
        foreach (var line in lineData)
        {
            textlist.Add(line);
        }
        textlabel.text = textlist[index];
    }
    public void SetTextFile(TextAsset file)
    {
        textfile = file;
        
        GetTextFromFile(textfile);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPuzzleSequence : MonoBehaviour
{
    public GameObject blackScreen;
    public ChestPuzzleColor chest1;
    public ChestPuzzleColor chest2;
    public ChestPuzzleColor chest3;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Player>().enabled = false;
        StartCoroutine(PuzzleSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PuzzleSequence()
    {
        chest1.GetComponent<ChestPuzzleColor>().hasKey = true;
        chest1.GetComponent<ChestPuzzleColor>().ChangeColor();
        yield return new WaitForSeconds(4);

        blackScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);

        blackScreen.gameObject.SetActive(false);
        chest1.GetComponent<ChestPuzzleColor>().hasKey = false;
        chest1.GetComponent<ChestPuzzleColor>().RevertColor();
        chest3.GetComponent<ChestPuzzleColor>().hasKey = true;
        chest3.GetComponent<ChestPuzzleColor>().ChangeColor();
        yield return new WaitForSeconds(2);

        blackScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);

        blackScreen.gameObject.SetActive(false);
        chest3.GetComponent<ChestPuzzleColor>().hasKey = false;
        chest3.GetComponent<ChestPuzzleColor>().RevertColor();
        chest2.GetComponent<ChestPuzzleColor>().hasKey = true;
        chest2.GetComponent<ChestPuzzleColor>().ChangeColor();
        yield return new WaitForSeconds(2);

        blackScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);

        blackScreen.gameObject.SetActive(false);
        chest2.GetComponent<ChestPuzzleColor>().RevertColor();
        player.GetComponent<Player>().enabled = true;

    }
}

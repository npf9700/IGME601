using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPuzzleSequence : MonoBehaviour
{
    public GameObject blackScreen;
    public ChestPuzzleColor chest1;
    public ChestPuzzleColor chest2;
    public ChestPuzzleColor chest3;

    public float blackScreenTime;
    public float visibleTime;

    public Player player;

    [SerializeField]
    private Key key;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PuzzleSequence()
    {
        yield return new WaitForSeconds(visibleTime);
        blackScreen.gameObject.SetActive(true);
        key.gameObject.SetActive(false);
        Destroy(key.gameObject);
        yield return new WaitForSeconds(blackScreenTime);

        blackScreen.gameObject.SetActive(false);
        chest1.GetComponent<ChestPuzzleColor>().hasKey = true;
        chest1.GetComponent<ChestPuzzleColor>().ChangeColor();
        chest2.OpenChest();
        chest3.OpenChest();
        yield return new WaitForSeconds(visibleTime);

        blackScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(blackScreenTime);

        blackScreen.gameObject.SetActive(false);
        chest1.GetComponent<ChestPuzzleColor>().hasKey = false;
        chest1.GetComponent<ChestPuzzleColor>().RevertColor();
        chest3.GetComponent<ChestPuzzleColor>().hasKey = true;
        chest3.GetComponent<ChestPuzzleColor>().ChangeColor();
        chest2.OpenChest();
        chest3.CloseChest();
        chest1.OpenChest();
        yield return new WaitForSeconds(visibleTime);

        blackScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(blackScreenTime);

        blackScreen.gameObject.SetActive(false);
        chest3.GetComponent<ChestPuzzleColor>().hasKey = false;
        chest3.GetComponent<ChestPuzzleColor>().RevertColor();
        chest2.GetComponent<ChestPuzzleColor>().hasKey = true;
        chest2.GetComponent<ChestPuzzleColor>().ChangeColor();
        chest2.CloseChest();
        chest3.OpenChest();
        chest1.OpenChest();
        yield return new WaitForSeconds(visibleTime);

        blackScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(blackScreenTime);

        blackScreen.gameObject.SetActive(false);
        chest2.GetComponent<ChestPuzzleColor>().RevertColor();
        chest2.CloseChest();
        chest3.CloseChest();
        chest1.CloseChest();
        player.GetComponent<Player>().enabled = true;

    }

    public void BeginPuzzle()
    {
        player.StopVelo();
        player.GetComponent<Player>().enabled = false;
        StartCoroutine(PuzzleSequence());
    }
}

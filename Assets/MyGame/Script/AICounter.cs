using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class AICounter : MonoBehaviour
{
    private GameObject[] vikings;
    private GameObject[] orcs;
    private GameObject[] asians;
    private GameObject[] tunguss;
    private GameObject[] titans;

    public TextMeshProUGUI vikingsCountText;
    public TextMeshProUGUI orcsCountText;
    public TextMeshProUGUI asiansCountText;
    public TextMeshProUGUI tungussCountText;
    public TextMeshProUGUI titansCountText;

    // Update is called once per frame
    void Update()
    {
        vikings = GameObject.FindGameObjectsWithTag("Viking");
        orcs = GameObject.FindGameObjectsWithTag("Orc");
        asians = GameObject.FindGameObjectsWithTag("Asian");
        tunguss = GameObject.FindGameObjectsWithTag("Tungus");
        titans = GameObject.FindGameObjectsWithTag("Titan");

        vikingsCountText.text="Vikings: "+ (vikings.Length-21f).ToString();
        orcsCountText.text="Orcs: "+ (orcs.Length-21f).ToString();
        asiansCountText.text="Asians: "+ (asians.Length-21f).ToString();
        tungussCountText.text="Tunguss: "+ (tunguss.Length - 21f).ToString();
        titansCountText.text="Titans: "+ (titans.Length - 4f).ToString();
    }
}

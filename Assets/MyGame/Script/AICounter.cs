using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class AICounter : BaseManager<AICounter> 
{
    public GameObject[] vikings;
    public GameObject[] orcs;
    public GameObject[] asians;
    public GameObject[] tunguss;
    public GameObject[] titans;

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
        
        vikingsCountText.text="Vikings: "+ vikings.Length.ToString();
        orcsCountText.text="Orcs: "+ orcs.Length.ToString();
        asiansCountText.text="Asians: "+ asians.Length.ToString();
        tungussCountText.text="Tunguss: "+ tunguss.Length.ToString();
        titansCountText.text="Titans: "+ titans.Length.ToString();
    }
}

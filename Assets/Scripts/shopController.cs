using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class shopController : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject[,] shopItems = new GameObject[4,4];
    [SerializeField] GameObject shopObject; //If there is more than one shop we'll need a line to pick the shop the player interacted with
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI selectedDisplay;

    public int[] selected = {0,0};

    private Dictionary<int, string> LETTER_MAP = new Dictionary<int, string>()
        {
            {0, "A"},
            {1, "B"},
            {2, "C"},
            {3, "D"}
        };

    void Start() 
    {
        GameObject[] allItems = GameObject.FindGameObjectsWithTag("ShopItem").OrderBy( go => go.name ).ToArray();
        for (int i = 0; i < 16; i++){
            shopItems[i/4,i % 4] = allItems[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable(){ //using this should allow the same shop ui to be used for every shop
        //populate the shop with the items determined in the shopObject;
    }

    // Make the selected Item green
    public void SelectRow(int button){
        ChangeColour(new Color(255,255,255));
        selected[0] = button;
        ChangeColour(new Color(0,255,0));
        UpdateDisplay();
    }

    public void SelectColumn(int button){
        ChangeColour(new Color(255,255,255));
        selected[1] = button;
        ChangeColour(new Color(0,255,0));
        UpdateDisplay();
    }

    public void OnSubmit() {
        Debug.Log(shopItems[selected[0], selected[1]].name);
        // Call OnInteract in the VendingMachine script
        GetComponent<VendingMachine>().OnInteract();
    }

    private void ChangeColour(Color color){
        shopItems[selected[0],selected[1]].GetComponent<Image>().color = color;
    }

    private void UpdateDisplay(){
        selectedDisplay.text = LETTER_MAP[selected[0]]+(selected[1] + 1).ToString();
    }
}


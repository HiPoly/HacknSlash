using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLogger : MonoBehaviour
{
    //Ask Jacob how to store these inputs to a list efficiently and potentially print them in a practice range with representative icons
    //including how to make bindable keys in control settings
    //and how to tell forward from back when inputting (I can probably work this one out)

    private List<string> InputList = new List<string>() {" "};
    private bool FacingRight;
    private int ListLength;
    [SerializeField] private int Intendedlength = 6;

    //Display Slots
    private string Element1;
    private string Element2;
    private string Element3;
    private string Element4;
    private string Element5;
    private string Element6;

    private void Start()
    {
        FacingRight = true;
    }

    void Update()
    {
        
        LogInputs();
        UpdateValues();
        //UpdateUI();

        //need something to remove expired or excess inputs aswell



        //check if list length is greater than x then reduce to correct size from back end.
        ListLength = InputList.Count;
        if (ListLength >= Intendedlength) 
        {
            InputList.RemoveAt(Intendedlength - 1);
        }
        //check if input has been listed for a long period of time and remove expired values.
    }

    void LogInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            InputList.Insert(0, "Dodge");
        }
        if (Input.GetMouseButtonDown(0)) {
            InputList.Insert(0, "Attack");
        }
        if (Input.GetMouseButtonDown(1)) {
            InputList.Insert(0, "Block");
        }
        if (Input.GetMouseButtonDown(2)) {
            InputList.Insert(0, "Taunt");
        }
        if (Input.GetAxisRaw(Axis.horizontalaxis) < 0) {
            if (FacingRight){
                InputList.Insert(0, "Back");
            }
            else{
                InputList.Insert(0, "Forward");
            }
        }
        if (Input.GetAxisRaw(Axis.horizontalaxis) > 0) {
            if (FacingRight){
                InputList.Insert(0, "Forward");
            }
            else{
                InputList.Insert(0, "Back");
            }
        }
        if (Input.GetAxisRaw(Axis.verticalaxis) < 0){
            InputList.Insert(0, "Down");
        }
        if (Input.GetAxisRaw(Axis.verticalaxis) > 0){
            InputList.Insert(0, "Up");
        }
    }
    void UpdateValues()
    //
    {
        
        Element1 = InputList[0];
        //If InputList[0] = "Up"{
        //Set 'image1' to UpArrow.png

        foreach (string element in InputList) ;
        {

        }
    }

    //UpdateUI()
        //Send the Elements to TextBoxes within unity
    
}

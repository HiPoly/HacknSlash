using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLogger : MonoBehaviour
{
    //Ask Jacob how to store these inputs to a list efficiently and potentially print them in a practice range with representative icons
    //including how to make bindable keys in control settings
    //and how to tell forward from back when inputting (I can probably work this one out)

    private List<string> InputList = new List<string>()
    {" "};
    private bool FacingRight;

    private void Start()
    {
        FacingRight = true;
        
        InputList = new List<string>()
        {" "};
    }

    void Update()
    {
        //need something to remove expired or excess inputs aswell

        if (Input.GetKeyDown(KeyCode.Space)){
            InputList.Insert(0, "Dodge");
        }
        if (Input.GetMouseButtonDown(0)){
            InputList.Insert(0, "Attack");
        }
        if (Input.GetMouseButtonDown(1)){
            InputList.Insert(0, "Block");
        }
        if (Input.GetMouseButtonDown(2)){
            InputList.Insert(0, "Taunt");
        }
        if (Input.GetAxisRaw(Axis.horizontalaxis) < 0)
        {
            if (FacingRight) {
                InputList.Insert(0, "Back");}
            else{
                InputList.Insert(0, "Forward");}
        }
        if (Input.GetAxisRaw(Axis.horizontalaxis) > 0)
        {
            if (FacingRight) {
                InputList.Insert(0, "Forward");}
            else{
                InputList.Insert(0, "Back");}
        }
        if (Input.GetAxisRaw(Axis.verticalaxis) < 0){
            InputList.Insert(0, "Down");
        }
        if (Input.GetAxisRaw(Axis.verticalaxis) > 0){
            InputList.Insert(0, "Up");
        }
    }
}

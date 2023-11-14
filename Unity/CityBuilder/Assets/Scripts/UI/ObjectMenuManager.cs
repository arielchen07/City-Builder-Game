using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMenuManager : MonoBehaviour
{
    public PlacementSystem ps;
    public Text objectName;
    public Text population;
    public Text power;
    public Text water;
    public Text sewage;
    public Text internet;
    public Text naturalGas;
    public GameObject currentlySelecting;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void UpdateInfo(GameObject selectedObject){
        if(selectedObject.TryGetComponent<House>(out var h)){
            objectName.text = h.objectName;
            population.text = h.GetPopulation();
            power.text = h.GetPower();
            water.text = h.GetWater();
            sewage.text = h.GetSewage();
            internet.text = h.GetInternet();
            naturalGas.text = h.GetGas();
        }
    }

    public void Move(){
        ps.MoveObject();
    }

    public void Delete(){
        //todo
    }

    public void RotateLeft(){
        ps.RotateObject(true);
    }

    public void RotateRight(){
        ps.RotateObject(false);
    }
}
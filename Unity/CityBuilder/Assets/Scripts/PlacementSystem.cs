using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementSystem : MonoBehaviour
{
    public int testInt = 10;
    [SerializeField] private GameObject pointer;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject[] placeableObjects;
    public GameObject road;
    public GameObject currentlyPlacing;
    public GameObject currentlySelecting;
    public GameObject currentlyHovering;
    public GameObject objectMenu;
    public CameraController cameraController;
    public MenuManager menuManager;
    Vector3 currentRotation;
    Vector3 oldPosition;
    Vector3 oldRotation;
    bool beginPlacingContinuousObjects = false;
    public bool isSelectingObject = false;
    void Start()
    {
        currentRotation = new Vector3(0,0,0);
    }
    void Update()
    {
        //update pointer position
        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        pointer.transform.position = mousePos;
        //get object the cursor is currently colliding with
        if(inputManager.hitObject != null){
            currentlyHovering = inputManager.hitObject;
        }
        //checks for key inputs to spawn objects
        SpawnObjectOnKey();


        if(!EventSystem.current.IsPointerOverGameObject()){
            if (beginPlacingContinuousObjects) {
                PlaceContinuousObjects(road);
            }

            if(currentlyPlacing != null && !beginPlacingContinuousObjects){
                if(Input.GetKeyDown(KeyCode.Mouse0)){
                    PlaceObject();
                }
                else if(Input.GetKeyDown(KeyCode.Escape)){
                    DropObject();
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow)){
                    RotateObject(true);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow)){
                    RotateObject(false);
                }
                else if (Input.GetKeyDown(KeyCode.Delete)) {
                    Destroy(currentlyPlacing);
                    currentlyPlacing = null;
                }
            } else if (currentlyHovering != null) {
                if (Input.GetKeyDown(KeyCode.Mouse0)) {
                    if (currentlyHovering.CompareTag("Object")) {
                        SelectObject();
                    } else if (currentlySelecting != null){
                        DeselectObject();
                    }
                }
            }

        }

        if(isSelectingObject) {
            inputManager.placementLayermask = LayerMask.GetMask("Ground");
        } else {
            inputManager.placementLayermask = LayerMask.GetMask("Ground") | LayerMask.GetMask("Foreground");
        }
    }
    /*
    For manual testing
    */
    void SpawnObjectOnKey() {
        if(!isSelectingObject){
            if(Input.GetKeyDown(KeyCode.Alpha1)){
                HoverObject(placeableObjects[0]);
            }
            if(Input.GetKeyDown(KeyCode.Alpha2)){
                HoverObject(placeableObjects[1]);
            }
            if(Input.GetKeyDown(KeyCode.Alpha3)){
                HoverObject(placeableObjects[2]);
            }
            if(Input.GetKeyDown(KeyCode.Alpha4)){
                HoverObject(placeableObjects[3]);
            }
            if(Input.GetKeyDown(KeyCode.R)){ //place roads
                beginPlacingContinuousObjects = true;
                if(currentlyPlacing != null) {
                    DropObject();
                }
            }
        }
    }
    public void RotateObject(bool rotateLeft){
        float yRot = -90f;
        if(rotateLeft){
            yRot = 90f;
        }
        currentlySelecting.transform.Rotate(new Vector3(0,yRot,0));
        currentRotation = currentlySelecting.transform.rotation.eulerAngles;
    }
    /*
    PlaceObject is called when the user is currently selecting an object and wants to place it.
    */
    public void PlaceObject(){
        if (currentlyPlacing.GetComponent<PlaceableObject>().CanBePlaced()){
            currentlyPlacing.transform.parent = null;
            foreach(GameObject tile in currentlyPlacing.GetComponent<PlaceableObject>().currentlyColliding){
                tile.GetComponent<MapTile>().isOccupied = true;
                tile.GetComponent<MapTile>().placedObject = currentlyPlacing;
            }
            inputManager.placementLayermask = LayerMask.GetMask("Ground") | LayerMask.GetMask("Foreground");
            currentlyPlacing.GetComponent<PlaceableObject>().isHovering = false;
            currentlyPlacing.GetComponent<PlaceableObject>().hasBeenPlaced = true;
            currentlyPlacing.GetComponent<PlaceableObject>().OnPlace();
            currentlyPlacing = null;
        }
        isSelectingObject = false;
    }

    /*
    HoverObject is called when the user creates a new building and needs to place it.
    */
    public void HoverObject(GameObject objectToPlace){
        inputManager.placementLayermask = LayerMask.GetMask("Ground");
        isSelectingObject = true;
        if(currentlyPlacing != null){
            Destroy(currentlyPlacing);
            currentlyPlacing = null;
        }
        GameObject newBuilding = Instantiate(objectToPlace, pointer.GetComponent<PointerDetector>().indicator.transform);
        currentlyPlacing = newBuilding;
        currentlyPlacing.transform.Rotate(currentRotation);
        AssignObjectToCursor();
    }

    /*
    DropObject is called when the user selects an object and presses escape. Ie the player wants to move an object but decides
    to not move it.
    */
    public void DropObject() {
        if(currentlyPlacing.GetComponent<PlaceableObject>().hasBeenPlaced == true) {
            currentlyPlacing.transform.SetPositionAndRotation(oldPosition, Quaternion.Euler(oldRotation));
            currentlyPlacing.GetComponent<PlaceableObject>().isHovering = false;
            currentlyPlacing.transform.parent = null;
        } else {
            Destroy(currentlyPlacing);
        }
        currentlyPlacing = null;
        isSelectingObject = false;
    }

    void AssignObjectToCursor(){
        pointer.GetComponent<PointerDetector>().currentlyPlacing = currentlyPlacing;
        pointer.GetComponent<PointerDetector>().AlignObject();
        currentlyPlacing.GetComponent<PlaceableObject>().isHovering = true;
    }
    /*
    SelectObject is called when a user hovers their cursor over an object and wants to select it.
    */
    public void SelectObject() {        
        isSelectingObject = true;
        currentlySelecting = currentlyHovering;
        ToggleObjectMenu();
        cameraController.ZoomToItem(currentlySelecting.transform.position);
        menuManager.CloseInventory();
        objectMenu.GetComponent<ObjectMenuManager>().UpdateInfo(currentlySelecting);
    }
    public void ToggleObjectMenu(){
        Animator objectMenuAnim = objectMenu.GetComponent<Animator>();
        objectMenuAnim.SetBool("isOpen", isSelectingObject);
        objectMenuAnim.SetTrigger("toggle");
    }

    public void DeselectObject(){
        isSelectingObject = false;
        currentlySelecting = null;
        ToggleObjectMenu();
        cameraController.isLocked = false;
        menuManager.OpenInventory();
    }

    public void MoveObject(){
        currentlyPlacing = currentlySelecting;
        isSelectingObject = false;
        ToggleObjectMenu();
        inputManager.placementLayermask = LayerMask.GetMask("Ground");
        oldPosition = currentlyPlacing.transform.position;
        oldRotation = currentlyPlacing.transform.rotation.eulerAngles;
        foreach(GameObject tile in currentlyPlacing.GetComponent<PlaceableObject>().currentlyColliding){
            tile.GetComponent<MapTile>().isOccupied = false;
            tile.GetComponent<MapTile>().placedObject = null;
        }
        currentlyPlacing.transform.parent = pointer.GetComponent<PointerDetector>().indicator.transform;
        currentlyPlacing.transform.localPosition = new Vector3(0,0,0);
        AssignObjectToCursor();
    }

    void PlaceContinuousObjects(GameObject objectToPlace){
        GameObject pointerIndicator = pointer.GetComponent<PointerDetector>().indicator;
        if (Input.GetKey(KeyCode.Mouse0)){
            Collider[] cols = Physics.OverlapSphere(pointerIndicator.transform.position, 0.1f, LayerMask.GetMask("Foreground"));
            if(cols.Length == 0){
                Instantiate(objectToPlace, pointerIndicator.transform.position, Quaternion.identity);
                return;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0)) {
            beginPlacingContinuousObjects = false;
        }
    }

    public GameObject GetCurrentlyPlacing()
    {
        return this.currentlyPlacing;
    }

    public GameObject GetCurrentlySelecting()
    {
        return this.currentlySelecting;
    }
}

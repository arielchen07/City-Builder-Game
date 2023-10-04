using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public List<GameObject> currentlyColliding;
    public GameObject HoverValid;
    public GameObject HoverInvalid;
    public Vector3 worldCenter;
    public Vector3 worldHalfExtents;
    public Vector2 dimensions;
    public bool canBePlaced;
    public bool isHovering;
    public bool evenDimensions;
    public bool hasBeenPlaced = false;
    
    void Start()
    {
        currentlyColliding = new List<GameObject>();
        HoverValid.SetActive(false);
        HoverInvalid.SetActive(false);
    }


    void Update()
    {
        currentlyColliding = GetCollidingTiles();
        canBePlaced = CanBePlaced();
        if (isHovering) {
            if (canBePlaced) {
                HoverValid.SetActive(true);
                HoverInvalid.SetActive(false);
            } else {
                HoverValid.SetActive(false);
                HoverInvalid.SetActive(true);
            }
        } else {
            HoverValid.SetActive(false);
            HoverInvalid.SetActive(false);
        }
    }

    public bool CanBePlaced(){
        if(currentlyColliding.Count < dimensions.x * dimensions.y){
            return false;
        }
        foreach(GameObject collidingObject in currentlyColliding){
            MapTile tile = collidingObject.GetComponent<MapTile>();
            if (tile != null) {
                if (tile.isOccupied){
                    return false;
                }
            }
        }
        return true;
    }

    public List<GameObject> GetCollidingTiles(){
        currentlyColliding.Clear();
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector3 worldCenter = collider.transform.TransformPoint(collider.center);
        Vector3 worldHalfExtents = collider.transform.TransformVector(collider.size * 0.5f);
        Collider[] cols = Physics.OverlapBox(worldCenter, worldHalfExtents, collider.transform.rotation);
        foreach(Collider col in cols){
            if(col.CompareTag("Tile"))
            {
                currentlyColliding.Add(col.gameObject);
            }   
        }
        return currentlyColliding;
    }
    public virtual void OnPlace() {

    }
}

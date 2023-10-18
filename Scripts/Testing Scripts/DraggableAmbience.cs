using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableAmbience : MonoBehaviour
{ 

 
    
    //DON'T FORGET TO ACTIVATE COLLIDER FOR OBJECT!
    //represents difference in position between mouse and object
/*     private Vector2 difference = Vector2.zero;  
 */    


//OnMouseDown and OnMouseDrag concern with movement of Dot
//OnMouseDown the mouse is dragged, the difference in distance with the Dot is established
//OnMouseDrag the position of the Dot is refreshed by subtracting the previously established difference 

    //DETECTION
    //Finding current difference in distance between mouse and dot
    /* private void OnMouseDown(){
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        } */

        //EXECUTION
//Updating the Dot position to compensate difference with mouse position
/*     private void OnMouseDrag(){
 */    
    
   // float posX = PlayerPrefs.GetFloat ("X");
   // float posY = PlayerPrefs.GetFloat ("Y");
   // float posZ = PlayerPrefs.GetFloat ("Z");

    //A mouse cursor that is beyond the confines of the square should not perform a dragging operation on the dot
    //if the mouse cursor is within the confines of the square
    //(aka if the value of X and Y are more than their minimum and less than their maximum)


    //then perform the Dot position rectification
    /* transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    transform.localPosition = new Vector3(
                                        Mathf.Clamp(transform.localPosition.x, -0.5f, 0.5f), 
                                        Mathf.Clamp(transform.localPosition.y, -0.5f, 0.5f),
                                        -0.01f
                                        );
 */
 //otherwise do nothing
    
/* } */





}

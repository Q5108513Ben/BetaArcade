using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour {

    //public GameObject playerCore;
    [Tooltip("Attach the main camera that will be following the player around.")]
    public CameraController current_camera;
    [Tooltip("Attach the room that the player will be spawning in.")]
    public RoomBoundary current_room;

    bool initialUpdate = false;
	
	// Update is called once per frame
	void Update () {

        // Had to stick this here instead of in the start function because Unity
        // likes to call Start() in random orders leading to 'current_room' being
        // uninitialised sometimes.
        if (!initialUpdate) {
            current_camera.UpdateRoomBoundary(current_room);
            initialUpdate = true;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 10.0f;
        
        //if(this.GetComponentInChildren<Collider>().)
        var y = Input.GetAxis("Vertical") * Time.deltaTime * 2.5f;

        //this.GetComponentInChildren<SphereCollider>().

        transform.Translate(x, y, 0);

        #region Room Boundary Checks

        if (transform.position.x < current_room.boundary_left)
        {

            foreach (var room in current_room.rooms_to_left)
            {

                if (transform.position.y > room.boundary_bottom)
                {

                    current_room = room;
                    current_camera.UpdateRoomBoundary(room);
                    break;

                }
            }

        }

        else if (transform.position.x > current_room.boundary_right)
        {

            foreach (var room in current_room.rooms_to_right)
            {

                if (transform.position.y > room.boundary_bottom)
                {

                    current_room = room;
                    current_camera.UpdateRoomBoundary(room);
                    break;

                }
            }

        }

        else if (transform.position.y < current_room.boundary_bottom)
        {

            foreach (var room in current_room.rooms_below)
            {

                if (transform.position.x > room.boundary_left)
                {

                    current_room = room;
                    current_camera.UpdateRoomBoundary(room);
                    break;

                }
            }

        }

        else if (transform.position.y > current_room.boundary_top)
        {

            foreach (var room in current_room.rooms_above)
            {

                if (transform.position.x > room.boundary_left)
                {

                    current_room = room;
                    current_camera.UpdateRoomBoundary(room);
                    break;

                }
            }

        }

        #endregion

    }
}

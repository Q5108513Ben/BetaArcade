using UnityEngine;

public class PlayerController : MonoBehaviour {

    public CameraController current_camera;
    public RoomBoundary current_room;

    void Start() {

        current_camera.UpdateRoomBoundry(current_room);

    }

	void Update() {

        //-----------------------------------Player-Controls----------------------------------\\

        if (Input.GetButton("Move - Right")) {

            var xVelocity = 10.0f * Time.deltaTime;

            transform.Translate(xVelocity, 0, 0);

        }

        if (Input.GetButton("Move - Left")) {

            var xVelocity = -10.0f * Time.deltaTime;

            transform.Translate(xVelocity, 0, 0);

        }

        if (Input.GetButton("Move - Up")) {

            var yVelocity = 10.0f * Time.deltaTime;

            transform.Translate(0, yVelocity, 0);

        }

        if (Input.GetButton("Move - Down"))
        {
            var yVelocity = -10.0f * Time.deltaTime;
            transform.Translate(0, yVelocity, 0);
        }

        //---------------------------------Room-Boundary-Check---------------------------------\\

        if (transform.position.x < current_room.boundary_left) {

            current_room = current_room.room_to_left;
            current_camera.UpdateRoomBoundry(current_room);

        } 

        else if (transform.position.x > current_room.boundary_right) {

            current_room = current_room.room_to_right;
            current_camera.UpdateRoomBoundry(current_room);

        }

        else if (transform.position.y < current_room.boundary_bottom) {

            current_room = current_room.room_below;
            current_camera.UpdateRoomBoundry(current_room);

        }

        else if (transform.position.y > current_room.boundary_top) {

            current_room = current_room.room_above;
            current_camera.UpdateRoomBoundry(current_room);

        }

    }

}
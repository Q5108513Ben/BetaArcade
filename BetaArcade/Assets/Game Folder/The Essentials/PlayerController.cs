using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Tooltip("Attach the main camera that will be following the player around.")]
    public CameraController current_camera;
    [Tooltip("Attach the room that the player will be spawning in.")]
    public RoomBoundary current_room;

    void Start() {

        current_camera.UpdateRoomBoundary(current_room);

    }

	void Update() {

        #region Player Controls

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

        #endregion

        #region Room Boundary Checks

        if (transform.position.x < current_room.boundary_left) {

            current_room = current_room.room_to_left;
            current_camera.UpdateRoomBoundary(current_room);

        } 

        else if (transform.position.x > current_room.boundary_right) {

            current_room = current_room.room_to_right;
            current_camera.UpdateRoomBoundary(current_room);

        }

        else if (transform.position.y < current_room.boundary_bottom) {

            current_room = current_room.room_below;
            current_camera.UpdateRoomBoundary(current_room);

        }

        else if (transform.position.y > current_room.boundary_top) {

            current_room = current_room.room_above;
            current_camera.UpdateRoomBoundary(current_room);

        }

        #endregion

    }

}
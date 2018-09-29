using UnityEngine;

public class CameraController : MonoBehaviour {

    [Tooltip("The target that the camera will be following.")]
    public Transform target;

    [Tooltip("The distance between the camera and the target. This is a static value.")]
    public Vector3 camera_offset;

    [Tooltip("How far behind the camera lags when the player moves around.")]
    public float camera_lag;

    private Vector3 clamped_position;
    private Vector3 camera_velocity = Vector3.zero;

    public bool xMaxReached = false;
    public bool xMinReached = false;
    public bool yMaxReached = false;
    public bool yMinReached = false;

    public float xMaxValue = 0;
    public float xMinValue = 0;
    public float yMaxValue = 0;
    public float yMinValue = 0;

    // If the camera's dimensions reach the max / min value we need to prevent it from
    // moving any further in that direction. The max / min values will be retrieved 
    // from the current room the player is in, for now they are hardcoded for testing.

    void Update() {

        clamped_position = target.position;

        if (xMaxReached && xMinReached) {

            clamped_position.x = Mathf.Clamp(target.position.x, xMinValue, xMaxValue);

        }

        else if (xMaxReached) {

            clamped_position.x = Mathf.Clamp(target.position.x, target.position.x, xMaxValue);

        }

        else if (xMinReached) {

            clamped_position.x = Mathf.Clamp(target.position.x, xMinValue, target.position.x);

        }

        if (yMaxReached && yMinReached) {

            clamped_position.y = Mathf.Clamp(target.position.y, yMinValue, yMaxValue);

        }

        if (yMaxReached) {

            clamped_position.y = Mathf.Clamp(target.position.y, target.position.y, yMaxValue);

        }

        if (yMinReached) {

            clamped_position.y = Mathf.Clamp(target.position.y, yMinValue, target.position.y);

        }

    }

	void LateUpdate() {

        transform.position = Vector3.SmoothDamp(transform.position, clamped_position + camera_offset, ref camera_velocity, camera_lag);

    }

    void UpdateRoomBoundry() {

        // This function would be called whenever the player enters a new room, we would
        // just retrieve the new room's boundaries and copy them into our max / min values.

    }
}

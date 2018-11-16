using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraController : MonoBehaviour {

    [Tooltip("The target that the camera will be following.")]
    public Transform target;

    [Tooltip("The distance between the camera and the target. This is a static value.")]
    public Vector3 camera_offset;

    [Tooltip("How far behind the camera lags when the player moves around.")]
    public float camera_lag;

    private Vector3 clamped_position;
    private Vector3 camera_velocity = Vector3.zero;

    [System.NonSerialized]
    public float xMaxValue = 0;
    [System.NonSerialized]
    public float xMinValue = 0;
    [System.NonSerialized]
    public float yMaxValue = 0;
    [System.NonSerialized]
    public float yMinValue = 0;

    // If the camera's dimensions reach the max / min value we need to prevent it from
    // moving any further in that direction.

    [Tooltip("The image we are fading in and out to. Recommended to just keep this as a black image.")]
    public Image fade_image;
    [Tooltip("The speed it takes for the image to change from 0 to 1 opactity. (0 being transparent)")]
    public float fade_in_speed = 0;
    [Tooltip("The speed it takes for the image to change from 1 to 0 opactity. (0 being transparent)")]
    public float fade_out_speed = 0;
    [Tooltip("This value determines how long the image will linger on 1 opacity in seconds.")]
    public float fade_blackout_time = 0;

    private bool isBlack = true;
    private bool isTransitioning = false;

    // Temporary variables for Max's camera easing.
    public float maxSpeed;
    public bool maxCameraEnabled = false;

    private void Start() {

        // For some reason the first time the coroutine is called it has a delayed effect.
        // We call it here on start to get the 'delayed' initial call out of the way so that
        // when the player transitions a room for the first time it works as intended.
        StartCoroutine(Fade()); 

    }

    void Update() {

        clamped_position = target.position;

        clamped_position.x = Mathf.Clamp(target.position.x, xMinValue, xMaxValue);
        clamped_position.y = Mathf.Clamp(target.position.y, yMinValue, yMaxValue);
        
    }

	void LateUpdate() {

        if (!isTransitioning) {

            if (!maxCameraEnabled) {
                transform.position = Vector3.SmoothDamp(transform.position, clamped_position + camera_offset, ref camera_velocity, camera_lag);
            }
            else {
                transform.position = transform.position - camera_offset;
                transform.position += (clamped_position - transform.position) * maxSpeed;
                transform.position = transform.position + camera_offset;
            }
        }
        
    }

    public void UpdateRoomBoundary(RoomBoundary room) {

        isTransitioning = true;

        StartCoroutine(Fade());

        Camera camera = GetComponent<Camera>();

        float camera_height = camera.orthographicSize * 2.0f;
        float camera_width = camera_height * camera.aspect;

        xMaxValue = room.boundary_right - (camera_width / 2);
        xMinValue = room.boundary_left + (camera_width / 2);
        yMaxValue = room.boundary_top - (camera_height / 2);
        yMinValue = room.boundary_bottom + (camera_height / 2);

    }

    IEnumerator Fade() {

        while (!isBlack) {

            var temp_colour = fade_image.color;
            temp_colour.a += fade_in_speed * Time.deltaTime;
            fade_image.color = temp_colour;

            if (temp_colour.a >= 1) {

                isBlack = true;

                isTransitioning = false;

                yield return new WaitForSeconds(fade_blackout_time);
                
            }

            yield return null;

        }

        while (isBlack) {

            var temp_colour = fade_image.color;
            temp_colour.a -= fade_out_speed * Time.deltaTime;
            fade_image.color = temp_colour;

            if (temp_colour.a <= 0) {

                isBlack = false;

                isTransitioning = false;

            }

            yield return null;

        }

    }

}
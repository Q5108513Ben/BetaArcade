using UnityEngine;


public class PlayerMovement : MonoBehaviour {

    //public GameObject playerCore;
    [Tooltip("Attach the main camera that will be following the player around.")]
    public CameraController current_camera;
    [Tooltip("Attach the room that the player will be spawning in.")]
    public RoomBoundary current_room;

    [Tooltip("Amount of multiplicitive jump height of the player.")]
    [Range(1, 10)]
    public float jumpVelocity = 5;

    public float fallMultiplier = 2f;
    public float lowJumpMultiplier = 2f;

    [Tooltip("Amount of multiplicitive speed of the player.")]
    [Range(1, 100)]
    public float speedVelocity = 10;


    private Rigidbody rb;

    public bool jumpHeld = false;
    public bool isJumping = false;
    public bool isFalling = false;

    bool initialUpdate = false;

    public BotCounterWidget botCounter;
    public Vector3 respawnLocation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {

        // Had to stick this here instead of in the start function because Unity
        // likes to call Start() in random orders leading to 'current_room' being
        // uninitialised sometimes.
        if (!initialUpdate)
        {
            current_camera.UpdateRoomBoundary(current_room);
            initialUpdate = true;
        }

        // Horizontal movement
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * speedVelocity;

        transform.Translate(x, 0, 0);

        // Vertical movement
        // Check for player input
        if (Input.GetButton("Jump") && !isJumping && !isFalling)
        {
            rb.velocity = Vector3.up * jumpVelocity;
            isJumping = true;
            jumpHeld = true;
        }
        else if (Input.GetButtonUp("Jump") && isJumping)
        {
            //isJumping = false;
            jumpHeld = false;
        }

        // Check if the player is falling
        if (rb.velocity.y <= -0.3)
        {
            isJumping = false;
            isFalling = true;
        }
        else if (rb.velocity.y >= -0.3)
        {
            isFalling = false;
        }

        // Adjust velocity based on previous info
        if (rb.velocity.y > 0 && !jumpHeld)
        { 
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        else if (isFalling)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        

        #region Room Boundary Checks

        if (transform.position.x < current_room.boundary_left)
        {

            RoomBoundary tempCurrentRoom = current_room;

            foreach (var room in current_room.rooms_to_left)
            {

                if (transform.position.y > room.boundary_bottom)
                {

                    current_room = room;
                    current_camera.UpdateRoomBoundary(room);
                    break;

                }
            }

            if (current_room == tempCurrentRoom) {
                Respawn();
            }

        }

        else if (transform.position.x > current_room.boundary_right)
        {

            RoomBoundary tempCurrentRoom = current_room;

            foreach (var room in current_room.rooms_to_right)
            {

                if (transform.position.y > room.boundary_bottom)
                {

                    current_room = room;
                    current_camera.UpdateRoomBoundary(room);
                    break;

                }
            }

            if (current_room == tempCurrentRoom) {
                Respawn();
            }

        }

        else if (transform.position.y < current_room.boundary_bottom)
        {

            RoomBoundary tempCurrentRoom = current_room;

            foreach (var room in current_room.rooms_below)
            {

                if (transform.position.x > room.boundary_left)
                {

                    current_room = room;
                    current_camera.UpdateRoomBoundary(room);
                    break;

                }
            }

            if (current_room == tempCurrentRoom) {
                Respawn();
            }

        }

        else if (transform.position.y > current_room.boundary_top)
        {

            RoomBoundary tempCurrentRoom = current_room;

            foreach (var room in current_room.rooms_above)
            {

                if (transform.position.x > room.boundary_left)
                {

                    current_room = room;
                    current_camera.UpdateRoomBoundary(room);
                    break;

                }
            }

            if (current_room == tempCurrentRoom) {
                Respawn();
            }

        }

        #endregion

    }

    private void Respawn() {

        transform.position = respawnLocation;

        foreach (var bot in GameObject.FindGameObjectsWithTag("Bot")) {

            Destroy(bot);

        }

        if (botCounter != null) {

            botCounter.EmptyCounter();

        }

    }

}

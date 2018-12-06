﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour {

    //public GameObject playerCore;
    [Tooltip("Attach the main camera that will be following the player around.")]
    public CameraController current_camera;
    [Tooltip("Attach the room that the player will be spawning in.")]
    public RoomBoundary current_room;

    [Tooltip("Amount of multiplicitive jump height of the player.")]
    [Range(1, 10)]
    public float jumpVelocity = 5;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Tooltip("Amount of multiplicitive speed of the player.")]
    [Range(1, 100)]
    public float speedVelocity = 10;


    private Rigidbody rb;

    private bool isJumping = false;

    bool initialUpdate = false;

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


        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            rb.velocity = Vector3.up * jumpVelocity;
            isJumping = true;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * speedVelocity;

        transform.Translate(x, 0, 0);
        //rb.velocity += Vector3.right * x;

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y == 0)
        {
            isJumping = false;
        }
        
        



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

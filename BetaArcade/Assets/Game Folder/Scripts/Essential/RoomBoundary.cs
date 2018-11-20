using UnityEngine;

public class RoomBoundary : MonoBehaviour {

    [Tooltip("Links the given room as being to the left of the selected room. This is required to transition from room to room.")]
    public RoomBoundary[] rooms_to_left;
    [Tooltip("Links the given room as being to the right of the selected room. This is required to transition from room to room.")]
    public RoomBoundary[] rooms_to_right;
    [Tooltip("Links the given room as being above the selected room. This is required to transition from room to room.")]
    public RoomBoundary[] rooms_above;
    [Tooltip("Links the given room as being below the selected room. This is required to transition from room to room.")]
    public RoomBoundary[] rooms_below;

    [System.NonSerialized]
    public float boundary_left;
    [System.NonSerialized]
    public float boundary_right;
    [System.NonSerialized]
    public float boundary_top;
    [System.NonSerialized]
    public float boundary_bottom;

    void Start() {

        BoxCollider boundary_local = GetComponent<BoxCollider>();
        Vector3 boundary_world = transform.position + boundary_local.center;

        boundary_left = boundary_world.x - boundary_local.size.x / 2;
        boundary_right = boundary_world.x + boundary_local.size.x / 2;

        boundary_top = boundary_world.y + boundary_local.size.y / 2;
        boundary_bottom = boundary_world.y - boundary_local.size.y / 2;

    }

}
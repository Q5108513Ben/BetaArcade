using UnityEngine;

public class RoomBoundary : MonoBehaviour {

    public RoomBoundary room_to_left;
    public RoomBoundary room_to_right;
    public RoomBoundary room_above;
    public RoomBoundary room_below;

    public float boundary_left;
    public float boundary_right;
    public float boundary_top;
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
using Unity.Netcode;
using UnityEngine;

public class PlayerCam : NetworkBehaviour
{
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] private Transform orientation;

    private float pitch;
    private float yaw;

    [SerializeField] private Camera cam;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            cam.enabled = false;
        }

    }

    public void HandleCameraRotation()
    {
        Vector2 mouseDelta = GameInput.Instance.GetMouseDelta();

        yaw += mouseDelta.x * sensX;

        pitch -= mouseDelta.y * sensY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);

        orientation.rotation = Quaternion.Euler(0f, yaw, 0f);

    }
}

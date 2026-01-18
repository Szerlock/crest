using Unity.Netcode;
using UnityEngine;

public class MoveCamera : NetworkBehaviour
{
    [SerializeField] private Transform cameraPosition;

    void Update()
    {
        transform.position = cameraPosition.position;
    }
}

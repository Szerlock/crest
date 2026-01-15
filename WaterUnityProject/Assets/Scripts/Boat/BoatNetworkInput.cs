using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatNetworkInput : NetworkBehaviour
{
    BoatMovement _motor;
    Rigidbody _rb;

    void Awake()
    {
        _motor = GetComponent<BoatMovement>();
        _rb = GetComponent<Rigidbody>();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            enabled = false;
        }
    }

    void Update()
    {
        if (!IsOwner) return;

        float throttle = 0f;
        float steer = 0f;

#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null)
        {
            throttle =
                (Keyboard.current.wKey.isPressed ? 1f : 0f) +
                (Keyboard.current.sKey.isPressed ? -1f : 0f);

            steer =
                (Keyboard.current.dKey.isPressed ? 1f : 0f) +
                (Keyboard.current.aKey.isPressed ? -1f : 0f);
        }
#else
        throttle = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");
#endif

        Debug.Log($"[BoatNetworkInput] Throttle: {throttle}, Steer: {steer}");

        _motor.SetInput(throttle, steer);

        if (_rb != null)
        {
            Debug.Log($"[BoatNetworkInput] Rigidbody velocity: {_rb.linearVelocity}");
        }
    }
}

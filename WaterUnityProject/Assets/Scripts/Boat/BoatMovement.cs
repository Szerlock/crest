using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public float enginePower = 11f;
    public float turnPower = 1.3f;
    public float forceHeightOffset = -0.3f;

    Rigidbody _rb;
    float _throttle;
    float _steer;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void SetInput(float throttle, float steer)
    {
        _throttle = Mathf.Clamp(throttle, -1f, 1f);
        _steer = Mathf.Clamp(steer, -1f, 1f);
    }

    void FixedUpdate()
    {
        if (_rb.isKinematic) return;

        var forcePos = _rb.worldCenterOfMass + forceHeightOffset * Vector3.up;

        _rb.AddForceAtPosition(
            transform.forward * enginePower * _throttle,
            forcePos,
            ForceMode.Acceleration
        );

        _rb.AddTorque(
            transform.up * turnPower * _steer,
            ForceMode.Acceleration
        );
    }
}

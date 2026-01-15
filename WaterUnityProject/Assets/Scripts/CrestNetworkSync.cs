using Crest;
using System;
using Unity.Netcode;
using UnityEngine;

public class CrestNetworkSync : NetworkBehaviour
{
    [Header("References")]
    [Tooltip("Reference to the OceanRenderer in the scene.")]
    public OceanRenderer oceanRenderer;

    [Header("Wind Settings")]
    public float defaultWindSpeed = 10f;
    public float defaultWindDirection = 0f;

    // Networked variables for wind
    private NetworkVariable<float> netWindSpeed = new NetworkVariable<float>();
    private NetworkVariable<float> netWindDirection = new NetworkVariable<float>();

    void Start()
    {
        if (oceanRenderer == null)
        {
            Debug.LogError("[CrestNetworkSync] OceanRenderer reference is missing!");
            enabled = false;
            return;
        }

        // Only server sets initial values
        if (IsServer)
        {
            netWindSpeed.Value = defaultWindSpeed;
            netWindDirection.Value = defaultWindDirection;
        }

        // Subscribe to changes for clients
        netWindSpeed.OnValueChanged += OnWindSpeedChanged;
        netWindDirection.OnValueChanged += OnWindDirectionChanged;

        // Apply initial values locally
        ApplyWind(netWindSpeed.Value, netWindDirection.Value);
    }

    void Update()
    {
        if (!IsServer) return;

        // Example: allow server to modify wind dynamically
        // For demo purposes only; you could use UI or other logic
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetWind(netWindSpeed.Value + 1f, netWindDirection.Value);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetWind(netWindSpeed.Value - 1f, netWindDirection.Value);
        }
    }

    /// <summary>
    /// Called on server to change wind values.
    /// </summary>
    public void SetWind(float speed, float direction)
    {
        if (!IsServer) return;

        netWindSpeed.Value = speed;
        netWindDirection.Value = direction;
    }

    private void OnWindSpeedChanged(float oldValue, float newValue)
    {
        ApplyWind(newValue, netWindDirection.Value);
    }

    private void OnWindDirectionChanged(float oldValue, float newValue)
    {
        ApplyWind(netWindSpeed.Value, newValue);
    }

    private void ApplyWind(float speed, float direction)
    {
        if (oceanRenderer == null) return;

        oceanRenderer._globalWindSpeed = speed;
        oceanRenderer._globalWindDirectionAngle = direction;
    }
}

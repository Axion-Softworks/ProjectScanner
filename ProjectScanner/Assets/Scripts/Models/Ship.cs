using UnityEngine;

public class Ship : MonoBehaviour
{
    [Header("Variables")]
    public float Acceleration = 5f;
    public float MaxSpeed = 10f;
    public float Mass = 2f;
    public float RotationSpeed = 1f;
    public int numberOfSensors = 8;
    public float sensorDistance = 10f;
}
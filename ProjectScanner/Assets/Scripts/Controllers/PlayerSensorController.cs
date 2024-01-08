using System.Collections.Generic;
using UnityEngine;

public class PlayerSensorController : MonoBehaviour
{
    [Header("Objects")]
    private Ship _playerShip;
    public GameObject sensorPrefab;
    public GameObject sensorParent;
    private List<GameObject> _sensors;

    [Header("Variables")]
    private bool _deployed;

    // Start is called before the first frame update
    void Start()
    {
        _playerShip = this.GetComponent<Ship>();

        if (_playerShip == null)
            Debug.LogError("Parameter _playerShip is null");

        for (int i = 0; i < _playerShip.numberOfSensors; i++)
        {
            var sensor = GameObject.Instantiate(sensorPrefab);
            sensor.transform.parent = sensorParent.transform;
            _sensors.Add(sensor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleSensorDeployment();
        }

        MaintainSensorPosition();
    }

    private void ToggleSensorDeployment()
    {
        _deployed = !_deployed;
    }

    private void MaintainSensorPosition()
    {
        if (!_deployed)
            RetractSensors();
        else
            ExtendSensors();
    }

    private void RetractSensors() 
    {
        var targetPosition = this.transform.position;

        foreach (var sensor in _sensors)
        {
            sensor.transform.position = Vector3.Lerp(sensor.transform.position, targetPosition, Time.deltaTime);
        }
    }

    private void ExtendSensors() 
    {
        var circumferencePeriod = 360/_playerShip.numberOfSensors;
        var offset = new Vector3(0, 0, _playerShip.sensorDistance);

        for (int i = 0; i < _sensors.Count; i++)
        {
            var vector = sensorParent.transform.localPosition + offset;
            var rotatedVector = Quaternion.AngleAxis(circumferencePeriod * i, Vector3.up) * vector; 

            _sensors[i].transform.localPosition = Vector3.Lerp(_sensors[i].transform.localPosition, rotatedVector, Time.deltaTime);
        }
    }
}

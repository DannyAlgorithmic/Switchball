using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperController : MonoBehaviour {

    #region Private Variables
    [Range(0, 25)]
    public int ShotPower = 15;
    [Range(0, 65)]
    public int MaxAngle = 54;
    public Rigidbody2D LeftFlipper, RightFlipper;
    #endregion

    #region Private Variables
    private float minAngleLeft = 0, minAngleRight = 0;
    private bool activeHardLeft = false, activeHardRight = false, activeSoftLeft = true, activeSoftRight = true;
    private float pressureLeft = 0, pressureRight = 0;
    private KeyCode leftTriggerHard = KeyCode.Joystick1Button4, rightTriggerHard = KeyCode.Joystick1Button5;
    private const string leftTriggerSoft = "Left Trigger Soft", rightTriggerSoft = "Right Trigger Soft";
    #endregion

    private void OnEnable()
    {
        minAngleLeft = LeftFlipper.rotation;
        minAngleRight = RightFlipper.rotation;
    }

    private void FixedUpdate()
    {
        // Shooting
        ManageHardFlips(LeftFlipper, activeHardLeft, minAngleLeft, minAngleLeft + MaxAngle, ShotPower);
        ManageHardFlips(RightFlipper, activeHardRight, minAngleRight, minAngleRight + -MaxAngle, ShotPower);

        ManageSoftFlips(LeftFlipper, activeSoftLeft, pressureLeft, minAngleLeft, MaxAngle, ShotPower);
        ManageSoftFlips(RightFlipper, activeSoftRight, pressureRight, minAngleRight, -MaxAngle, ShotPower);

        // Returning
        if (activeHardLeft == false && activeSoftLeft == false && Mathf.Approximately(LeftFlipper.rotation, minAngleLeft) == false)
        {
            HardFlip(LeftFlipper, minAngleLeft, ShotPower - 4);
        }

        if (activeHardRight == false && activeSoftRight == false && Mathf.Approximately(RightFlipper.rotation, minAngleRight) == false)
        {
            HardFlip(RightFlipper, minAngleRight, ShotPower - 4);
        }
    }

    private void Update()
    {
        activeHardLeft = Input.GetKey(leftTriggerHard);
        activeHardRight = Input.GetKey(rightTriggerHard);

        if (activeHardLeft == false)
        {
            pressureLeft = Input.GetAxis(leftTriggerSoft);
            activeSoftLeft = (pressureLeft > 0) ? true : false;
        }
        else if(pressureLeft > 0)
        {
            pressureLeft = 0;
        }

        if (activeHardRight == false)
        {
            pressureRight = Input.GetAxis(rightTriggerSoft);
            activeSoftRight = (pressureRight > 0) ? true : false;
        }
        else if (pressureRight > 0)
        {
            pressureRight = 0;
        }
    }

    #region Flip Control Functions
    private void ManageHardFlips(Rigidbody2D _flipper, bool _isActive, float _minAngle, float _maxAngle, float _shotPower)
    {
        if (_isActive == true)
        {
            HardFlip(_flipper, _maxAngle, _shotPower);
        }
    }

    private void ManageSoftFlips(Rigidbody2D _flipper, bool _isActive, float _pressure, float _minAngle, float _maxAngle, float shotPower)
    {
        if (_isActive == true)
        {
            float currentAngle = _minAngle + _maxAngle * ( _pressure * 20);
            SoftFlip(_flipper, currentAngle);
        }
    }
    #endregion

    #region Flip Functions

    private void HardFlip(Rigidbody2D _flipper, float _maxAngle, float _shotPower)
    {
        _flipper.MoveRotation(Mathf.MoveTowardsAngle(_flipper.rotation, _maxAngle, _shotPower));
    }

    private void SoftFlip(Rigidbody2D _flipper, float _currentAngle)
    {
        _flipper.MoveRotation(_currentAngle);
    }

    #endregion

}

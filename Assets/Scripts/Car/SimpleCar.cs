using System;
using UnityEngine;

namespace Car
{
    public class SimpleCar : MonoBehaviour
    {
        [Header("Steer")]
        [SerializeField]
        private float maxSteer = 45;
        [SerializeField]
        private Wheel[] steerWheels = Array.Empty<Wheel>();
        [Header("Power")]
        [SerializeField]
        private float power = 10;
        [SerializeField]
        private Wheel[] powerWheels = Array.Empty<Wheel>();
        [Space]
        [SerializeField]
        private Lights lights;


        private enum Drive
        {
            FWD,
            RWD,
            AWD
        }

        private Drive _carDrive = Drive.RWD;

        private void Update()
        {
            Turning();
            Powering();
            Lights();
        }

        private void Turning()
        {
            foreach (var wheelCollider in steerWheels)
            {
                wheelCollider.Steer(
                    Input.GetAxis("Horizontal")
                    * maxSteer
                );
            }
        }

        private void Powering()
        {
            switch(_carDrive) {
                case Drive.FWD:
                PoweringFront();
                break;
                case Drive.RWD:
                PoweringRear();
                break;
                case Drive.AWD:
                PoweringFront();
                PoweringRear();
                break;
            }
        }

        private void PoweringFront()
        {
            foreach (var powerWheel in steerWheels)
            {
                powerWheel.Torque(
                    Input.GetAxis("Vertical")
                    * power
                    * Time.deltaTime
                );
            }
        }

        private void PoweringRear()
        {
            foreach (var powerWheel in powerWheels)
            {
                powerWheel.Torque(
                    Input.GetAxis("Vertical")
                    * power
                    * Time.deltaTime
                );
            }
        }

        // private void Powering()
        // {
        //     foreach (var powerWheel in powerWheels)
        //     {
        //         powerWheel.Torque(
        //             Input.GetAxis("Vertical")
        //             * power
        //             * Time.deltaTime
        //         );
        //     }
        // }

        private void Lights()
        {
            lights.TailLights(Input.GetAxis("Vertical") < 0);

            lights.FrontLights(Input.GetKeyDown(KeyCode.Q));
        }

        void OnGUI()
        {
            Rect Rect1 = new Rect(20.0f, 20, 100, 50);
            Rect Rect2 = new Rect(140.0f, 20, 100, 50);
            Rect Rect3 = new Rect(260.0f, 20, 100, 50);

            // Front-wheel drive
            if (GUI.Button(Rect1, "FWD")) {
                _carDrive = Drive.FWD;
            }
            // Rear-wheel drive
            if (GUI.Button(Rect2, "RWD")) {
                _carDrive = Drive.RWD;
            }
            // All-wheel drive
            if (GUI.Button(Rect3, "AWD")) {
                _carDrive = Drive.AWD;
            }
        }
    }
}
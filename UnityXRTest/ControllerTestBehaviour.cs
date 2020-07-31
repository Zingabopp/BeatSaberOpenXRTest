using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.XR;

namespace UnityXRTest
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
	public class ControllerTestBehaviour : MonoBehaviour
    {

        private void InputDevices_deviceConfigChanged(InputDevice obj)
        {
            Plugin.log.Info("----Device Config Changed----");
            obj.PrintDeviceInfo();
        }

        private void InputDevices_deviceDisconnected(InputDevice obj)
        {
            Plugin.log.Info("----Device Disconnected----");
            obj.PrintDeviceInfo();
        }

        private void InputDevices_deviceConnected(InputDevice obj)
        {
            Plugin.log.Info("----Device Connected----");
            obj.PrintDeviceInfo();
        }

        #region Monobehaviour Messages
        /// <summary>
        /// Only ever called once, mainly used to initialize variables.
        /// </summary>
        private void Awake()
        {
            Plugin.log.Info($"ControllerTestBehaviour Awake.");
            InputDevices.deviceConnected += InputDevices_deviceConnected;
            InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
            InputDevices.deviceConfigChanged += InputDevices_deviceConfigChanged;
        }

        /// <summary>
        /// Called every frame if the script is enabled.
        /// </summary>
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                try
                {
                    List<InputDevice> inputDevices = new List<InputDevice>();
                    InputDevices.GetDevices(inputDevices);
                    Plugin.log.Info($"Found {inputDevices.Count} devices:");
                    foreach (var dev in inputDevices)
                    {
                        dev.PrintDeviceInfo();
                    }
                }
                catch (Exception ex)
                {
                    Plugin.log.Error($"Error getting device list: {ex.Message}");
                    Plugin.log.Debug(ex);
                }
            }
        }

        /// <summary>
        /// Called when the script is being destroyed.
        /// </summary>
        private void OnDestroy()
        {
            InputDevices.deviceConnected -= InputDevices_deviceConnected;
            InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;
            InputDevices.deviceConfigChanged -= InputDevices_deviceConfigChanged;
            Plugin.log.Info($"ControllerTestBehaviour Destroyed.");
        }
        #endregion
    }
}

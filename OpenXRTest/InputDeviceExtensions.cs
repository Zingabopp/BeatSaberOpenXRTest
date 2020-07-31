using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.XR;

namespace OpenXRTest
{
    public static class InputDeviceExtensions
    {
        public static void PrintDeviceInfo(this InputDevice device)
        {
            Plugin.log.Error($"{device.name} | {device.serialNumber}");
            Plugin.log.Info($"Characteristics: {device.characteristics}");
            Plugin.log.Info($"isValid: {device.isValid}");
#pragma warning disable CS0618 // Type or member is obsolete
            Plugin.log.Info($"Role: {device.role}");
#pragma warning restore CS0618 // Type or member is obsolete
            var featureList = new List<InputFeatureUsage>();

            if (device.TryGetFeatureUsages(featureList))
            {
                Plugin.log.Info($"Features: {string.Join(", ", featureList.Select(f => $"{f.name}|{f.type}"))}");
            }
            else
                Plugin.log.Error("Unable to get features");
            if (device.TryGetHapticCapabilities(out HapticCapabilities hcap))
            {
                Plugin.log.Info($"HapticCapabilities: {hcap.HapticFeaturesString()}");
                Plugin.log.Info($"BufferValues: {hcap.HapticBufferValues()}");
            }
            else
                Plugin.log.Warn("No Haptic Capabilities");
        }

        public static string HapticFeaturesString(this HapticCapabilities hcap)
        {
            return $"{nameof(hcap.numChannels)}: {hcap.numChannels} | {nameof(hcap.supportsBuffer)}: {hcap.supportsBuffer} | {nameof(hcap.supportsImpulse)}: {hcap.supportsImpulse}";
        }

        public static string HapticBufferValues(this HapticCapabilities hcap)
        {
            return $"bufferFrequencyHz: {hcap.bufferFrequencyHz} | {nameof(hcap.bufferMaxSize)}: {hcap.bufferMaxSize} | {nameof(hcap.bufferOptimalSize)}: {hcap.bufferOptimalSize}";
        }
    }
}

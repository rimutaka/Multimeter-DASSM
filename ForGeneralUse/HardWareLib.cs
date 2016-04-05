// Decompiled with JetBrains decompiler
// Type: ForGeneralUse.HardWareLib
// Assembly: DT-9961T, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 681D06A8-439A-4E73-B916-52A7EBFBD7ED
// Assembly location: C:\Program Files (x86)\Multimeter\Multimeter.exe

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ForGeneralUse
{
  public class HardWareLib
  {
    public const int DIGCF_ALLCLASSES = 4;
    public const int DIGCF_DEVICEINTERFACE = 16;
    public const int DIGCF_PRESENT = 2;
    public const int INVALID_HANDLE_VALUE = -1;
    public const int MAX_DEV_LEN = 1000;

    [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr SetupDiGetClassDevs(ref Guid gClass, string enumerator, IntPtr hParent, uint nFlags);

    [DllImport("setupapi.dll", SetLastError = true)]
    public static extern bool SetupDiGetDeviceInstanceId(IntPtr DeviceInfoSet, HardWareLib.SP_DEVINFO_DATA DeviceInfoData, StringBuilder DeviceInstanceId, uint DeviceInstanceIdSize, uint RequiredSize);

    [DllImport("setupapi.dll", SetLastError = true)]
    public static extern bool SetupDiEnumDeviceInfo(IntPtr lpInfoSet, uint dwIndex, HardWareLib.SP_DEVINFO_DATA devInfoData);

    [DllImport("setupapi.dll", SetLastError = true)]
    public static extern bool SetupDiGetDeviceRegistryProperty(IntPtr lpInfoSet, HardWareLib.SP_DEVINFO_DATA DeviceInfoData, uint Property, uint PropertyRegDataType, StringBuilder PropertyBuffer, uint PropertyBufferSize, IntPtr RequiredSize);

    [DllImport("setupapi.dll", SetLastError = true)]
    public static extern bool SetupDiDestroyDeviceInfoList(IntPtr lpInfoSet);

    public enum SPDRP_
    {
      SPDRP_DEVICEDESC = 0,
      SPDRP_HARDWAREID = 1,
      SPDRP_SERVICE = 4,
      SPDRP_CLASS = 7,
      SPDRP_CLASSGUID = 8,
      SPDRP_DRIVER = 9,
      SPDRP_CONFIGFLAGS = 10,
      SPDRP_MFG = 11,
      SPDRP_FRIENDLYNAME = 12,
      SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = 14,
      SPDRP_CAPABILITIES = 15,
      SPDRP_REMOVAL_POLICY_HW_DEFAULT = 32,
      SPDRP_INSTALL_STATE = 34,
    }

    [StructLayout(LayoutKind.Sequential)]
    public class SP_DEVINFO_DATA
    {
      public int cbSize;
      public Guid classGuid;
      public int devInst;
      public ulong reserved;
    }
  }
}

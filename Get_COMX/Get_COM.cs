// Decompiled with JetBrains decompiler
// Type: Get_COMX.Get_COM
// Assembly: DT-9961T, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 681D06A8-439A-4E73-B916-52A7EBFBD7ED
// Assembly location: C:\Program Files (x86)\Multimeter\Multimeter.exe

using ForGeneralUse;
using System;
using System.Collections;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Get_COMX
{
  internal class Get_COM
  {
    public static ArrayList GetPortNameFormVidPid(string vid, string pid)
    {
      Guid gClass = Guid.Empty;
      string enumerator = "USB";
      ArrayList arrayList = new ArrayList();
      try
      {
        IntPtr classDevs = HardWareLib.SetupDiGetClassDevs(ref gClass, enumerator, IntPtr.Zero, 6U);
        if (classDevs.ToInt32() == -1)
          throw new Exception("没有该类设备");
        HardWareLib.SP_DEVINFO_DATA spDevinfoData = new HardWareLib.SP_DEVINFO_DATA();
        spDevinfoData.cbSize = 28;
        spDevinfoData.devInst = 0;
        spDevinfoData.classGuid = Guid.Empty;
        spDevinfoData.reserved = 0UL;
        StringBuilder PropertyBuffer = new StringBuilder(1000);
        HardWareLib.SetupDiEnumDeviceInfo(classDevs, 0U, spDevinfoData);
        for (uint dwIndex = 0; HardWareLib.SetupDiEnumDeviceInfo(classDevs, dwIndex, spDevinfoData); ++dwIndex)
        {
          HardWareLib.SetupDiGetDeviceRegistryProperty(classDevs, spDevinfoData, 7U, 0U, PropertyBuffer, (uint) PropertyBuffer.Capacity, IntPtr.Zero);
          if (!(PropertyBuffer.ToString().ToLower() != "ports"))
          {
            HardWareLib.SetupDiGetDeviceRegistryProperty(classDevs, spDevinfoData, 1U, 0U, PropertyBuffer, (uint) PropertyBuffer.Capacity, IntPtr.Zero);
            if (PropertyBuffer.ToString().ToLower().Contains(vid.ToLower()) && PropertyBuffer.ToString().ToLower().Contains(pid.ToLower()))
            {
              HardWareLib.SetupDiGetDeviceRegistryProperty(classDevs, spDevinfoData, 12U, 0U, PropertyBuffer, (uint) PropertyBuffer.Capacity, IntPtr.Zero);
              string input = PropertyBuffer.ToString();
              string pattern = "COM[1-9][0-9]?";
              if (Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase))
              {
                string str = Regex.Match(input, pattern, RegexOptions.IgnoreCase).Value.Trim('(', ')');
                arrayList.Add((object) str);
              }
            }
          }
        }
        HardWareLib.SetupDiDestroyDeviceInfoList(classDevs);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message);
        return (ArrayList) null;
      }
      return arrayList;
    }

    public static ArrayList GetSerialPort(string portName)
    {
      ArrayList arrayList = new ArrayList();
      ManagementClass managementClass = new ManagementClass("Win32_SerialPort");
      ManagementObjectCollection instances = managementClass.GetInstances();
      foreach (ManagementBaseObject managementBaseObject in instances)
      {
        string input = managementBaseObject["Name"].ToString();
        if (input.Contains(portName))
        {
          string pattern = "COM[1-9][0-9]?";
          string str = Regex.Match(input, pattern, RegexOptions.IgnoreCase).Value;
          arrayList.Add((object) str);
        }
      }
      instances.Dispose();
      managementClass.Dispose();
      return arrayList;
    }
  }
}

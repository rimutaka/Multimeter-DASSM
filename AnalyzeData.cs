// This class converts byte stream from the device into a human readable format.

using System;
using System.Collections;
using System.Collections.Generic;

namespace DT_3391
{
  internal class AnalyzeData
  {
    private List<string> Templist = new List<string>();
    private List<string> list = new List<string>();

    public bool listdata(ref string listdat, byte[] data)
    {
      if (data.Length < 14)
        return false;
      listdat = DateTime.Now.ToString("o");
      this.list.Clear();
        //The sequence must start with FF or end with either of E8, E1 or E2
      foreach (byte num in data) this.list.Add(num.ToString("X2"));
      string lastByte = this.list[this.list.Count - 1];
      if (this.list[0] != "16" || lastByte != "E8" & lastByte != "E1" & lastByte != "E2") return false;
      this.list.RemoveRange(0, 5); //kill the first 5 bytes?
        //convert the values into something meaningful
      try
      {
        listdat+= this.byte1();
        listdat += this.num4() + this.num3() + this.num2() + this.num1()+",";
        listdat+= this.byte10();
        listdat+= this.byte11();
        listdat+= this.byte12();
        listdat+= this.byte13();
        listdat+= this.byte14();
      }
      catch
      {
          listdat = "";
        return false;
      }
      return true;
    }

    private string byte1()
    {
      string data = ",";
      string str1 = Convert.ToString((int) Convert.ToByte(this.list[0], 16), 2);
      if (str1 == "0") return data;
      int startIndex = str1.Length - 4;
      string str2 = str1.Substring(startIndex, 4);
      string[] str1_1 = new string[4]
      {
        "AC",
        "DC",
        "AUTO",
        "RS232"
      };
      this.todata(ref data, str2, str1_1);
      return data;
    }

    private string num4()
    {
      string twosys = "";
      for (int index = 1; index < 3; ++index)
      {
        string str1 = Convert.ToString((int) Convert.ToByte(this.list[index], 16), 2);
        if (str1 == "0")
          return "";
        if (str1.Length < 4)
        {
          string str2 = "";
          if (str1.Length == 1)
            str2 = "000";
          if (str1.Length == 2)
            str2 = "00";
          if (str1.Length == 3)
            str2 = "0";
          str1 = str2 + str1;
        }
        int startIndex = str1.Length - 4;
        twosys += str1.Substring(startIndex, 4);
      }
      return !(twosys.Substring(0, 1) == "1") ? this.tonum(twosys) : "-" + this.tonum("0" + twosys.Substring(1, 7));
    }

    private string num3()
    {
      string twosys = "";
      for (int index = 3; index < 5; ++index)
      {
        string str1 = Convert.ToString((int) Convert.ToByte(this.list[index], 16), 2);
        if (str1 == "0")
          return "";
        if (str1.Length < 4)
        {
          string str2 = "";
          if (str1.Length == 1)
            str2 = "000";
          if (str1.Length == 2)
            str2 = "00";
          if (str1.Length == 3)
            str2 = "0";
          str1 = str2 + str1;
        }
        int startIndex = str1.Length - 4;
        twosys += str1.Substring(startIndex, 4);
      }
      return !(twosys.Substring(0, 1) == "1") ? this.tonum(twosys) : "." + this.tonum("0" + twosys.Substring(1, 7));
    }

    private string num2()
    {
      string twosys = "";
      for (int index = 5; index < 7; ++index)
      {
        string str1 = Convert.ToString((int) Convert.ToByte(this.list[index], 16), 2);
        if (str1 == "0")
          return "";
        if (str1.Length < 4)
        {
          string str2 = "";
          if (str1.Length == 1)
            str2 = "000";
          if (str1.Length == 2)
            str2 = "00";
          if (str1.Length == 3)
            str2 = "0";
          str1 = str2 + str1;
        }
        int startIndex = str1.Length - 4;
        twosys += str1.Substring(startIndex, 4);
      }
      return !(twosys.Substring(0, 1) == "1") ? this.tonum(twosys) : "." + this.tonum("0" + twosys.Substring(1, 7));
    }

    private string num1()
    {
      string twosys = "";
      for (int index = 7; index < 9; ++index)
      {
        string str1 = Convert.ToString((int) Convert.ToByte(this.list[index], 16), 2);
        if (str1 == "0")
          return "";
        if (str1.Length < 4)
        {
          string str2 = "";
          if (str1.Length == 1)
            str2 = "000";
          if (str1.Length == 2)
            str2 = "00";
          if (str1.Length == 3)
            str2 = "0";
          str1 = str2 + str1;
        }
        int startIndex = str1.Length - 4;
        twosys += str1.Substring(startIndex, 4);
      }
      return !(twosys.Substring(0, 1) == "1") ? this.tonum(twosys) : "." + this.tonum("0" + twosys.Substring(1, 7));
    }

    private string byte10()
    {
      string data = ",";
      string str1 = Convert.ToString((int) Convert.ToByte(this.list[9], 16), 2);
      if (str1 == "0") return data;
      int startIndex = str1.Length - 4;
      string str2 = str1.Substring(startIndex, 4);
      string[] str1_1 = new string[4]
      {
        "μ",
        "n",
        "K",
        "diode"
      };
      this.todata(ref data, str2, str1_1);
      return data;
    }

    private string byte11()
    {
      string data = ",";
      string str1 = Convert.ToString((int) Convert.ToByte(this.list[10], 16), 2);
      if (str1 == "0") return data;
      int startIndex = str1.Length - 4;
      string str2 = str1.Substring(startIndex, 4);
      string[] str1_1 = new string[4]
      {
        "m",
        "%",
        "M",
        "sound"
      };
      this.todata(ref data, str2, str1_1);
      return data;
    }

    private string byte12()
    {
      string data = ",";
      string str1 = Convert.ToString((int) Convert.ToByte(this.list[11], 16), 2);
      if (str1 == "0")
        return data;
      int startIndex = str1.Length - 4;
      string str2 = str1.Substring(startIndex, 4);
      string[] str1_1 = new string[4]
      {
        "F",
        "Ω",
        "REL",
        "HOLD"
      };
      this.todata(ref data, str2, str1_1);
      return data;
    }

    private string byte13()
    {
      string data = ",";
      string str1 = Convert.ToString((int) Convert.ToByte(this.list[12], 16), 2);
      if (str1 == "0")
        return data;
      int startIndex = str1.Length - 4;
      string str2 = str1.Substring(startIndex, 4);
      string[] str1_1 = new string[4]
      {
        "A",
        "V",
        "Hz",
        ""
      };
      this.todata(ref data, str2, str1_1);
      return data;
    }

    private string byte14()
    {
      string data = ",";
      string str1 = Convert.ToString((int) Convert.ToByte(this.list[13], 16), 2);
      if (str1 == "0")
        return data;
      int startIndex = str1.Length - 4;
      string str2 = str1.Substring(startIndex, 4);
      string[] str1_1 = new string[4]
      {
        "",
        "",
        "°C",
        "°F"
      };
      this.todata(ref data, str2, str1_1);
      return data;
    }

      /// <summary>
      /// Return list of values from str1[] where a matching str bit = 1, e.g. 0110 returns 2nd and 3rd element of str1[] 
      /// </summary>
      /// <param name="data"></param>
      /// <param name="str"></param>
      /// <param name="str1"></param>
    private void todata(ref string data, string str, string[] str1)
    {
      char[] chArray = str.ToCharArray();
      for (int index = 0; index < chArray.Length; ++index)
      {
        if ((int) chArray[index] == 49)
          data+=str1[index]+",";
        else
          data+=",";
      }
    }

    private string tonum(string twosys)
    {
      string str = "";
      if (twosys == "00000101")
        str = "1";
      if (twosys == "01011011")
        str = "2";
      if (twosys == "00011111")
        str = "3";
      if (twosys == "00100111")
        str = "4";
      if (twosys == "00111110")
        str = "5";
      if (twosys == "01111110")
        str = "6";
      if (twosys == "00010101")
        str = "7";
      if (twosys == "01111111")
        str = "8";
      if (twosys == "00111111")
        str = "9";
      if (twosys == "01111101")
        str = "0";
      if (twosys == "01101000")
        str = "L";
      return str;
    }
  }
}

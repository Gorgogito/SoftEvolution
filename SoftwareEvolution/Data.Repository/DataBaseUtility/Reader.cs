using System;
using System.Data;
using System.Globalization;
using System.Text;

namespace Infraestructura.Repository
{
  public class Reader
  {

    public static object GetObjectValue(IDataReader dr, string column)
    {
      try
      {
        var obj = dr[column];
        return obj == DBNull.Value ? null : obj;
      }
      catch { return null; }
    }

    public static string GetStringValueEncrypted(IDataReader dr, string column)
    {
      try
      {
        var obj = GetObjectValue(dr, column);
        return obj == null ? "" : obj.ToString();
      }
      catch { return ""; }
    }

    public static string GetStringValue(IDataReader dr, string column)
    {
      try
      {
        var obj = GetObjectValue(dr, column);
        return obj == null ? "" : obj.ToString().Trim();
      }
      catch { return ""; }
    }

    public static float GetRealValue(IDataReader dr, string column)
    {
      try
      {
        var obj = GetObjectValue(dr, column);
        if (obj == null) return 0;
        return Convert.ToSingle(obj);
      }
      catch { return 0; }
    }

    public static double GetFloatValue(IDataReader dr, string column)
    {
      try
      {
        var obj = GetObjectValue(dr, column);
        if (obj == null) return 0;
        return Convert.ToDouble(obj);
      }
      catch { return 0; }
    }

    public static decimal GetDecimalValue(IDataReader dr, string column)
    {
      try
      {
        var obj = GetObjectValue(dr, column);
        if (obj == null) return 0;
        return Convert.ToDecimal(obj);
      }
      catch { return 0; }
    }

    public static Int64 GetBigIntValue(IDataReader dr, string column)
    {
      try
      {
        var obj = GetObjectValue(dr, column);
        if (obj == null) return 0;
        return Convert.ToInt64(obj);
      }
      catch { return 0; }
    }

    public static Int32 GetIntValue(IDataReader dr, string column)
    {
      try
      {
        var obj = GetObjectValue(dr, column);
        if (obj == null) return 0;
        return Convert.ToInt32(obj);
      }
      catch { return 0; }
    }

    public static byte GetByteValue(IDataReader dr, string column)
    {
      try
      {
        var obj = GetObjectValue(dr, column);
        if (obj == null) return 0;
        return Convert.ToByte(obj);
      }
      catch { return 0; }
    }

    public static Int16 GetSmallIntValue(IDataReader dr, string column)
    {
      try
      {
        var obj = GetObjectValue(dr, column);
        if (obj == null) return 0;
        return Convert.ToInt16(obj);
      }
      catch { return 0; }
    }

    public static byte GetTinyIntValue(IDataReader dr, string column)
    {
      try
      {
        var obj = GetObjectValue(dr, column);
        if (obj == null) return 0;
        return Convert.ToByte(obj);
      }
      catch { return 0; }
    }

    public static DateTime GetDateTimeValue(IDataReader dr, string column)
    {
      try
      {
        var obj = dr[column];
        return obj == DBNull.Value ? DateTime.Now : (DateTime)obj;
      }
      catch { return new DateTime(); }
    }

    public static bool GetBooleanValue(IDataReader dr, string column)
    {
      try
      {
        var obj = GetObjectValue(dr, column);
        if (obj == null) return false;
        return Convert.ToBoolean(obj);
      }
      catch { throw; }
    }

    public static double GetDoubleValue(IDataReader dr, string column)
    {
      try
      {
        var obj = GetObjectValue(dr, column);
        if (obj == null) return 0;
        return Convert.ToDouble(obj);
      }
      catch { throw; }
    }

    public static string GetDateStringValue(IDataReader dr, string column)
    {
      var tempDate = new DateTime();
      try
      {
        var obj = dr[column];
        tempDate = (DateTime)obj;
        return tempDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
      }
      catch { return string.Empty; }
    }
    public static string GetVarbinaryStringValue(IDataReader dr, string column)
    {
      try
      {
        byte[] obj = (byte[])dr[column];
        if (obj == null) return "";
        return Encoding.UTF8.GetString(obj); ;
      }
      catch { return ""; }
    }

    public static byte[] GetVarbinaryValue(IDataReader dr, string column)
    {

      try
      {
        byte[] obj = (byte[])dr[column];
        if (obj == null) return new Byte[0];
        return obj;
      }
      catch { return new Byte[0]; }
    }

    public static string GetVarbinaryString64Value(IDataReader dr, string column)
    {
      try
      {
        byte[] obj = (byte[])dr[column];
        if (obj == null) return "";
        return Convert.ToBase64String(obj);
      }
      catch { return ""; }
    }

    public static byte ObjectToByte(object obj)
    { return ((obj == null) || (obj == DBNull.Value)) ? byte.MinValue : Convert.ToByte(obj); }

    public static byte ObjectToByte(IDataReader reader, string columnName)
    { return ObjectToByte(GetReaderValue(reader, columnName)); }

    public static object GetReaderValue(IDataReader reader, string columnName)
    {
      DataTable schema = reader.GetSchemaTable();
      DataRow[] rows = schema.Select(string.Format("ColumnName='{0}'", columnName));
      if ((rows != null) && (rows.Length > 0))
      { return reader[columnName]; }
      else return null;
    }

    public static Byte[] ObjectToBytes(object obj)
    { return ((obj == null) || (obj == DBNull.Value)) ? new Byte[0] : ((Byte[])obj); }

    public static Byte[] ObjectToBytes(IDataReader reader, string columnName)
    { return ObjectToBytes(GetReaderValue(reader, columnName)); }

  }
}

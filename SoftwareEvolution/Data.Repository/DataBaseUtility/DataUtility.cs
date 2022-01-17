using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Infraestructura.Repository
{
  public static class DataUtility
  {
  
    public static T CloneJson<T>(this T source)
    {
      if (source == null)
        return default(T);
      var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
      return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
    }

    public static string GetDateOfSystem()
    { return DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); }

    public static string GetHourOfSystemFt24()
    { return DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture); }

    public static string GetHourOfSystemFt12()
    { return DateTime.Now.ToString("hh:mmtt", CultureInfo.InvariantCulture); }

    public static string GetYearOfSystem()
    { return DateTime.Now.ToString("yyyy", CultureInfo.InvariantCulture); }

    public static string GetMonthOfSystem()
    { return DateTime.Now.ToString("MM", CultureInfo.InvariantCulture); }

    public static string GetDayOfSystem()
    { return DateTime.Now.ToString("dd", CultureInfo.InvariantCulture); }
     
    public static string ConvertDecimalToStringWithTwoDecimals(decimal number, int formatdecimal)
    { return number.ToString("F" + formatdecimal.ToString(), CultureInfo.InvariantCulture); }

    public static DataTable ToDataTable<T>(this IList<T> data)
    {
      PropertyDescriptorCollection props =
          TypeDescriptor.GetProperties(typeof(T));
      DataTable table = new DataTable();
      for (int i = 0; i < props.Count; i++)
      {
        PropertyDescriptor prop = props[i];
        table.Columns.Add(prop.Name, prop.PropertyType);
      }
      object[] values = new object[props.Count];
      foreach (T item in data)
      {
        for (int i = 0; i < values.Length; i++)
        { values[i] = props[i].GetValue(item); }
        table.Rows.Add(values);
      }
      return table;
    }

    public static string ConvertListToXml<T>(T lista, string nombreXmlRoot)
    {
      string cadenaXml = string.Empty;
      Encoding utf8noBOM = new UTF8Encoding(false);
      XmlWriterSettings settings = new XmlWriterSettings
      {
        Indent = true,
        Encoding = utf8noBOM
      };
      XmlSerializer ser = new XmlSerializer(typeof(T), new XmlRootAttribute(nombreXmlRoot));
      StringBuilder sb = new StringBuilder();
      using (XmlWriter xml = XmlWriter.Create(sb, settings))
      { ser.Serialize(xml, lista); };
      cadenaXml = sb.ToString();
      return cadenaXml;
    }

    public static string GetColorHexadecimal(string _Color)
    {
      string colorHexadecimal = string.Empty;
      var auxColor = long.Parse(_Color);
      int b = (int)(auxColor / 65536);
      int g = (int)((auxColor - b * 65536) / 256);
      int r = (int)(auxColor - b * 65536 - g * 256);
      Color colorRGB = Color.FromArgb(r, g, b);
      colorHexadecimal = "#" + colorRGB.R.ToString("X2") + colorRGB.G.ToString("X2") + colorRGB.B.ToString("X2");
      return colorHexadecimal;
    }

    public static object GetReaderValue(IDataReader reader, string columnName)
    {
      DataTable schema = reader.GetSchemaTable();
      DataRow[] rows = schema.Select(string.Format("ColumnName='{0}'", columnName));
      if ((rows != null) && (rows.Length > 0))
      { return reader[columnName]; }
      else return null;
    }

    public static Int64 ObjectToInt64(IDataReader reader, string columnName)
    { return ObjectToInt64(GetReaderValue(reader, columnName)); }

    public static Int64 ObjectToInt64(object obj)
    { return ((obj == null) || (obj == DBNull.Value)) ? 0 : Convert.ToInt64(obj); }

    public static Int16 ObjectToInt16(object obj)
    { return ((obj == null) || (obj == DBNull.Value)) ? Int16.MinValue : Convert.ToInt16(obj); }

    public static Int16 ObjectToInt16(IDataReader reader, string columnName)
    { return ObjectToInt16(GetReaderValue(reader, columnName)); }

    public static Int32 ObjectToInt32(object obj)
    { return ((obj == null) || (obj == DBNull.Value)) ? 0 : Convert.ToInt32(obj); }

    public static Int32? ObjectToInt32Null(object obj)
    {
      Int32? valor = null;
      if ((obj == null) || (obj == DBNull.Value))
      { return valor; }
      else
      { valor = Convert.ToInt32(obj); }
      return valor;
    }

    public static Int32 ObjectToInt32(IDataReader reader, string columnName)
    { return ObjectToInt32(GetReaderValue(reader, columnName)); }

    public static decimal ObjectToDecimal(object obj)
    { return ((obj == null) || (obj == DBNull.Value)) ? 0.00M : Convert.ToDecimal(obj); }

    public static decimal ObjectToDecimal(IDataReader reader, string columnName)
    { return ObjectToDecimal(GetReaderValue(reader, columnName)); }

    public static decimal StrToDecimal(string obj)
    { return ((obj == null) || (obj == "")) ? 0.00M : Convert.ToDecimal(obj); }

    public static decimal StrToDecimal(IDataReader reader, string columnName)
    { return StrToDecimal(ObjectToString(GetReaderValue(reader, columnName))); }

    public static int ObjectToInt(object obj)
    { return ObjectToInt32(obj); }

    public static int ObjectToInt(IDataReader reader, string columnName)
    { return ObjectToInt(GetReaderValue(reader, columnName)); }

    public static double ObjectToDouble(object obj)
    { return ((obj == null) || (obj == DBNull.Value)) ? 0 : Convert.ToDouble(obj); }

    public static double ObjectToDouble(IDataReader reader, string columnName)
    { return ObjectToDouble(GetReaderValue(reader, columnName)); }

    public static bool ObjectToBool(object obj)
    { return ((obj == null) || (obj == DBNull.Value)) ? false : Convert.ToBoolean(obj); }

    public static bool ObjectToBool(IDataReader reader, string columnName)
    { return ObjectToBool(GetReaderValue(reader, columnName)); }

    public static string ObjectToString(object obj)
    { return ((obj == null) || (obj == DBNull.Value)) ? "" : Convert.ToString(obj); }

    public static string ObjectToString(IDataReader reader, string columnName)
    { return ObjectToString(GetReaderValue(reader, columnName)); }

    public static DateTime ObjectToDateTime(object obj)
    { return ((obj == null) || (obj == DBNull.Value)) ? DateTime.MinValue : Convert.ToDateTime(obj); }

    public static TimeSpan ObjectToTimeSpan(object obj)
    { return ((obj == null) || (obj == DBNull.Value)) ? TimeSpan.MinValue : TimeSpan.Parse(obj.ToString()); }

    public static DateTime? ObjectToDateTimeNull(object obj)
    {
      DateTime? valor = null;
      if ((obj == null) || (obj == DBNull.Value))
      { return valor; }
      else
      { valor = Convert.ToDateTime(obj); }
      return valor;
    }

    public static DateTime ObjectToDateTime(IDataReader reader, string columnName)
    { return ObjectToDateTime(GetReaderValue(reader, columnName)); }

    public static TimeSpan ObjectToTimeSpan(IDataReader reader, string columnName)
    { return ObjectToTimeSpan(GetReaderValue(reader, columnName)); }

    public static DateTime StringToDateTime(string str)
    { return ((str == "")) ? DateTime.MinValue : Convert.ToDateTime(str); }

    public static DateTime StringToDateTime(IDataReader reader, string columnName)
    { return StringToDateTime(ObjectToString(GetReaderValue(reader, columnName))); }

    public static byte ObjectToByte(object obj)
    { return ((obj == null) || (obj == DBNull.Value)) ? byte.MinValue : Convert.ToByte(obj); }

    public static byte ObjectToByte(IDataReader reader, string columnName)
    { return ObjectToByte(GetReaderValue(reader, columnName)); }

    public static int StringToInt(string str)
    { return ((str == null) || (str == "")) ? 0 : Convert.ToInt32(str); }

    public static int StringToInt(IDataReader reader, string columnName)
    { return StringToInt(ObjectToString(GetReaderValue(reader, columnName))); }

    public static object IntToDBNull(int int1)
    { return ((int1 == 0)) ? DBNull.Value : (object)int1; }

    public static object IntToDBNull(IDataReader reader, string columnName)
    { return IntToDBNull(ObjectToInt(GetReaderValue(reader, columnName))); }

    public static object Int32ToDBNull(Int32 int1)
    { return ((int1 == 0)) ? DBNull.Value : (object)int1; }

    public static object Int32ToDBNull(IDataReader reader, string columnName)
    { return Int32ToDBNull(ObjectToInt32(GetReaderValue(reader, columnName))); }

    public static object Int64ToDBNull(Int64 int1)
    { return ((int1 == 0)) ? DBNull.Value : (object)int1; }

    public static object Int64ToDBNull(IDataReader reader, string columnName)
    { return Int64ToDBNull(ObjectToInt64(GetReaderValue(reader, columnName))); }

    public static object DateTimeToDBNull(DateTime date)
    { return ((date == DateTime.MinValue)) ? DBNull.Value : (object)date; }

    public static object DateTimeToDBNull(IDataReader reader, string columnName)
    { return DateTimeToDBNull(ObjectToDateTime(GetReaderValue(reader, columnName))); }

    public static string ObjectDecimalToStringFormatThousands(object obj)
    { return ObjectToDecimal(obj).ToString("#,#0.00"); }

    public static string ObjectDecimalToStringFormatThousands(IDataReader reader, string columnName)
    { return ObjectDecimalToStringFormatThousands(GetReaderValue(reader, columnName)); }

    public static string BoolToString(bool flag)
    { return flag ? "1" : "0"; }

    public static string BoolToString(IDataReader reader, string columnName)
    { return BoolToString(ObjectToBool(GetReaderValue(reader, columnName))); }

    public static bool StringToBool(string flag)
    { return flag.Equals("1"); }

    public static bool StringToBool(IDataReader reader, string columnName)
    { return StringToBool(ObjectToString(GetReaderValue(reader, columnName))); }

    public static string IntToString(int int1)
    { return ((int1 == 0)) ? "" : Convert.ToString(int1); }

    public static string IntToString(IDataReader reader, string columnName)
    { return IntToString(ObjectToInt(GetReaderValue(reader, columnName))); }

    public static string GenerateXML(string[] array, bool cabecera)
    {
      string retorna;
      MemoryStream memory_stream = new MemoryStream();
      XmlTextWriter xml_text_writer = new XmlTextWriter(memory_stream, System.Text.Encoding.UTF8);
      xml_text_writer.Formatting = System.Xml.Formatting.Indented;
      xml_text_writer.Indentation = 4;
      GenerateHeadboard(xml_text_writer, cabecera, 'A');
      xml_text_writer.WriteStartElement("string-array");
      for (int i = 0; i < array.Length; i++)
      {
        if (array[i] == null)
          xml_text_writer.WriteElementString("null", "");
        else
          xml_text_writer.WriteElementString("string", array[i]);
      }
      xml_text_writer.WriteEndElement();
      GenerateHeadboard(xml_text_writer, cabecera, 'C');
      xml_text_writer.Flush();
      // Declaramos un StreamReader para mostrar el resultado.
      StreamReader stream_reader = new StreamReader(memory_stream);
      memory_stream.Seek(0, SeekOrigin.Begin);
      retorna = stream_reader.ReadToEnd();
      xml_text_writer.Close();
      stream_reader.Close();
      stream_reader.Dispose();
      return retorna;
    }

    private static void GenerateHeadboard(XmlTextWriter xmlTextWriter, bool genera, char estado)
    {
      if (genera)
      {
        switch (estado)
        {
          case 'A':
            xmlTextWriter.WriteStartDocument();
            break;
          case 'C':
            xmlTextWriter.WriteEndDocument();
            break;
          default:
            break;
        }
      }
    }

    public static DateTime StringddmmyyyyToDate(string strDate)
    {
      //dd/mm/yyyy
      int year = ObjectToInt(strDate.Split('/')[2]);
      int month = ObjectToInt(strDate.Split('/')[1]);
      int day = ObjectToInt(strDate.Split('/')[0]);
      return new DateTime(year, month, day);
    }

    public static string BytesToString(byte[] byt)
    { return System.Text.Encoding.Default.GetString(byt); }

    public static string ObjectToXML(Object obj)
    {
      XmlSerializer xs = new XmlSerializer(obj.GetType());
      string xml = string.Empty;
      using (MemoryStream ms = new MemoryStream())
      {
        xs.Serialize(ms, obj);
        using (StreamReader sr = new StreamReader(ms))
        {
          ms.Seek(0, SeekOrigin.Begin);
          xml = sr.ReadToEnd();
        }
      }
      return xml;
    }

    public static string StringToSlug(string str)
    {
      str = str.Normalize(System.Text.NormalizationForm.FormD);
      str = new Regex(@"[^a-zA-Z0-9 ]").Replace(str, "").Trim();
      str = new Regex(@"[\/_| -]+").Replace(str, " ");
      return str;
    }

    public static string RemoveDiacritics(string stIn)
    {
      string stFormD = stIn.Normalize(NormalizationForm.FormD);
      StringBuilder sb = new StringBuilder();
      for (int ich = 0; ich <= stFormD.Length - 1; ich++)
      {
        UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
        if (uc != UnicodeCategory.NonSpacingMark || stFormD[ich].ToString() == "̃")
        { sb.Append(stFormD[ich]); }
      }
      return (sb.ToString().Normalize(NormalizationForm.FormC));
    }

    public static int Age(DateTime birthdate)
    {
      int edad = DateTime.Today.Year - birthdate.Year;
      //si el mes es menor restamos un año directamente
      if (DateTime.Today.Month < birthdate.Month)
      { --edad; }
      //sino preguntamos si estamos en el mismo mes, si es el mismo preguntamos si el dia de hoy es menor al de la fecha de nacimiento
      else if (DateTime.Today.Month == birthdate.Month && DateTime.Today.Day < birthdate.Day)
      { --edad; }
      return edad;
    }

  }

}

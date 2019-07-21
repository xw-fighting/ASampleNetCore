using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASample.NetCore
{
    public class JsonConverExtension
    {

    }

    public class ChinaDateTimeConverter : DateTimeConverterBase
    {
        private static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter();

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var result = dtConverter.ReadJson(reader, objectType, existingValue, serializer);
                return result;
            }
            catch (Exception)
            {
                return Convert.ToDateTime(reader.Value);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            dtConverter.WriteJson(writer, value, serializer);
        }
    }

    public class ChinaDateConverter : DateTimeConverterBase
    {
        private static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var result = dtConverter.ReadJson(reader, objectType, existingValue, serializer);
                return result;
            }
            catch (Exception)
            {
                return Convert.ToDateTime(reader.Value);
                throw;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
            dtConverter.WriteJson(writer, value, serializer);
        }
    }

    public class ChinaTimeConverter : DateTimeConverterBase
    {
        private static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { };
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var result = dtConverter.ReadJson(reader, objectType, existingValue, serializer);
                return result;
            }
            catch (Exception)
            {
                return Convert.ToDateTime(reader.Value);
                throw;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dtConverter = new IsoDateTimeConverter { DateTimeFormat = "HH:mm" };
            dtConverter.WriteJson(writer, value, serializer);
        }
    }

    public class BooleanConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue((bool)value ? "是" : "否");
        }
    }
}

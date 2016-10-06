using System;
using System.Collections;
using System.Reflection;
using Newtonsoft.Json;

namespace Digipolis.Web.Api.JsonConverters
{
    internal class PageResultConverter : JsonConverter
    {
        public override bool CanRead { get; } = false;

        public override bool CanWrite { get; } = true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var type = value?.GetType()?.GetGenericArguments()?[0];
            var generic = typeof(PagedResult<>).MakeGenericType(type);
            dynamic obj = Convert.ChangeType(value, generic) ;

            var links = obj?.Links as PagedResultLinks;
            var data = obj?.Data as IEnumerable;
            var page = obj?.Page as Page;

            writer.WriteStartObject();
                if(links != null) { }
                writer.WritePropertyName("_links");
                if (links != null) serializer.Serialize(writer, links);
                else
                {
                    writer.WriteStartObject();
                    writer.WriteEndObject();
                }
                writer.WritePropertyName("_embedded");
                    writer.WriteStartObject();
                        writer.WritePropertyName("resourceList");
                            writer.WriteStartArray();
                                foreach (var item in data)
                                {
                                    serializer.Serialize(writer, item);
                                }
                            writer.WriteEndArray();
                    writer.WriteEndObject();
                writer.WritePropertyName("_page");
                   if(page != null) serializer.Serialize(writer, page);
                    else
                    {
                        writer.WriteStartObject();
                        writer.WriteEndObject();
                    }
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.GetTypeInfo().IsGenericType && objectType.GetGenericTypeDefinition() == typeof(PagedResult<>);
        }
    }
}
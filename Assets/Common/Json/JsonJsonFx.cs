using System;
using System.Collections.Generic;

namespace Common
{
    class JsonJsonFx : Json.IProxy
    {
        #region IProxy 成员

        public T Deserialize<T>(string value)
        {
#if WINDOWS_PHONE
			return new JsonFx.Json.JsonReader().Read<T>(value);
#else
            return JsonFx.Json.JsonReader.Deserialize<T>(value);
#endif
        }

        public string Serialize(object value)
        {
#if WINDOWS_PHONE
			return new JsonFx.Json.JsonWriter().Write(value);
#else
            return JsonFx.Json.JsonWriter.Serialize(value);
#endif
        }

        #endregion
    }
}

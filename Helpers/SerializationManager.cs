using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace clickfree_Maui.Helpers
{
    public static class SerializationManager
    {
        #region Methods

        public static T Deserialize<T>(Stream stream) where T : class
        {
            T result = default(T);

            try
            {
                if (stream != null)
                {
                    DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(T));

                    result = dcjs.ReadObject(stream) as T;
                }
            }
            catch
            {
            }

            return result;
        }

        #endregion
    }
}

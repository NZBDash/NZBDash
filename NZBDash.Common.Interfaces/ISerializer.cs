using System;
using System.Collections.Generic;

namespace NZBDash.Common.Interfaces
{
    public interface ISerializer
    {
        /// <summary>
        /// Serializes the XML data.
        /// </summary>
        /// <typeparam name="T">Return Type</typeparam>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        T SerializeXmlData<T>(string uri) where T : new();

        /// <summary>
        /// Serializeds the json data.
        /// </summary>
        /// <typeparam name="T">Return Type</typeparam>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        T SerializedJsonData<T>(string url) where T : new();

        /// <summary>
        /// Serializeds the json data.
        /// </summary>
        /// <typeparam name="T">The Func's return type</typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="method">The method.</param>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        T SerializedJsonData<T>(string url, string method, Func<T> func) where T : new();
    }
}

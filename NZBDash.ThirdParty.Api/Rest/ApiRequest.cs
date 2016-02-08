#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: ApiRequest.cs
//   Created By: Jamie Rees
//  
//   Permission is hereby granted, free of charge, to any person obtaining
//   a copy of this software and associated documentation files (the
//   "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish,
//   distribute, sublicense, and/or sell copies of the Software, and to
//   permit persons to whom the Software is furnished to do so, subject to
//   the following conditions:
//  
//   The above copyright notice and this permission notice shall be
//   included in all copies or substantial portions of the Software.
//  
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//   EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//   LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//   OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//   WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ************************************************************************/
#endregion
using System;
using System.Threading.Tasks;

using NZBDash.ThirdParty.Api.Interfaces;

using RestSharp;

namespace NZBDash.ThirdParty.Api.Rest
{
    public class ApiRequest : IApiRequest
    {
        /// <summary>
        /// An Async API request handler
        /// </summary>
        /// <typeparam name="T">The type of class you want to deserialize</typeparam>
        /// <param name="request">The Api request</param>
        /// <param name="baseUri">The base URI of the request</param>
        /// <returns>The type of class you want to deserialize</returns>
        public Task<T> ExecuteAsync<T>(RestRequest request, Uri baseUri) where T : new()
        {
            var client = new RestClient();
            var taskCompletionSource = new TaskCompletionSource<T>();

            client.BaseUrl = baseUri;

            client.ExecuteAsync<T>(request, (response) => taskCompletionSource.SetResult(response.Data));

            return taskCompletionSource.Task;
        }
    }
}

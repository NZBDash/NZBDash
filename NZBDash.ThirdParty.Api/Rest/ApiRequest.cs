﻿#region Copyright
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

using NZBDash.Common.Interfaces;
using NZBDash.ThirdParty.Api.Interfaces;

using RestSharp;

namespace NZBDash.ThirdParty.Api.Rest
{
    public class ApiRequest : IApiRequest
    {
        public ApiRequest(ILogger logger)
        {
            Logger = logger;
        }
        private ILogger Logger { get; set; }

        /// <summary>
        /// An API request handler
        /// </summary>
        /// <typeparam name="T">The type of class you want to deserialize</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="baseUri">The base URI.</param>
        /// <returns>The type of class you want to deserialize</returns>
        public T Execute<T>(IRestRequest request, Uri baseUri) where T : new()
        {
            var client = new RestClient { BaseUrl = baseUri };

            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                Logger.Fatal("Exception thrown while Executing a API request. BaseURI = {0}, Resource = {1}, Method Type = {2}", baseUri, request.Resource, request.Method);
                Logger.Fatal(response.ErrorException);

                var message = "Error retrieving response. Check inner details for more info.";
                throw new ApplicationException(message, response.ErrorException);
            }

            return response.Data;
        }
    }
}

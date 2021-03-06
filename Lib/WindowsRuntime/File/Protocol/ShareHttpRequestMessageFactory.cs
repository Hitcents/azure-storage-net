﻿// -----------------------------------------------------------------------------------------
// <copyright file="ShareHttpRequestMessageFactory.cs" company="Microsoft">
//    Copyright 2013 Microsoft Corporation
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>
// -----------------------------------------------------------------------------------------

namespace Microsoft.WindowsAzure.Storage.File.Protocol
{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Core;
    using Microsoft.WindowsAzure.Storage.Shared.Protocol;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    internal static class ShareHttpRequestMessageFactory
    {
        /// <summary>
        /// Constructs a web request to create a new share.
        /// </summary>
        /// <param name="uri">The absolute URI to the share.</param>
        /// <param name="timeout">The server timeout interval.</param>
        /// <returns>A web request to use to perform the operation.</returns>
        public static HttpRequestMessage Create(Uri uri, int? timeout, HttpContent content, OperationContext operationContext)
        {
            UriQueryBuilder shareBuilder = GetShareUriQueryBuilder();
            return HttpRequestMessageFactory.Create(uri, timeout, shareBuilder, content, operationContext);
        }

        /// <summary>
        /// Constructs a web request to delete the share and all of the files within it.
        /// </summary>
        /// <param name="uri">The absolute URI to the share.</param>
        /// <param name="timeout">The server timeout interval.</param>
        /// <param name="accessCondition">The access condition to apply to the request.</param>
        /// <returns>A web request to use to perform the operation.</returns>
        public static HttpRequestMessage Delete(Uri uri, int? timeout, AccessCondition accessCondition, HttpContent content, OperationContext operationContext)
        {
            UriQueryBuilder shareBuilder = GetShareUriQueryBuilder();
            HttpRequestMessage request = HttpRequestMessageFactory.Delete(uri, timeout, shareBuilder, content, operationContext);
            request.ApplyAccessCondition(accessCondition);
            return request;
        }

        /// <summary>
        /// Generates a web request to return the user-defined metadata for this share.
        /// </summary>
        /// <param name="uri">The absolute URI to the share.</param>
        /// <param name="timeout">The server timeout interval.</param>
        /// <param name="accessCondition">The access condition to apply to the request.</param>
        /// <returns>A web request to use to perform the operation.</returns>
        public static HttpRequestMessage GetMetadata(Uri uri, int? timeout, AccessCondition accessCondition, HttpContent content, OperationContext operationContext)
        {
            UriQueryBuilder shareBuilder = GetShareUriQueryBuilder();
            HttpRequestMessage request = HttpRequestMessageFactory.GetMetadata(uri, timeout, shareBuilder, content, operationContext);
            request.ApplyAccessCondition(accessCondition);
            return request;
        }

        /// <summary>
        /// Generates a web request to return the properties and user-defined metadata for this share.
        /// </summary>
        /// <param name="uri">The absolute URI to the share.</param>
        /// <param name="timeout">The server timeout interval.</param>
        /// <param name="accessCondition">The access condition to apply to the request.</param>
        /// <returns>A web request to use to perform the operation.</returns>
        public static HttpRequestMessage GetProperties(Uri uri, int? timeout, AccessCondition accessCondition, HttpContent content, OperationContext operationContext)
        {
            UriQueryBuilder shareBuilder = GetShareUriQueryBuilder();
            HttpRequestMessage request = HttpRequestMessageFactory.GetProperties(uri, timeout, shareBuilder, content, operationContext);
            request.ApplyAccessCondition(accessCondition);
            return request;
        }

        /// <summary>
        /// Generates a web request to set user-defined metadata for the share.
        /// </summary>
        /// <param name="uri">The absolute URI to the share.</param>
        /// <param name="timeout">The server timeout interval.</param>
        /// <param name="accessCondition">The access condition to apply to the request.</param>
        /// <returns>A web request to use to perform the operation.</returns>
        public static HttpRequestMessage SetMetadata(Uri uri, int? timeout, AccessCondition accessCondition, HttpContent content, OperationContext operationContext)
        {
            UriQueryBuilder shareBuilder = GetShareUriQueryBuilder();
            HttpRequestMessage request = HttpRequestMessageFactory.SetMetadata(uri, timeout, shareBuilder, content, operationContext);
            request.ApplyAccessCondition(accessCondition);
            return request;
        }

        /// <summary>
        /// Adds user-defined metadata to the request as one or more name-value pairs.
        /// </summary>
        /// <param name="request">The web request.</param>
        /// <param name="metadata">The user-defined metadata.</param>
        public static void AddMetadata(HttpRequestMessage request, IDictionary<string, string> metadata)
        {
            HttpRequestMessageFactory.AddMetadata(request, metadata);
        }

        /// <summary>
        /// Adds user-defined metadata to the request as a single name-value pair.
        /// </summary>
        /// <param name="request">The web request.</param>
        /// <param name="name">The metadata name.</param>
        /// <param name="value">The metadata value.</param>
        public static void AddMetadata(HttpRequestMessage request, string name, string value)
        {
            HttpRequestMessageFactory.AddMetadata(request, name, value);
        }

        /// <summary>
        /// Constructs a web request to return a listing of all shares in this storage account.
        /// </summary>
        /// <param name="uri">The absolute URI for the account.</param>
        /// <param name="timeout">The server timeout interval.</param>
        /// <param name="listingContext">A set of parameters for the listing operation.</param>
        /// <param name="detailsIncluded">Additional details to return with the listing.</param>
        /// <returns>A web request for the specified operation.</returns>
        public static HttpRequestMessage List(Uri uri, int? timeout, ListingContext listingContext, ShareListingDetails detailsIncluded, HttpContent content, OperationContext operationContext)
        {
            UriQueryBuilder builder = new UriQueryBuilder();
            builder.Add(Constants.QueryConstants.Component, "list");

            if (listingContext != null)
            {
                if (listingContext.Prefix != null)
                {
                    builder.Add("prefix", listingContext.Prefix);
                }

                if (listingContext.Marker != null)
                {
                    builder.Add("marker", listingContext.Marker);
                }

                if (listingContext.MaxResults.HasValue)
                {
                    builder.Add("maxresults", listingContext.MaxResults.ToString());
                }
            }

            if ((detailsIncluded & ShareListingDetails.Metadata) != 0)
            {
                builder.Add("include", "metadata");
            }

            HttpRequestMessage request = HttpRequestMessageFactory.CreateRequestMessage(HttpMethod.Get, uri, timeout, builder, content, operationContext);
            return request;
        }

        /// <summary>
        /// Gets the share Uri query builder.
        /// </summary>
        /// <returns>A <see cref="UriQueryBuilder"/> for the share.</returns>
        internal static UriQueryBuilder GetShareUriQueryBuilder()
        {
            UriQueryBuilder uriBuilder = new UriQueryBuilder();
            uriBuilder.Add(Constants.QueryConstants.ResourceType, "share");
            return uriBuilder;
        }
    }
}

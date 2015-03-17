﻿//-----------------------------------------------------------------------
// <copyright file="DeleteSnapshotsOption.cs" company="Microsoft">
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
//-----------------------------------------------------------------------

namespace Microsoft.WindowsAzure.Storage.Blob
{
    /// <summary>
    /// The set of options describing delete operation.
    /// </summary>
    public enum DeleteSnapshotsOption
    {
        /// <summary>
        /// Delete the blob only. If the blob has snapshots, this option will result in an error from the service.
        /// </summary>
        None,

        /// <summary>
        /// Delete the blob and its snapshots.
        /// </summary>
        IncludeSnapshots,

        /// <summary>
        /// Delete the blob's snapshots only.
        /// </summary>
        DeleteSnapshotsOnly
    }
}

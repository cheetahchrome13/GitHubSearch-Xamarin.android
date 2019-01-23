﻿// <auto-generated />
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Refit;
using GitHubSearch.Model;

/* ******** Hey You! *********
 *
 * This is a generated file, and gets rewritten every time you build the
 * project. If you want to edit it, you need to edit the mustache template
 * in the Refit package */

#pragma warning disable
namespace GitHubSearch.RefitInternalGenerated
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
    sealed class PreserveAttribute : Attribute
    {

        //
        // Fields
        //
        public bool AllMembers;

        public bool Conditional;
    }
}
#pragma warning restore

namespace GitHubSearch.Interface
{
    using GitHubSearch.RefitInternalGenerated;

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    [DebuggerNonUserCode]
    [Preserve]
    partial class AutoGeneratedIGitHubApi : IGitHubApi        {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedIGitHubApi(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        public virtual Task<ApiResponse> GetUser(string search)
        {
            var arguments = new object[] { search };
            var func = requestBuilder.BuildRestResultFuncForMethod("GetUser", new Type[] { typeof(string) });
            return (Task<ApiResponse>)func(Client, arguments);
        }

    }
}
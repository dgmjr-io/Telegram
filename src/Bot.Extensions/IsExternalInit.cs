#if NET5_0_OR_GREATER

[assembly: System.Runtime.CompilerServices.TypeForwardedTo(
    typeof(System.Runtime.CompilerServices.IsExternalInit)
)]

#else

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel;

namespace System.Runtime.CompilerServices;

// https://github.com/dotnet/runtime/blob/v8.0.0/src/libraries/System.Private.CoreLib/src/System/Runtime/CompilerServices/IsExternalInit.cs

/// <summary>
/// Reserved to be used by the compiler for tracking metadata.
/// This class should not be used by developers in source code.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
internal // polyfill!
static class IsExternalInit { }

#endif

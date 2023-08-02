using System;
using System.ComponentModel;

public record RandomChoiceObject<T>(T Value, double Chances);

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal class IsExternalInit { }
}
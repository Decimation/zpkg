// Read S zpkg PackageCommandAttribute.cs
// 2023-06-05 @ 6:23 PM

namespace zpkg;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class PackageCommandAttribute : Attribute { }
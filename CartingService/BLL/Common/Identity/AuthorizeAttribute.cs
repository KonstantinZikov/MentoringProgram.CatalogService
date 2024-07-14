namespace BLL.Common.Identity;

/// <summary>
/// Specifies the class this attribute is applied to requires authorization.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class. 
/// </remarks>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AuthorizeAttribute(string roles = "") : Attribute
{
    /// <summary>
    /// Gets or sets a comma delimited list of roles that are allowed to access the resource.
    /// </summary>
    public string Roles { get; set; } = roles;
}

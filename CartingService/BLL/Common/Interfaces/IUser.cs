﻿namespace BLL.Common.Interfaces;

public interface IUser
{
    string? Id { get; }

    IEnumerable<string>? Roles { get; }
}

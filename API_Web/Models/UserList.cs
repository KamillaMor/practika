using System;
using System.Collections.Generic;

namespace API_Web.Models;

public partial class UserList
{
    public int UserId { get; set; }

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Patronymic { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }
}

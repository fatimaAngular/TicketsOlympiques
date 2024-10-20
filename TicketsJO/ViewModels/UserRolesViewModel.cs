﻿public class UserRolesViewModel
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public List<string> Roles { get; set; }
}

public class ManageUserRolesViewModel
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public List<RoleViewModel> Roles { get; set; }
}

public class RoleViewModel
{
    public string RoleName { get; set; }
    public bool IsSelected { get; set; }
}

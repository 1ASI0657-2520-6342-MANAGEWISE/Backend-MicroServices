using AidManager.Iam.Domain.Entities;
using AidManager.Iam.Domain.Enums;

namespace AidManager.Iam.Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void Can_Create_User_With_Username_Password_And_Role()
    {
        // 1. Arrange
        var user = new User("testuser", "hashedpassword", UserRole.Manager);
        // 2. Assert
        Assert.Equal("testuser", user.Username);
        Assert.Equal("hashedpassword", user.PasswordHash);
        Assert.Equal(UserRole.Manager, user.Role);
    }
}

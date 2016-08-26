namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9c7df92e-1539-4557-8917-2de5c3854ab1', N'guest@vidly.com', 0, N'AMgYScUotGgLux0lAbgqQXz+LQccwPLN0OrItjWVtPa01PXBOcpZEiNRvtR0vgZfMw==', N'916f6b19-590d-43b5-8e09-139002ba29e3', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9d68b172-96a6-41bb-a06d-97cae13e8791', N'admin@vidly.com', 0, N'AMTZ/0feGwEU0czWjHWtsK+l0QhyLme+FWJGHLZfnWzjsVa+cVD36xj2e5tRbv7d4A==', N'63eb9d10-f21e-4938-8fd5-19bf83d5b904', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'5be2f189-6938-4de1-8281-369d85a9962a', N'CanManageMovies')
                
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9d68b172-96a6-41bb-a06d-97cae13e8791', N'5be2f189-6938-4de1-8281-369d85a9962a')
                ");
        }
        
        public override void Down()
        {
        }
    }
}

namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'31db041f-aa81-435a-8e89-a347b97fc212', N'guest@vidly.com', 0, N'AI4sQbca3LiHOVRZSH61CiM4IAdWFpETqqdIn8/TcZBO7GQrjfudcqC2ARttiLzOjQ==', N'22953d6c-7e79-465e-bdb0-32c598c86ab0', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5d1c27ce-cb87-46f9-a274-591806f68562', N'admin@vidly.com', 0, N'AFBInOt2qnkJxXThMhXPlhEZijtuhPvuqvEl0ibQR9vaHKY1EIKy9qZ8WnIo0XYaQA==', N'46b3f436-3eff-44f5-b5d1-50df3c5e8e97', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4dadac74-60c2-4da9-8809-d2410314d1bd', N'CanManageMovies')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5d1c27ce-cb87-46f9-a274-591806f68562', N'4dadac74-60c2-4da9-8809-d2410314d1bd')
");
        }
        
        public override void Down()
        {
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShoppingProject.DataAccess.Migrations
{
    public partial class AddSpDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_GetCoverTypes As Select * from CoverTypes");
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_GetCoverType @Id int As Select * from CoverTypes Where Id=@Id ");
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_CreateCoverType @Name varchar(50) As insert CoverTypes values(@Name)");
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_UpdateCoverType @Id int, @Name Varchar(50) As update CoverTypes set name=@Name where Id = @Id");
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_DeleteCoverType @Id int As delete from CoverTypes where Id = @Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

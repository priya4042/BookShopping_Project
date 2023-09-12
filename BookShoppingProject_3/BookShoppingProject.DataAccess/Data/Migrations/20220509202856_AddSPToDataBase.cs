using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShoppingProject.DataAccess.Migrations
{
    public partial class AddSPToDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_GetCoverTypes As Select * from CoverTypes");

            migrationBuilder.Sql(@"CREATE PROCEDURE SP_GetCoverType @Id int As Select * from CoverTypes where Id=@Id");

            migrationBuilder.Sql(@"CREATE PROCEDURE SP_CreateCoverType @Name varchar(50) As insert CoverTypes values(@Name)");

            migrationBuilder.Sql(@"CREATE PROCEDURE SP_UpdateCoverType
                                    @Id int,
                                         @Name varchar(50) 
                                      As
                                       Update CoverTypes set name=@name where Id=@Id");
                                           
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_DeleteCoverType @Id int As delete from CoverTypes where Id=@id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

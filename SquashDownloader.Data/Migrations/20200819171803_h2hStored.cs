using Microsoft.EntityFrameworkCore.Migrations;

namespace Squash.Data.Migrations
{
    public partial class h2hStored : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetH2H]
                    @Player1 int,
                    @Player2 int
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT 
	                   p1.Name
		                ,p2.Name
	                    ,p1.Id
	                    ,p2.Id
	                  , g.Player1Score
	                  , g.Player2Score
	                  ,t.Date
                  FROM [Squash].[dbo].[Matches]
                  inner join Games g
                  on Matches.Id = g.MatchId
                  inner join Tournaments t
                  on Matches.TournamentId = t.Id
                  inner join Players p1
                  on Matches.Player1Id = p1.Id
                  inner join Players p2
                  on Matches.Player2Id = p2.Id
                  where (Player1Id = @Player1 or Player2Id = @Player1) and (Player1Id=@Player2 or Player2Id=@Player2)
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

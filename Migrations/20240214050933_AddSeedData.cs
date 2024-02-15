using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        private const int SeedDataCount = 20;
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed data for Authors table
            for (int i = 1; i <= SeedDataCount; i++)
            {
                migrationBuilder.InsertData(
                    table: "Authors",
                    columns: new[] { "Name" },
                    values: new object[] { $"Author Name {i}" }
                );
            }

            
            // Seed data for LibraryBranches table
            for (int i = 1; i <= SeedDataCount; i++)
            {
                migrationBuilder.InsertData(
                    table: "LibraryBranches",
                    columns: new[] { "BranchName" },
                    values: new object[] { $"Branch Name {i}" }
                );
            }

            

            // Seed data for Customers table
            for (int i = 1; i <= SeedDataCount; i++)
            {
                migrationBuilder.InsertData(
                    table: "Customers",
                    columns: new[] { "Name" },
                    values: new object[] { $"Customer Name {i}" }
                );
            }
            

            // Seed data for Books table. Assuming foreign key constraints to Authors and LibraryBranches
            for (int i = 1; i <= SeedDataCount; i++)
            {
                migrationBuilder.InsertData(
                    table: "Books",
                    columns: new[] { "Title", "AuthorId", "LibraryBranchId" },
                    values: new object[] { $"Book Title {i}", i, i }
                );
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

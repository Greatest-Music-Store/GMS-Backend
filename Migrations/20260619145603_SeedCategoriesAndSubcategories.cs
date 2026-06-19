using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMS_Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategoriesAndSubcategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var cordasId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var percussaoId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var soprosId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var teclasId = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var audioId = Guid.Parse("55555555-5555-5555-5555-555555555555");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: ["Id", "Name"],
                values: new object[,]
                {
                    { cordasId, "Cordas" },
                    { percussaoId, "Percussão" },
                    { soprosId, "Sopros" },
                    { teclasId, "Teclas" },
                    { audioId, "Áudio" }
            });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: ["Id", "Name", "CategoryId"],
                values: new object[,]
                {
                    // Cordas
                    { Guid.Parse("10000000-0000-0000-0000-000000000001"), "Violão", cordasId },
                    { Guid.Parse("10000000-0000-0000-0000-000000000002"), "Guitarra", cordasId },
                    { Guid.Parse("10000000-0000-0000-0000-000000000003"), "Baixo", cordasId },
                    { Guid.Parse("10000000-0000-0000-0000-000000000004"), "Violino", cordasId },
                    { Guid.Parse("10000000-0000-0000-0000-000000000005"), "Viola", cordasId },
                    { Guid.Parse("10000000-0000-0000-0000-000000000006"), "Ukulele", cordasId },
                    { Guid.Parse("10000000-0000-0000-0000-000000000007"), "Harpa", cordasId },

                    // Percussão
                    { Guid.Parse("20000000-0000-0000-0000-000000000001"), "Bateria", percussaoId },
                    { Guid.Parse("20000000-0000-0000-0000-000000000002"), "Cajón", percussaoId },
                    { Guid.Parse("20000000-0000-0000-0000-000000000003"), "Pandeiro", percussaoId },
                    { Guid.Parse("20000000-0000-0000-0000-000000000004"), "Tamborim", percussaoId },
                    { Guid.Parse("20000000-0000-0000-0000-000000000005"), "Conga", percussaoId },
                    { Guid.Parse("20000000-0000-0000-0000-000000000006"), "Bongo", percussaoId },

                    // Sopros
                    { Guid.Parse("30000000-0000-0000-0000-000000000001"), "Saxofone", soprosId },
                    { Guid.Parse("30000000-0000-0000-0000-000000000002"), "Flauta", soprosId },
                    { Guid.Parse("30000000-0000-0000-0000-000000000003"), "Trompete", soprosId },
                    { Guid.Parse("30000000-0000-0000-0000-000000000004"), "Trombone", soprosId },
                    { Guid.Parse("30000000-0000-0000-0000-000000000005"), "Clarinete", soprosId },
                    { Guid.Parse("30000000-0000-0000-0000-000000000006"), "Oboé", soprosId },

                    // Teclas
                    { Guid.Parse("40000000-0000-0000-0000-000000000001"), "Piano", teclasId },
                    { Guid.Parse("40000000-0000-0000-0000-000000000002"), "Teclado", teclasId },
                    { Guid.Parse("40000000-0000-0000-0000-000000000003"), "Sintetizador", teclasId },
                    { Guid.Parse("40000000-0000-0000-0000-000000000004"), "Controlador MIDI", teclasId },

                    // Áudio
                    { Guid.Parse("50000000-0000-0000-0000-000000000001"), "Microfone", audioId },
                    { Guid.Parse("50000000-0000-0000-0000-000000000002"), "Interface de Áudio", audioId },
                    { Guid.Parse("50000000-0000-0000-0000-000000000003"), "Mesa de Som", audioId },
                    { Guid.Parse("50000000-0000-0000-0000-000000000004"), "Monitor de Áudio", audioId }
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

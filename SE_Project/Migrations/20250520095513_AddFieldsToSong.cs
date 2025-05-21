using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SE_Project.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToSong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Album",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Songs",
                newName: "ThumbnailUrl");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Songs",
                newName: "AudioUrl");

            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "Songs",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<int>(
                name: "PlayCount",
                table: "Songs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayCount",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "ThumbnailUrl",
                table: "Songs",
                newName: "ImagePath");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Songs",
                newName: "DateAdded");

            migrationBuilder.RenameColumn(
                name: "AudioUrl",
                table: "Songs",
                newName: "Genre");

            migrationBuilder.AddColumn<string>(
                name: "Album",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Songs",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

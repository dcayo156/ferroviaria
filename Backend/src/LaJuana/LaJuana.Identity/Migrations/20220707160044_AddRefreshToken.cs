using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaJuana.Identity.Migrations
{
    public partial class AddRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79ba8e3f-5c28-42cb-a03e-babcfb0b5bd8",
                column: "ConcurrencyStamp",
                value: "5f16f37b-4ea8-4328-8f4e-eb31efa1ba28");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c26c17c-ffe7-43ad-a3b3-b6d50ca71a63",
                column: "ConcurrencyStamp",
                value: "3ecc3056-b956-4248-aece-694c6822431f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "294d249b-9b57-48c1-9689-11a91abb6447",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "SecurityStamp" },
                values: new object[] { "adc84375-8bf5-428c-b97b-168236a5b0c8", "AQAAAAEAACcQAAAAEKuFBstP8xK0yY05tbC7W6XOYHo/7oys2yJ3a0GLzb18OWmvJF6w11aAMCxL24HvsQ==", "", "2497a053-175a-4351-b177-00e14b5d2023" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f284b3fd-f2cf-476e-a9b6-6560689cc48c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "SecurityStamp" },
                values: new object[] { "18fabeb7-c1cf-43c8-9ba8-d934fe464d6b", "AQAAAAEAACcQAAAAEE/Xa7tobZUujfOw2VO3kOkKdawU3b6ld9co+jGEc6nIBAYlQnSYNnnl+ykoIF/b3g==", "", "bc5c1aad-4fd9-401f-b5ea-3653d1773aff" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79ba8e3f-5c28-42cb-a03e-babcfb0b5bd8",
                column: "ConcurrencyStamp",
                value: "280c9d00-10c2-4a70-b48a-2807d51ccaaa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c26c17c-ffe7-43ad-a3b3-b6d50ca71a63",
                column: "ConcurrencyStamp",
                value: "a3decb52-950b-4804-9cf5-b1e303b001e5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "294d249b-9b57-48c1-9689-11a91abb6447",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d27a1cd0-ed3e-4ce4-91b4-19edc502473e", "AQAAAAEAACcQAAAAELS3xKf6JejNhUpdhufB9tfDGp92oErO9QB9KBYJ/X7hj25xNZikoypyQCHGLzC5MQ==", "1ad01628-a471-4c9c-8f6a-8311c98e7def" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f284b3fd-f2cf-476e-a9b6-6560689cc48c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b64ad15f-e9e2-41c6-b677-538057ee4acb", "AQAAAAEAACcQAAAAEBd70Ha7JNKTXhiLoVQr3F0xISMzdQsiu6N51Yf0sMNZt6SqFeQoXpM5uXDsBbQJYQ==", "2fb4cd94-41d2-41b4-87a5-a03eb78a7ac5" });
        }
    }
}

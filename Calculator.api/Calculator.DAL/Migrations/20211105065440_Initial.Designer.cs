﻿// <auto-generated />
using System;
using Calculator.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Calculator.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211105065440_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Calculator.DAL.Entity.Expression", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MathExpression")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Result")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Expressions");
                });
#pragma warning restore 612, 618
        }
    }
}
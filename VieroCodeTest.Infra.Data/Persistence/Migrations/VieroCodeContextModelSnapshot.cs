﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VieroCodeTest.Infra.Data.Persistence;

#nullable disable

namespace VieroCodeTest.Infra.Data.Persistence.Migrations
{
    [DbContext(typeof(VieroCodeContext))]
    partial class VieroCodeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VieroCodeTest.Infra.Data.Persistence.Models.Alumno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Alumnos");
                });

            modelBuilder.Entity("VieroCodeTest.Infra.Data.Persistence.Models.AlumnoGrado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlumnoId")
                        .HasColumnType("int");

                    b.Property<int>("GradoId")
                        .HasColumnType("int");

                    b.Property<string>("Seccion")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("AlumnoId");

                    b.HasIndex("GradoId");

                    b.ToTable("AlumnoGrados");
                });

            modelBuilder.Entity("VieroCodeTest.Infra.Data.Persistence.Models.Grado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ProfesorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfesorId");

                    b.ToTable("Grados");
                });

            modelBuilder.Entity("VieroCodeTest.Infra.Data.Persistence.Models.Profesor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Profesores");
                });

            modelBuilder.Entity("VieroCodeTest.Infra.Data.Persistence.Models.AlumnoGrado", b =>
                {
                    b.HasOne("VieroCodeTest.Infra.Data.Persistence.Models.Alumno", "Alumno")
                        .WithMany("AlumnoGrados")
                        .HasForeignKey("AlumnoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VieroCodeTest.Infra.Data.Persistence.Models.Grado", "Grado")
                        .WithMany("AlumnoGrados")
                        .HasForeignKey("GradoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alumno");

                    b.Navigation("Grado");
                });

            modelBuilder.Entity("VieroCodeTest.Infra.Data.Persistence.Models.Grado", b =>
                {
                    b.HasOne("VieroCodeTest.Infra.Data.Persistence.Models.Profesor", "Profesor")
                        .WithMany("Grados")
                        .HasForeignKey("ProfesorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("VieroCodeTest.Infra.Data.Persistence.Models.Alumno", b =>
                {
                    b.Navigation("AlumnoGrados");
                });

            modelBuilder.Entity("VieroCodeTest.Infra.Data.Persistence.Models.Grado", b =>
                {
                    b.Navigation("AlumnoGrados");
                });

            modelBuilder.Entity("VieroCodeTest.Infra.Data.Persistence.Models.Profesor", b =>
                {
                    b.Navigation("Grados");
                });
#pragma warning restore 612, 618
        }
    }
}
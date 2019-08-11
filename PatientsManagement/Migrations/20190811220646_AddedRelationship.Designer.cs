﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PatientsManagement.Storage;

namespace PatientsManagement.Migrations
{
    [DbContext(typeof(PatientsManagementContext))]
    [Migration("20190811220646_AddedRelationship")]
    partial class AddedRelationship
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PatientsManagement.Common.Models.AdditionalContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Kind");

                    b.Property<int>("PatientId");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(13);

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("AdditionalContact");
                });

            modelBuilder.Entity("PatientsManagement.Common.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("Patronymic")
                        .HasMaxLength(128);

                    b.Property<string>("PrimaryPhone")
                        .IsRequired()
                        .HasMaxLength(13);

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("PatientsManagement.Common.Models.AdditionalContact", b =>
                {
                    b.HasOne("PatientsManagement.Common.Models.Patient")
                        .WithMany("AdditionalContacts")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

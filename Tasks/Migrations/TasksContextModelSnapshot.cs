﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tasks.Persistence;

namespace Tasks.Migrations
{
    [DbContext(typeof(TasksContext))]
    partial class TasksContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Tasks.Persistence.Popug", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Role")
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<string>("UserName")
                        .HasColumnType("text")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_popug");

                    b.ToTable("popug");
                });

            modelBuilder.Entity("Tasks.Persistence.PopugTask", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<Guid?>("AssigneeId")
                        .HasColumnType("uuid")
                        .HasColumnName("assignee_id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.HasKey("Id")
                        .HasName("pk_tasks");

                    b.HasIndex("AssigneeId")
                        .HasDatabaseName("ix_tasks_assignee_id");

                    b.ToTable("tasks");
                });

            modelBuilder.Entity("Tasks.Persistence.PopugTask", b =>
                {
                    b.HasOne("Tasks.Persistence.Popug", "Assignee")
                        .WithMany()
                        .HasForeignKey("AssigneeId")
                        .HasConstraintName("fk_tasks_popug_assignee_id");

                    b.Navigation("Assignee");
                });
#pragma warning restore 612, 618
        }
    }
}
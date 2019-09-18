﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SimpleSocialNetwork.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int?>("OwnerId");

                    b.Property<int?>("PostId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SimpleSocialNetwork.Models.Friendship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("RequestFromUserId");

                    b.Property<int?>("RequestToUserId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("RequestFromUserId");

                    b.HasIndex("RequestToUserId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("SimpleSocialNetwork.Models.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("OwnerId");

                    b.Property<int?>("PostId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PostId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("SimpleSocialNetwork.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Text");

                    b.Property<int?>("UserFromId");

                    b.Property<int?>("UserToId");

                    b.HasKey("Id");

                    b.HasIndex("UserFromId");

                    b.HasIndex("UserToId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("SimpleSocialNetwork.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image");

                    b.Property<int?>("PostId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("SimpleSocialNetwork.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int?>("OwnerId");

                    b.Property<string>("Text");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("SimpleSocialNetwork.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<DateTime?>("BirthDay");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Email");

                    b.Property<string>("JobPlace");

                    b.Property<string>("JobPosition");

                    b.Property<string>("MobiePhone");

                    b.Property<string>("Name");

                    b.Property<string>("School");

                    b.Property<string>("Surname");

                    b.Property<string>("University");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SimpleSocialNetwork.Models.Comment", b =>
                {
                    b.HasOne("SimpleSocialNetwork.Models.User", "Owner")
                        .WithMany("Comments")
                        .HasForeignKey("OwnerId");

                    b.HasOne("SimpleSocialNetwork.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("SimpleSocialNetwork.Models.Friendship", b =>
                {
                    b.HasOne("SimpleSocialNetwork.Models.User", "RequestFrom")
                        .WithMany("OutgoingFrienshipRequests")
                        .HasForeignKey("RequestFromUserId");

                    b.HasOne("SimpleSocialNetwork.Models.User", "RequestTo")
                        .WithMany("IncomingFrienshipRequests")
                        .HasForeignKey("RequestToUserId");
                });

            modelBuilder.Entity("SimpleSocialNetwork.Models.Like", b =>
                {
                    b.HasOne("SimpleSocialNetwork.Models.User", "Owner")
                        .WithMany("Likes")
                        .HasForeignKey("OwnerId");

                    b.HasOne("SimpleSocialNetwork.Models.Post", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("SimpleSocialNetwork.Models.Message", b =>
                {
                    b.HasOne("SimpleSocialNetwork.Models.User", "UserFrom")
                        .WithMany("MessageFrom")
                        .HasForeignKey("UserFromId");

                    b.HasOne("SimpleSocialNetwork.Models.User", "UserTo")
                        .WithMany("MessageTo")
                        .HasForeignKey("UserToId");
                });

            modelBuilder.Entity("SimpleSocialNetwork.Models.Photo", b =>
                {
                    b.HasOne("SimpleSocialNetwork.Models.Post", "Post")
                        .WithMany("Photos")
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("SimpleSocialNetwork.Models.Post", b =>
                {
                    b.HasOne("SimpleSocialNetwork.Models.User", "Owner")
                        .WithMany("Posts")
                        .HasForeignKey("OwnerId");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using CallsRegistry.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CallsRegistry.Migrations
{
    [DbContext(typeof(CallsRegistryContext))]
    partial class CallsRegistryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CallsRegistry.Model.Call", b =>
                {
                    b.Property<string>("MSISDN");

                    b.Property<DateTimeOffset>("Date");

                    b.Property<int>("Duration");

                    b.HasKey("MSISDN", "Date");

                    b.ToTable("PhoneCalls");
                });

            modelBuilder.Entity("CallsRegistry.Model.SmsMessage", b =>
                {
                    b.Property<string>("MSISDN");

                    b.Property<DateTimeOffset>("Date");

                    b.HasKey("MSISDN", "Date");

                    b.ToTable("SmsMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
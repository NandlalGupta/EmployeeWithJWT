using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWithJWT.Models;

public partial class EmployeeWithJwtContext : DbContext
{
    public EmployeeWithJwtContext()
    {
    }

    public EmployeeWithJwtContext(DbContextOptions<EmployeeWithJwtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<State> States { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__Countrie__10D160BF1D671781");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CountryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD18C8D2CB");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1FDEBE5D2");

            entity.HasIndex(e => e.Username, "UQ__Employee__536C85E45F5C765C").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Employee__A9D105340FC0D8EA").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.StateId).HasColumnName("StateID");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Employees__Depar__6754599E");

            entity.HasOne(d => d.State).WithMany(p => p.Employees)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK__Employees__State__66603565");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.SalaryId).HasName("PK__Salaries__4BE204B705881D20");

            entity.Property(e => e.SalaryId).HasColumnName("SalaryID");
            entity.Property(e => e.Allowance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BasicSalary).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Deduction).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Hra)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("HRA");

            entity.HasOne(d => d.Employee).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Salaries__Employ__6A30C649");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK__States__C3BA3B5ACEF85A9E");

            entity.Property(e => e.StateId).HasColumnName("StateID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.StateName).HasMaxLength(100);

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__States__CountryI__5EBF139D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

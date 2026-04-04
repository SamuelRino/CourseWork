using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Models;

public partial class VendingDbContext : DbContext
{
    public VendingDbContext()
    {
    }

    public VendingDbContext(DbContextOptions<VendingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DictEmployeeRole> DictEmployeeRoles { get; set; }

    public virtual DbSet<DictMachineStatus> DictMachineStatuses { get; set; }

    public virtual DbSet<DictPaymentMethod> DictPaymentMethods { get; set; }

    public virtual DbSet<DictProductCategory> DictProductCategories { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<MachineStock> MachineStocks { get; set; }

    public virtual DbSet<MaintenanceLog> MaintenanceLogs { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<RestockLog> RestockLogs { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<VendingMachine> VendingMachines { get; set; }

    public virtual DbSet<VwEmployeeActivity> VwEmployeeActivities { get; set; }

    public virtual DbSet<VwLowStockAlert> VwLowStockAlerts { get; set; }

    public virtual DbSet<VwRevenueByLocation> VwRevenueByLocations { get; set; }

    public virtual DbSet<VwSalesByMachine> VwSalesByMachines { get; set; }

    public virtual DbSet<VwStockFullInfo> VwStockFullInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=localhost\\sqlexpress; database=VendingDB; user=ИСП-31; password=1234567890; encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DictEmployeeRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_EmployeesRoles");

            entity.ToTable("dictEmployeeRoles");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<DictMachineStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK_MachineStatuses");

            entity.ToTable("dictMachineStatuses");

            entity.Property(e => e.StatusId)
                .ValueGeneratedNever()
                .HasColumnName("StatusID");
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<DictPaymentMethod>(entity =>
        {
            entity.HasKey(e => e.MethodId).HasName("PK_PaymentMethods");

            entity.ToTable("dictPaymentMethods");

            entity.Property(e => e.MethodId)
                .ValueGeneratedNever()
                .HasColumnName("MethodID");
            entity.Property(e => e.MethodName).HasMaxLength(50);
        });

        modelBuilder.Entity<DictProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_ProductCategories");

            entity.ToTable("dictProductCategories");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedNever()
                .HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1925BBBF3");

            entity.HasIndex(e => e.Login, "UQ__Employee__5E55825B24E53379").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Roles");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__E7FEA477174E2536");

            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.BuildingName).HasMaxLength(100);
        });

        modelBuilder.Entity<MachineStock>(entity =>
        {
            entity.HasKey(e => e.StockId).HasName("PK__MachineS__2C83A9E2EB639ACC");

            entity.ToTable("MachineStock");

            entity.HasIndex(e => new { e.MachineId, e.ProductId }, "UQ_Machine_Product").IsUnique();

            entity.Property(e => e.StockId).HasColumnName("StockID");
            entity.Property(e => e.MachineId).HasColumnName("MachineID");
            entity.Property(e => e.MinLevel).HasDefaultValue(5);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Machine).WithMany(p => p.MachineStocks)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__MachineSt__Machi__70DDC3D8");

            entity.HasOne(d => d.Product).WithMany(p => p.MachineStocks)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__MachineSt__Produ__4316F928");
        });

        modelBuilder.Entity<MaintenanceLog>(entity =>
        {
            entity.HasKey(e => e.MaintenanceId).HasName("PK__Maintena__E60542B52486865B");

            entity.Property(e => e.MaintenanceId).HasColumnName("MaintenanceID");
            entity.Property(e => e.Cost)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.MachineId).HasColumnName("MachineID");
            entity.Property(e => e.MaintenanceDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.MaintenanceLogs)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Maintenan__Emplo__72C60C4A");

            entity.HasOne(d => d.Machine).WithMany(p => p.MaintenanceLogs)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__Maintenan__Machi__73BA3083");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6EDDA761364");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.BasePrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");
        });

        modelBuilder.Entity<RestockLog>(entity =>
        {
            entity.HasKey(e => e.RestockId).HasName("PK__RestockL__93A9C5A3AD7D1B7C");

            entity.Property(e => e.RestockId).HasColumnName("RestockID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.MachineId).HasColumnName("MachineID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.RestockDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.RestockLogs)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__RestockLo__Emplo__75A278F5");

            entity.HasOne(d => d.Machine).WithMany(p => p.RestockLogs)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__RestockLo__Machi__76969D2E");

            entity.HasOne(d => d.Product).WithMany(p => p.RestockLogs)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__RestockLo__Produ__52593CB8");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Sales__1EE3C41F9D2F3164");

            entity.HasIndex(e => e.MachineId, "IX_Sales_MID");

            entity.HasIndex(e => e.ProductId, "IX_Sales_PID");

            entity.Property(e => e.SaleId).HasColumnName("SaleID");
            entity.Property(e => e.MachineId).HasColumnName("MachineID");
            entity.Property(e => e.MethodId).HasColumnName("MethodID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.SaleDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SalePrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Machine).WithMany(p => p.Sales)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__Sales__MachineID__4BAC3F29");

            entity.HasOne(d => d.Method).WithMany(p => p.Sales)
                .HasForeignKey(d => d.MethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_PaymentMethods");

            entity.HasOne(d => d.Product).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Sales__ProductID__4CA06362");
        });

        modelBuilder.Entity<VendingMachine>(entity =>
        {
            entity.HasKey(e => e.MachineId).HasName("PK__VendingM__44EE5B58383327A8");

            entity.HasIndex(e => e.SerialNumber, "UQ__VendingM__048A00081C1C4785").IsUnique();

            entity.Property(e => e.MachineId).HasColumnName("MachineID");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Model).HasMaxLength(100);
            entity.Property(e => e.SerialNumber).HasMaxLength(50);
            entity.Property(e => e.StatusId)
                .HasDefaultValue(1)
                .HasColumnName("StatusID");

            entity.HasOne(d => d.Location).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__VendingMa__Locat__7B5B524B");

            entity.HasOne(d => d.Status).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachines_Statuses");
        });

        modelBuilder.Entity<VwEmployeeActivity>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_EmployeeActivity");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<VwLowStockAlert>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_LowStockAlert");

            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.SerialNumber).HasMaxLength(50);
        });

        modelBuilder.Entity<VwRevenueByLocation>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_RevenueByLocation");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.BuildingName).HasMaxLength(100);
            entity.Property(e => e.TotalRevenue).HasColumnType("decimal(38, 2)");
        });

        modelBuilder.Entity<VwSalesByMachine>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_SalesByMachine");

            entity.Property(e => e.AvgPrice).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.Model).HasMaxLength(100);
            entity.Property(e => e.SerialNumber).HasMaxLength(50);
            entity.Property(e => e.TotalRevenue).HasColumnType("decimal(38, 2)");
        });

        modelBuilder.Entity<VwStockFullInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_StockFullInfo");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.SerialNumber).HasMaxLength(50);
            entity.Property(e => e.StockStatus)
                .HasMaxLength(12)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

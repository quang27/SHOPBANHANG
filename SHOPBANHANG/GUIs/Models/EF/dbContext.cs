using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace GUIs.Models.EF
{
    public partial class dbContext : DbContext
    {
        public dbContext()
            : base("name=dbContext")
        {
        }

        public virtual DbSet<CHITIETHOADON> CHITIETHOADON { get; set; }
        public virtual DbSet<HOADON> HOADON { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANG { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIEN { get; set; }
        public virtual DbSet<QUANLY> QUANLY { get; set; }
        public virtual DbSet<SANPHAM> SANPHAM { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HOADON>()
                .Property(e => e.telephone)
                .IsFixedLength();

            modelBuilder.Entity<HOADON>()
                .HasMany(e => e.CHITIETHOADON)
                .WithOptional(e => e.HOADON)
                .HasForeignKey(e => e.idhd);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.telephone)
                .IsFixedLength();

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .HasMany(e => e.HOADON)
                .WithOptional(e => e.KHACHHANG)
                .HasForeignKey(e => e.idkh);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.telephone)
                .IsFixedLength();

            modelBuilder.Entity<NHANVIEN>()
                .HasMany(e => e.HOADON)
                .WithOptional(e => e.NHANVIEN)
                .HasForeignKey(e => e.idnv);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.origin)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.CHITIETHOADON)
                .WithOptional(e => e.SANPHAM)
                .HasForeignKey(e => e.idsp);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PagosVisaWeb.Models
{
    public partial class ElectrosurContext : DbContext
    {
        public ElectrosurContext()
        {
        }

        public ElectrosurContext(DbContextOptions<ElectrosurContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PdpOpepOperacion> PdpOpepOperacion { get; set; }
        public virtual DbSet<PdpPagpPago> PdpPagpPago { get; set; }
        public virtual DbSet<PdpUsrtUsuarioDelSistema> PdpUsrtUsuarioDelSistema { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //}
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PdpOpepOperacion>(entity =>
            {
                entity.HasKey(e => e.OpeidOperacion);

                entity.ToTable("pdpOPEpOperacion");

                entity.Property(e => e.OpeidOperacion).HasColumnName("OPEid_operacion");

                entity.Property(e => e.Opecreado)
                    .HasColumnName("OPEcreado")
                    .HasColumnType("datetime");

                entity.Property(e => e.Opemonto)
                    .HasColumnName("OPEmonto")
                    .HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Opesuministro).HasColumnName("OPEsuministro");

                entity.Property(e => e.UsridUsuario).HasColumnName("USRid_usuario");

                entity.HasOne(d => d.UsridUsuarioNavigation)
                    .WithMany(p => p.PdpOpepOperacion)
                    .HasForeignKey(d => d.UsridUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pdpOPEpOperacion_pdpUSRtUsuarioDelSistema");
            });

            modelBuilder.Entity<PdpPagpPago>(entity =>
            {
                entity.HasKey(e => e.PagidPago);

                entity.ToTable("pdpPAGpPago");

                entity.Property(e => e.PagidPago).HasColumnName("PAGid_pago");

                entity.Property(e => e.PAGRespuestaSielse).HasColumnName("PAGrespuesta_sielse");

                entity.Property(e => e.OpeidOperacion).HasColumnName("OPEid_operacion");

                entity.Property(e => e.Opemonto)
                    .HasColumnName("OPEmonto")
                    .HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Opesuministro).HasColumnName("OPEsuministro");

                entity.Property(e => e.Pagbrand)
                    .IsRequired()
                    .HasColumnName("PAGbrand")
                    .HasMaxLength(50);

                entity.Property(e => e.Pagcard)
                    .IsRequired()
                    .HasColumnName("PAGcard")
                    .HasMaxLength(50);

                entity.Property(e => e.PagcodigoCliente)
                    .IsRequired()
                    .HasColumnName("PAGcodigo_cliente")
                    .HasMaxLength(50);

                entity.Property(e => e.PagcodigoComprobante)
                    .IsRequired()
                    .HasColumnName("PAGcodigo_comprobante")
                    .HasMaxLength(50);

                entity.Property(e => e.Pagcreado)
                    .HasColumnName("PAGcreado")
                    .HasColumnType("datetime");

                entity.Property(e => e.Pagdescription)
                    .IsRequired()
                    .HasColumnName("PAGdescription")
                    .HasColumnType("ntext");

                entity.Property(e => e.Pagestado)
                    .IsRequired()
                    .HasColumnName("PAGestado")
                    .HasMaxLength(10);

                entity.Property(e => e.PagmetodoPago).HasColumnName("PAGmetodo_pago");

                entity.Property(e => e.UsridUsuario).HasColumnName("USRid_usuario");

                entity.HasOne(d => d.OpeidOperacionNavigation)
                    .WithMany(p => p.PdpPagpPago)
                    .HasForeignKey(d => d.OpeidOperacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pdpPAGpPago_pdpOPEpOperacion");

                entity.HasOne(d => d.UsridUsuarioNavigation)
                    .WithMany(p => p.PdpPagpPago)
                    .HasForeignKey(d => d.UsridUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pdpPAGpPago_pdpUSRtUsuarioDelSistema");
            });

            modelBuilder.Entity<PdpUsrtUsuarioDelSistema>(entity =>
            {
                entity.HasKey(e => e.UsridUsuario);

                entity.ToTable("pdpUSRtUsuarioDelSistema");

                entity.Property(e => e.UsridUsuario).HasColumnName("USRid_usuario");

                entity.Property(e => e.UsrapellidoMaterno)
                    .IsRequired()
                    .HasColumnName("USRapellido_materno")
                    .HasMaxLength(100);

                entity.Property(e => e.UsrapellidoPaterno)
                    .IsRequired()
                    .HasColumnName("USRapellido_paterno")
                    .HasMaxLength(100);

                entity.Property(e => e.UsrconfirmacionCorreo).HasColumnName("USRconfirmacion_correo");

                entity.Property(e => e.Usrcontrasena)
                    .IsRequired()
                    .HasColumnName("USRcontrasena")
                    .HasMaxLength(50);

                entity.Property(e => e.UsrcorreoPrimario)
                    .IsRequired()
                    .HasColumnName("USRcorreo_primario")
                    .HasMaxLength(100);

                entity.Property(e => e.UsrcorreoSecundario)
                    .IsRequired()
                    .HasColumnName("USRcorreo_secundario")
                    .HasMaxLength(100);

                entity.Property(e => e.Usrcreado)
                    .HasColumnName("USRcreado")
                    .HasColumnType("datetime");

                entity.Property(e => e.Usrestado).HasColumnName("USRestado");

                entity.Property(e => e.Usrmodificado)
                    .HasColumnName("USRmodificado")
                    .HasColumnType("datetime");

                entity.Property(e => e.Usrnombre)
                    .IsRequired()
                    .HasColumnName("USRnombre")
                    .HasMaxLength(100);

                entity.Property(e => e.UsrnumeroDocumento)
                    .IsRequired()
                    .HasColumnName("USRnumero_documento")
                    .HasMaxLength(12);

                entity.Property(e => e.UsrrecuperarContrasena).HasColumnName("USRrecuperar_contrasena");

                entity.Property(e => e.Usrtelefono)
                    .IsRequired()
                    .HasColumnName("USRtelefono")
                    .HasMaxLength(12);

                entity.Property(e => e.UsrtipoDocumento)
                    .IsRequired()
                    .HasColumnName("USRtipo_documento")
                    .HasMaxLength(18);

                entity.Property(e => e.UsrultimoAcceso)
                    .HasColumnName("USRultimo_acceso")
                    .HasColumnType("datetime");

                entity.Property(e => e.UsruniqueId).HasColumnName("USRunique_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

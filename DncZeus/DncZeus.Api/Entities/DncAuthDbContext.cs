/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * 版权所有，请勿删除
 ******************************************/

using DncZeus.Api.Entities.QueryModels.DncPermission;
using Microsoft.EntityFrameworkCore;

namespace DncZeus.Api.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class DncZeusDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public DncZeusDbContext(DbContextOptions<DncZeusDbContext> options) : base(options)
        {

        }
        /// <summary>
        /// 数据库
        /// </summary>
        public virtual DbSet<Data> Data { get; set; }
        /// <summary>
        /// 报表视图
        /// </summary>
        public virtual DbSet<MeterView> MeterView { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<AfterView> AfterView { get; set; }


        public virtual DbSet<BaseTable> BaseTable { get; set; }
        public virtual DbSet<DeviceInformation> DeviceInformation { get; set; }

        public virtual DbSet<InstallationPosition> InstallationPosition { get; set; }
        public virtual DbSet<EquipmentInstallation> EquipmentInstallation { get; set; }

        public virtual DbSet<Ecutable> Ecutable { get; set; }
        public virtual DbSet<UptownView> UptownView { get; set; }

        public virtual DbSet<StatisticsReport> StatisticsReport { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public virtual DbSet<AfterStaff> AfterStaff { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<DncUser> DncUser { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<DncRole> DncRole { get; set; }
        /// <summary>
        /// 菜单
        /// </summary>
        public DbSet<DncMenu> DncMenu { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public DbSet<DncIcon> DncIcon { get; set; }
        /// <summary>
        /// 测试
        /// </summary>
        public DbSet<DncCeshi> DncCeshi { get; set; }
        /// <summary>
        /// 任务清单
        /// </summary>
        public DbSet<DncTaskList> DncTaskList { get; set; }
        /// <summary>
        /// 工作任务
        /// </summary>
        public DbSet<DncWorkTask> DncWorkTask { get; set; }
        /// <summary>
        /// 日期表
        /// </summary>
        public DbSet<DataList> DataList { get; set; }

        /// <summary>
        /// 用户-角色多对多映射
        /// </summary>
        public DbSet<DncUserRoleMapping> DncUserRoleMapping { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public DbSet<DncPermission> DncPermission { get; set; }
        /// <summary>
        /// 角色-权限多对多映射
        /// </summary>
        public DbSet<DncRolePermissionMapping> DncRolePermissionMapping { get; set; }

        #region DbQuery
        /// <summary>
        /// 
        /// </summary>
        public DbQuery<DncPermissionWithAssignProperty> DncPermissionWithAssignProperty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbQuery<DncPermissionWithMenu> DncPermissionWithMenu { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<DncUser>()
            //    .Property(x => x.Status);
            //modelBuilder.Entity<DncUser>()
            //    .Property(x => x.IsDeleted);


            modelBuilder.Entity<DncRole>(entity =>
            {
                entity.HasIndex(x => x.Code).IsUnique();
            });

            modelBuilder.Entity<DncMenu>(entity =>
            {
                //entity.haso
            });


            modelBuilder.Entity<DncUserRoleMapping>(entity =>
            {
                entity.HasKey(x => new
                {
                    x.UserGuid,
                    x.RoleCode
                });

                entity.HasOne(x => x.DncUser)
                    .WithMany(x => x.UserRoles)
                    .HasForeignKey(x => x.UserGuid)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.DncRole)
                    .WithMany(x => x.UserRoles)
                    .HasForeignKey(x => x.RoleCode)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DncPermission>(entity =>
            {
                entity.HasIndex(x => x.Code)
                    .IsUnique();

                entity.HasOne(x => x.Menu)
                    .WithMany(x => x.Permissions)
                    .HasForeignKey(x => x.MenuGuid);
            });

            modelBuilder.Entity<DncRolePermissionMapping>(entity =>
            {
                entity.HasKey(x => new
                {
                    x.RoleCode,
                    x.PermissionCode
                });

                entity.HasOne(x => x.DncRole)
                    .WithMany(x => x.Permissions)
                    .HasForeignKey(x => x.RoleCode)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.DncPermission)
                    .WithMany(x => x.Roles)
                    .HasForeignKey(x => x.PermissionCode)
                    .OnDelete(DeleteBehavior.Restrict);
            });

       
            modelBuilder.Entity<Data>(entity =>
            {
                entity.ToTable("data");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.Cmptlevel).HasColumnName("cmptlevel");

                entity.Property(e => e.Crdate)
                    .HasColumnName("crdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dbid).HasColumnName("dbid");

                entity.Property(e => e.Filename)
                    .HasColumnName("filename")
                    .HasMaxLength(260);

                entity.Property(e => e.Mode).HasColumnName("mode");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(128);

                entity.Property(e => e.Reserved)
                    .HasColumnName("reserved")
                    .HasColumnType("datetime");

                entity.Property(e => e.Sid)
                    .HasColumnName("sid")
                    .HasMaxLength(85);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Status2).HasColumnName("status2");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<MeterView>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.AreaName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BuildId).HasColumnName("BuildID");

                entity.Property(e => e.BuildName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Byrq)
                    .HasColumnName("BYRQ")
                    .HasColumnType("datetime");

                entity.Property(e => e.Bys)
                    .HasColumnName("BYS")
                    .HasColumnType("numeric(12, 2)");

                entity.Property(e => e.Cbwc).HasColumnName("CBWC");

                entity.Property(e => e.DataName).HasMaxLength(50);

                entity.Property(e => e.Lrrq)
                    .HasColumnName("LRRQ")
                    .HasColumnType("datetime");

                entity.Property(e => e.MeterAddr)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.MeterId).HasColumnName("MeterID");

                entity.Property(e => e.RoomNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SaleRoomName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Syrq)
                    .HasColumnName("SYRQ")
                    .HasColumnType("datetime");

                entity.Property(e => e.Sys)
                    .HasColumnName("SYS")
                    .HasColumnType("numeric(12, 2)");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.UnitName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UptownId).HasColumnName("UptownID");

                entity.Property(e => e.UptownName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserAddr)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AfterView>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cbwc0count).HasColumnName("CBWC0Count");

                entity.Property(e => e.Cbwc1count).HasColumnName("CBWC1Count");

                entity.Property(e => e.Cbwc2count).HasColumnName("CBWC2Count");

                entity.Property(e => e.Cbwc3count).HasColumnName("CBWC3Count");

                entity.Property(e => e.Cbwc4count).HasColumnName("CBWC4Count");

                entity.Property(e => e.Cbwc5number).HasColumnName("CBWC5Number");

                entity.Property(e => e.DataName).HasMaxLength(50);

                entity.Property(e => e.FiveDs).HasColumnName("FiveDS");

                entity.Property(e => e.FourDs).HasColumnName("FourDS");

                entity.Property(e => e.OneDs).HasColumnName("OneDS");

                entity.Property(e => e.OneMp)
                    .HasColumnName("OneMP")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OneMs).HasColumnName("OneMS");

                entity.Property(e => e.OneWs).HasColumnName("OneWS");

                entity.Property(e => e.SaleRoomName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SevenDp)
                    .HasColumnName("SevenDP")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SevenDs).HasColumnName("SevenDS");

                entity.Property(e => e.SixDs).HasColumnName("SixDS");

                entity.Property(e => e.ThreeDs).HasColumnName("ThreeDS");

                entity.Property(e => e.ThreeMp)
                    .HasColumnName("ThreeMP")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ThreeWs).HasColumnName("ThreeWS");

                entity.Property(e => e.ToDs).HasColumnName("ToDS");

                entity.Property(e => e.TwoDs).HasColumnName("TwoDS");

                entity.Property(e => e.TwoMp)
                    .HasColumnName("TwoMP")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TwoMs).HasColumnName("TwoMS");

                entity.Property(e => e.TwoWs).HasColumnName("TwoWS");
            });

            modelBuilder.Entity<BaseTable>(entity =>
            {
                entity.HasKey(e => e.Btid);

                entity.Property(e => e.Btid)
                    .HasColumnName("BTID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BatchNumber)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BatchCount)
                   .HasColumnName("BatchCount")
                   .ValueGeneratedNever();

                entity.Property(e => e.CommonFlowRatio)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CommonTraffic)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DateOfManufacture)
                    .HasColumnName("dateOfManufacture")
                   .HasColumnType("datetime");

                entity.Property(e => e.ExecutiveStandard)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InitialFlow)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InstallationMethod)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MaximumDegree)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MaximumOperatingTemperature)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MaximumWorkingPressure)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MinimumReading)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ParameterSpecification)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Remarks)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SteelSealNumberRange)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.WorkingEnvironmentHumidity)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.WorkingEnvironmentTemperature)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DeviceInformation>(entity =>
            {
                entity.HasKey(e => e.Diid);

                entity.Property(e => e.Diid)
                    .HasColumnName("DIID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BatchNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Btid).HasColumnName("BTID")
                 .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DateOfManufacture)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ecuid).HasColumnName("ECUID").IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.ElectronicUnitNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Eunumber)
                    .IsRequired()
                    .HasColumnName("EUNumber")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProductModel)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Remarks)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Ecutable>(entity =>
            {
              
                entity.HasKey(e => e.Ecuid);

                entity.ToTable("ECUTable");

                entity.Property(e => e.Ecuid)
                    .HasColumnName("ECUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AverageWorkingCurrent)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BatteryModel)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CommunicationMode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

               
                entity.Property(e => e.DateOfManufacture)
                   .HasColumnName("dateOfManufacture")
                   .HasColumnType("datetime");

                entity.Property(e => e.ElectronicUnitNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Imei)
                    .IsRequired()
                    .HasColumnName("IMEI")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Imsi)
                    .IsRequired()
                    .HasColumnName("IMSI")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MagneticInterferenceStatus)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProductModel)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProgramVersion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProtectionLevel)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Remarks)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SignalIntensity)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Voltage)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.VoltageState)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<InstallationPosition>(entity =>
            {
                entity.HasKey(e => e.Ipid);

                entity.Property(e => e.Ipid)
                    .HasColumnName("IPID")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AreaId)
                    .HasColumnName("AreaID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AreaName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BuildId)
                    .HasColumnName("BuildID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BuildName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UnitId)
                    .HasColumnName("UnitID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UnitName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UptownAddr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UptownId)
                    .HasColumnName("UptownID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UptownName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PayNumber)
                   .HasMaxLength(20)
                   .IsUnicode(false)
                   .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentInstallation>(entity =>
            {
                entity.HasKey(e => e.Eiid);

                entity.Property(e => e.Eiid)
                    .HasColumnName("EIID")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Diid)
                    .IsRequired()
                    .HasColumnName("DIID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Eunumber)
                    .IsRequired()
                    .HasColumnName("EUNumber")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ipid)
                    .IsRequired()
                    .HasColumnName("IPID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PayNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<UptownView>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cbwc0count).HasColumnName("CBWC0Count");

                entity.Property(e => e.Cbwc1count).HasColumnName("CBWC1Count");

                entity.Property(e => e.Cbwc2count).HasColumnName("CBWC2Count");

                entity.Property(e => e.Cbwc3count).HasColumnName("CBWC3Count");

                entity.Property(e => e.Cbwc4count).HasColumnName("CBWC4Count");

                entity.Property(e => e.Cbwc5number).HasColumnName("CBWC5Number");

                entity.Property(e => e.DataName).HasMaxLength(50);

                entity.Property(e => e.FiveDs).HasColumnName("FiveDS");

                entity.Property(e => e.FourDs).HasColumnName("FourDS");

                entity.Property(e => e.OneDs).HasColumnName("OneDS");

                entity.Property(e => e.OneMp)
                    .HasColumnName("OneMP")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OneMs).HasColumnName("OneMS");

                entity.Property(e => e.OneWs).HasColumnName("OneWS");

                entity.Property(e => e.SaleRoomName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SevenDp)
                    .HasColumnName("SevenDP")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SevenDs).HasColumnName("SevenDS");

                entity.Property(e => e.SixDs).HasColumnName("SixDS");

                entity.Property(e => e.ThreeDs).HasColumnName("ThreeDS");

                entity.Property(e => e.ThreeMp)
                    .HasColumnName("ThreeMP")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ThreeWs).HasColumnName("ThreeWS");

                entity.Property(e => e.ToDs).HasColumnName("ToDS");

                entity.Property(e => e.TwoDs).HasColumnName("TwoDS");

                entity.Property(e => e.TwoMp)
                    .HasColumnName("TwoMP")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TwoMs).HasColumnName("TwoMS");

                entity.Property(e => e.TwoWs).HasColumnName("TwoWS");

                entity.Property(e => e.Uptownname)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<StatisticsReport>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Asof0).HasColumnName("Asof_0");
            });





            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSignalR.Server
{
	public class DB : DbContext
	{

		public DbSet<Shared.Proba> Probas { get; set; }

		public DbSet<OtMA> OtMAs { get; set; }
		public DbSet<OtMB> OtMBs { get; set; }

		public DbSet<OtOA> OtOAs { get; set; }
		public DbSet<OtOB> OtOBs { get; set; }

		public DbSet<MtMA> MtMAs { get; set; }
		public DbSet<MtMB> MtMBs { get; set; }
		public DbSet<MtMA_B> MtMA_Bs { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-75VO5EN\TESTSERVER;Initial Catalog=TestBaza;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Shared.Proba>().HasKey(p => p.ID);

			modelBuilder.Entity<OtMA>().HasKey(a => a.ID);
			modelBuilder.Entity<OtMB>().HasKey(b => b.ID);

			modelBuilder.Entity<OtOA>().HasKey(a => a.ID);
			modelBuilder.Entity<OtOB>().HasKey(b => b.ID);

			modelBuilder.Entity<MtMA>().HasKey(a => a.ID);
			modelBuilder.Entity<MtMB>().HasKey(b => b.ID);
			modelBuilder.Entity<MtMA_B>().HasKey(ab 
				=> new {ab.IdA, ab.IdB });

			modelBuilder.Entity<OtMA>().HasMany(a => a.lista)
										.WithOne(b => b.A);

			modelBuilder.Entity<OtOA>().HasOne(a => a.B)
										.WithOne(b => b.A)
										.HasForeignKey<OtOB>(b => b.IDA);

			modelBuilder.Entity<MtMA_B>().HasOne(ab => ab.A)
										.WithMany(a => a.listaAB)
										.HasForeignKey(ab => ab.IdA);

			modelBuilder.Entity<MtMA_B>().HasOne(ab => ab.B)
										.WithMany(b => b.listaAB)
										.HasForeignKey(ab => ab.IdB);
		}
	}

	public class OtMA
	{
		public int ID { get; set; }
		public List<OtMB> lista { get; set; } = new List<OtMB>();
	}
	public class OtMB
	{
		public int ID { get; set; }
		public OtMA A { get; set; }
	}

	public class OtOA
	{
		public int ID { get; set; }
		public OtOB B { get; set; } 
	}
	public class OtOB
	{
		public int ID { get; set; }
		public OtOA A { get; set; }
		public int IDA { get; set; }
	}

	public class MtMA
	{
		public int ID { get; set; }
		public List<MtMA_B> listaAB { get; set; } = new List<MtMA_B>();
	}

	public class MtMB
	{
		public int ID { get; set; }
		public List<MtMA_B> listaAB { get; set; } = new List<MtMA_B>();
	}

	public class MtMA_B
	{
		public int IdA { get; set; }
		public int IdB { get; set; }

		public MtMA A { get; set; }
		public MtMB B { get; set; }

		public MtMA_B(MtMA a, MtMB b)
		{
			A = a;
			B = b;
		}

		public MtMA_B() { }
	}


}

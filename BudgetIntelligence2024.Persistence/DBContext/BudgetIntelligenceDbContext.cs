using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BudgetIntelligence2024.Persistence.DBContext;

public partial class BudgetIntelligenceDbContext : DbContext
{
    public BudgetIntelligenceDbContext()
    {
    }

    public BudgetIntelligenceDbContext(DbContextOptions<BudgetIntelligenceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountBalance> AccountBalances { get; set; }

    public virtual DbSet<BalanceDifference> BalanceDifferences { get; set; }

    public virtual DbSet<Budget> Budgets { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<EditDistance> EditDistances { get; set; }

    public virtual DbSet<FutureTransaction> FutureTransactions { get; set; }

    public virtual DbSet<PeriodType> PeriodTypes { get; set; }

    public virtual DbSet<Phrase> Phrases { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionList> TransactionLists { get; set; }

    public virtual DbSet<TransactionStaging> TransactionStagings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VCategoryHierarchy> VCategoryHierarchies { get; set; }

    public virtual DbSet<VCategoryHierarchyExpanded> VCategoryHierarchyExpandeds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:BudgetIntelligence");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_User");
        });

        modelBuilder.Entity<AccountBalance>(entity =>
        {
            entity.ToTable("AccountBalance");

            entity.Property(e => e.Balance).HasColumnType("money");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountBalances)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountBalance_Account");

            entity.HasOne(d => d.User).WithMany(p => p.AccountBalances)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountBalance_User");
        });

        modelBuilder.Entity<BalanceDifference>(entity =>
        {
            entity.ToTable("BalanceDifference");

            entity.Property(e => e.Difference).HasColumnType("money");
        });

        modelBuilder.Entity<Budget>(entity =>
        {
            entity.ToTable("Budget");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.Comment)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PeriodTypeQty).HasColumnType("money");

            entity.HasOne(d => d.Account).WithMany(p => p.Budgets)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Budget_Account");

            entity.HasOne(d => d.Category).WithMany(p => p.Budgets)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Budget_Category");

            entity.HasOne(d => d.PeriodType).WithMany(p => p.Budgets)
                .HasForeignKey(d => d.PeriodTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Budget_PeriodType");

            entity.HasOne(d => d.User).WithMany(p => p.Budgets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Budget_User");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Expense");
            entity.Property(e => e.Typo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Category_Category");
        });

        modelBuilder.Entity<EditDistance>(entity =>
        {
            entity.ToTable("EditDistance");

            entity.Property(e => e.EditDistance1).HasColumnName("EditDistance");

            entity.HasOne(d => d.Related).WithMany(p => p.EditDistanceRelateds)
                .HasForeignKey(d => d.RelatedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EditDistance_Transaction1");

            entity.HasOne(d => d.Transaction).WithMany(p => p.EditDistanceTransactions)
                .HasForeignKey(d => d.TransactionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EditDistance_Transaction");

            entity.HasOne(d => d.User).WithMany(p => p.EditDistances)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EditDistance_User");
        });

        modelBuilder.Entity<FutureTransaction>(entity =>
        {
            entity.ToTable("FutureTransaction");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.Balance).HasColumnType("money");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.FutureTransactions)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FutureTransaction_Account");

            entity.HasOne(d => d.Category).WithMany(p => p.FutureTransactions)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_FutureTransaction_Category");

            entity.HasOne(d => d.User).WithMany(p => p.FutureTransactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FutureTransaction_User");
        });

        modelBuilder.Entity<PeriodType>(entity =>
        {
            entity.ToTable("PeriodType");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Replace Me");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("DAY");
        });

        modelBuilder.Entity<Phrase>(entity =>
        {
            entity.ToTable("Phrase");

            entity.Property(e => e.PhraseText)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Phrases)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Phrase_Category");

            entity.HasOne(d => d.Transaction).WithMany(p => p.Phrases)
                .HasForeignKey(d => d.TransactionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Phrase_Transaction");

            entity.HasOne(d => d.User).WithMany(p => p.Phrases)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Phrase_User");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("Transaction");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.Balance).HasColumnType("money");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsSplitOrigin).HasDefaultValue(false);
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_Account");

            entity.HasOne(d => d.Category).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Transaction_Category");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_User");
        });

        modelBuilder.Entity<TransactionList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Transaction_List");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.Balance).HasColumnType("money");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TransactionId).ValueGeneratedOnAdd();
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TransactionStaging>(entity =>
        {
            entity.HasKey(e => e.TransactionId);

            entity.ToTable("TransactionStaging");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.Balance).HasColumnType("money");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.TransactionStagings)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TransactionStaging_Account");

            entity.HasOne(d => d.User).WithMany(p => p.TransactionStagings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TransactionStaging_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User_UserID");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LoginName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(64)
                .IsFixedLength();
        });

        modelBuilder.Entity<VCategoryHierarchy>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_CategoryHierarchy");

            entity.Property(e => e.ChildName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VCategoryHierarchyExpanded>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_CategoryHierarchyExpanded");

            entity.Property(e => e.Level1Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Level2Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Level3Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RootName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

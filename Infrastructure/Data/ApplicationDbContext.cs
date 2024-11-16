using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Audit> Audits { get; set; }

    public virtual DbSet<CatActivity> CatActivities { get; set; }

    public virtual DbSet<CatCity> CatCities { get; set; }

    public virtual DbSet<CatDepartment> CatDepartments { get; set; }

    public virtual DbSet<CatRole> CatRoles { get; set; }

    public virtual DbSet<CatState> CatStates { get; set; }

    public virtual DbSet<CatTag> CatTags { get; set; }

    public virtual DbSet<CatType> CatTypes { get; set; }

    public virtual DbSet<List> Lists { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Audit>(entity =>
        {
            entity.HasKey(e => e.IdAudit).HasName("audit_pkey");

            entity.ToTable("audit");

            entity.Property(e => e.IdAudit).HasColumnName("id_audit");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(124)
                .HasColumnName("modified_by");
            entity.Property(e => e.OldValue).HasColumnName("old_value");
            entity.Property(e => e.PostValue).HasColumnName("post_value");
            entity.Property(e => e.ViewAction)
                .HasMaxLength(50)
                .HasColumnName("view_action");
        });

        modelBuilder.Entity<CatActivity>(entity =>
        {
            entity.HasKey(e => e.IdActivity).HasName("cat_activity_pkey");

            entity.ToTable("cat_activity");

            entity.Property(e => e.IdActivity).HasColumnName("id_activity");
            entity.Property(e => e.NameActivity)
                .HasMaxLength(128)
                .HasColumnName("name_activity");
            entity.Property(e => e.UrlIcon)
                .HasMaxLength(15)
                .HasColumnName("url_icon");
        });

        modelBuilder.Entity<CatCity>(entity =>
        {
            entity.HasKey(e => e.IdCity).HasName("cat_city_pkey");

            entity.ToTable("cat_city");

            entity.Property(e => e.IdCity).HasColumnName("id_city");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.NameCity)
                .HasMaxLength(60)
                .HasColumnName("name_city");

            entity.HasOne(d => d.Department).WithMany(p => p.CatCities)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("cat_city_department_id_fkey");
        });

        modelBuilder.Entity<CatDepartment>(entity =>
        {
            entity.HasKey(e => e.IdDepartment).HasName("cat_department_pkey");

            entity.ToTable("cat_department");

            entity.Property(e => e.IdDepartment).HasColumnName("id_department");
            entity.Property(e => e.NameDepartment)
                .HasMaxLength(40)
                .HasColumnName("name_department");
        });

        modelBuilder.Entity<CatRole>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("cat_role_pkey");

            entity.ToTable("cat_role");

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.NameRole)
                .HasMaxLength(50)
                .HasColumnName("name_role");
        });

        modelBuilder.Entity<CatState>(entity =>
        {
            entity.HasKey(e => e.IdState).HasName("cat_state_pkey");

            entity.ToTable("cat_state");

            entity.Property(e => e.IdState).HasColumnName("id_state");
            entity.Property(e => e.NameState)
                .HasMaxLength(30)
                .HasColumnName("name_state");
            entity.Property(e => e.TableRelation)
                .HasMaxLength(30)
                .HasColumnName("table_relation");
        });

        modelBuilder.Entity<CatTag>(entity =>
        {
            entity.HasKey(e => e.IdTag).HasName("cat_tag_pkey");

            entity.ToTable("cat_tag");

            entity.Property(e => e.IdTag).HasColumnName("id_tag");
            entity.Property(e => e.NameTag)
                .HasMaxLength(20)
                .HasColumnName("name_tag");
        });

        modelBuilder.Entity<CatType>(entity =>
        {
            entity.HasKey(e => e.IdType).HasName("cat_type_pkey");

            entity.ToTable("cat_type");

            entity.Property(e => e.IdType).HasColumnName("id_type");
            entity.Property(e => e.NameType)
                .HasMaxLength(30)
                .HasColumnName("name_type");
            entity.Property(e => e.TableRelation)
                .HasMaxLength(30)
                .HasColumnName("table_relation");
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(e => e.IdList).HasName("list_pkey");

            entity.ToTable("list");

            entity.Property(e => e.IdList).HasColumnName("id_list");
            entity.Property(e => e.NameList)
                .HasMaxLength(30)
                .HasColumnName("name_list");
            entity.Property(e => e.PersonId).HasColumnName("person_id");

            entity.HasOne(d => d.Person).WithMany(p => p.Lists)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("list_person_id_fkey");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("person_pkey");

            entity.ToTable("person");

            entity.HasIndex(e => e.Email, "person_email_key").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.AccountDeleted)
                .HasDefaultValue(false)
                .HasColumnName("account_deleted");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modified");
            entity.Property(e => e.Email)
                .HasMaxLength(75)
                .HasColumnName("email");
            entity.Property(e => e.EmailList)
                .HasDefaultValue(true)
                .HasColumnName("email_list");
            entity.Property(e => e.EmailNotification)
                .HasDefaultValue(true)
                .HasColumnName("email_notification");
            entity.Property(e => e.EmailPost)
                .HasDefaultValue(true)
                .HasColumnName("email_post");
            entity.Property(e => e.NameUser)
                .HasMaxLength(65)
                .HasColumnName("name_user");
            entity.Property(e => e.Password)
                .HasMaxLength(128)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.Telephone)
                .HasMaxLength(15)
                .HasColumnName("telephone");

            entity.HasOne(d => d.Role).WithMany(p => p.People)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_role_id_fkey");

            entity.HasOne(d => d.State).WithMany(p => p.People)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("person_state_id_fkey");
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.HasKey(e => e.IdPicture).HasName("picture_pkey");

            entity.ToTable("picture");

            entity.Property(e => e.IdPicture).HasColumnName("id_picture");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .HasColumnName("link");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .HasColumnName("name");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.PlaceId).HasColumnName("place_id");
            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Person).WithMany(p => p.Pictures)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("picture_person_id_fkey");

            entity.HasOne(d => d.Place).WithMany(p => p.Pictures)
                .HasForeignKey(d => d.PlaceId)
                .HasConstraintName("picture_place_id_fkey");

            entity.HasOne(d => d.Review).WithMany(p => p.Pictures)
                .HasForeignKey(d => d.ReviewId)
                .HasConstraintName("picture_review_id_fkey");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Pictures)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("picture_ticket_id_fkey");

            entity.HasOne(d => d.Type).WithMany(p => p.Pictures)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("picture_type_id_fkey");
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.HasKey(e => e.IdPlace).HasName("place_pkey");

            entity.ToTable("place");

            entity.Property(e => e.IdPlace).HasColumnName("id_place");
            entity.Property(e => e.Address)
                .HasMaxLength(128)
                .HasColumnName("address");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modified");
            entity.Property(e => e.Description)
                .HasMaxLength(560)
                .HasColumnName("description");
            entity.Property(e => e.Latitude)
                .HasPrecision(9, 6)
                .HasColumnName("latitude");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .HasColumnName("link");
            entity.Property(e => e.Longitude)
                .HasPrecision(9, 6)
                .HasColumnName("longitude");
            entity.Property(e => e.NamePlace)
                .HasMaxLength(80)
                .HasColumnName("name_place");
            entity.Property(e => e.StateId).HasColumnName("state_id");

            entity.HasOne(d => d.City).WithMany(p => p.Places)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("place_city_id_fkey");

            entity.HasOne(d => d.State).WithMany(p => p.Places)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("place_state_id_fkey");

            entity.HasMany(d => d.Activities).WithMany(p => p.Places)
                .UsingEntity<Dictionary<string, object>>(
                    "ActivityPlace",
                    r => r.HasOne<CatActivity>().WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("activity_place_activity_id_fkey"),
                    l => l.HasOne<Place>().WithMany()
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("activity_place_place_id_fkey"),
                    j =>
                    {
                        j.HasKey("PlaceId", "ActivityId").HasName("activity_place_pkey");
                        j.ToTable("activity_place");
                        j.IndexerProperty<int>("PlaceId").HasColumnName("place_id");
                        j.IndexerProperty<int>("ActivityId").HasColumnName("activity_id");
                    });

            entity.HasMany(d => d.Lists).WithMany(p => p.Places)
                .UsingEntity<Dictionary<string, object>>(
                    "PlaceList",
                    r => r.HasOne<List>().WithMany()
                        .HasForeignKey("ListId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("place_list_list_id_fkey"),
                    l => l.HasOne<Place>().WithMany()
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("place_list_place_id_fkey"),
                    j =>
                    {
                        j.HasKey("PlaceId", "ListId").HasName("place_list_pkey");
                        j.ToTable("place_list");
                        j.IndexerProperty<int>("PlaceId").HasColumnName("place_id");
                        j.IndexerProperty<int>("ListId").HasColumnName("list_id");
                    });
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.IdPost).HasName("post_pkey");

            entity.ToTable("post");

            entity.Property(e => e.IdPost).HasColumnName("id_post");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(60)
                .HasColumnName("created_by");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.PlaceId).HasColumnName("place_id");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.Title)
                .HasMaxLength(60)
                .HasColumnName("title");

            entity.HasOne(d => d.Place).WithMany(p => p.Posts)
                .HasForeignKey(d => d.PlaceId)
                .HasConstraintName("post_place_id_fkey");

            entity.HasMany(d => d.Tags).WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTag",
                    r => r.HasOne<CatTag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("post_tag_tag_id_fkey"),
                    l => l.HasOne<Post>().WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("post_tag_post_id_fkey"),
                    j =>
                    {
                        j.HasKey("PostId", "TagId").HasName("post_tag_pkey");
                        j.ToTable("post_tag");
                        j.IndexerProperty<int>("PostId").HasColumnName("post_id");
                        j.IndexerProperty<int>("TagId").HasColumnName("tag_id");
                    });
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.IdReview).HasName("review_pkey");

            entity.ToTable("review");

            entity.Property(e => e.IdReview).HasColumnName("id_review");
            entity.Property(e => e.Comment)
                .HasMaxLength(255)
                .HasColumnName("comment");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modified");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.PlaceId).HasColumnName("place_id");
            entity.Property(e => e.Rate)
                .HasPrecision(2, 1)
                .HasColumnName("rate");

            entity.HasOne(d => d.Person).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("review_person_id_fkey");

            entity.HasOne(d => d.Place).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("review_place_id_fkey");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.IdTicket).HasName("ticket_pkey");

            entity.ToTable("ticket");

            entity.Property(e => e.IdTicket).HasColumnName("id_ticket");
            entity.Property(e => e.Affair).HasColumnName("affair");
            entity.Property(e => e.AnsweredBy)
                .HasMaxLength(60)
                .HasColumnName("answered_by");
            entity.Property(e => e.DateAnswered)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_answered");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.ScalpAdmin)
                .HasDefaultValue(false)
                .HasColumnName("scalp_admin");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Person).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticket_person_id_fkey");

            entity.HasOne(d => d.State).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("ticket_state_id_fkey");

            entity.HasOne(d => d.Type).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("ticket_type_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

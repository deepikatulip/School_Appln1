namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StudentDbContext : DbContext
    {
        public StudentDbContext()
            : base("name=StudentDbContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<State> States { get; set; }
        
        public virtual DbSet<DeletedCustomer> DeletedCustomers { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<emp> emps { get; set; }
        public virtual DbSet<empinfo> empinfoes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<TUser> TUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Blood_Group> Blood_Group { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<Fee_Detail> Fee_Detail { get; set; }
        public virtual DbSet<FeePayment_Detail> FeePayment_Detail { get; set; }
        public virtual DbSet<Fees_Component> Fees_Component { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<MSreplication_options> MSreplication_options { get; set; }
        public virtual DbSet<Payment_Mode> Payment_Mode { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<spt_fallback_db> spt_fallback_db { get; set; }
        public virtual DbSet<spt_fallback_dev> spt_fallback_dev { get; set; }
        public virtual DbSet<spt_fallback_usg> spt_fallback_usg { get; set; }
        public virtual DbSet<spt_monitor> spt_monitor { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Staff_Educational_Details> Staff_Educational_Details { get; set; }
        public virtual DbSet<Staff_Exp_Details> Staff_Exp_Details { get; set; }
        public virtual DbSet<Staff_Type> Staff_Type { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Student_Other_Details> Student_Other_Details { get; set; }
        public virtual DbSet<Student_Prev_School_Details> Student_Prev_School_Details { get; set; }
        public virtual DbSet<Student_Sibling_Details> Student_Sibling_Details { get; set; }
        public virtual DbSet<Subject_Detail> Subject_Detail { get; set; }
        public virtual DbSet<spt_values> spt_values { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.Name)
                .IsUnicode(false);
             
            modelBuilder.Entity<emp>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<emp>()
                .Property(e => e.designation)
                .IsUnicode(false);

            modelBuilder.Entity<Gender>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<emp>()
                .HasMany(e => e.emp1)
                .WithOptional(e => e.emp2)
                .HasForeignKey(e => e.supid);

            modelBuilder.Entity<empinfo>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<Section>()
                .Property(e => e.Section_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Blood_Group>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Designation>()
                .Property(e => e.Designation_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Fee_Detail>()
                .Property(e => e.Total)
                .HasPrecision(18, 0);

            modelBuilder.Entity<FeePayment_Detail>()
                .Property(e => e.Fees_Recipt_No)
                .IsFixedLength();

            modelBuilder.Entity<Fees_Component>()
                .Property(e => e.Fees_Component_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Institution>()
                .Property(e => e.Institution_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Payment_Mode>()
                .Property(e => e.Payment_Mode_Name)
                .IsUnicode(false);

            modelBuilder.Entity<School>()
                .Property(e => e.School_Name)
                .IsUnicode(false);

            modelBuilder.Entity<spt_fallback_db>()
                .Property(e => e.xserver_name)
                .IsUnicode(false);

            modelBuilder.Entity<spt_fallback_db>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<spt_fallback_dev>()
                .Property(e => e.xserver_name)
                .IsUnicode(false);

            modelBuilder.Entity<spt_fallback_dev>()
                .Property(e => e.xfallback_drive)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<spt_fallback_dev>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<spt_fallback_dev>()
                .Property(e => e.phyname)
                .IsUnicode(false);

            modelBuilder.Entity<spt_fallback_usg>()
                .Property(e => e.xserver_name)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Middle_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
              .Property(e => e.Name)
              .IsUnicode(false);

            modelBuilder.Entity<State>()
              .Property(e => e.Name)
              .IsUnicode(false);

            modelBuilder.Entity<City>()
              .Property(e => e.Name)
              .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Father_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Mobile_No)
                .IsFixedLength();

            modelBuilder.Entity<Staff>()
                .Property(e => e.Alt_Mobile_No)
                .IsFixedLength();

            modelBuilder.Entity<Staff>()
                .Property(e => e.Email_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Address_Line1)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Address_Line2)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.PinCode)
                .IsUnicode(false);

            //modelBuilder.Entity<Staff>()
            //    .Property(e => e.Experience_in_Years)
            //    .IsUnicode(false);

            modelBuilder.Entity<Staff_Educational_Details>()
                .Property(e => e.Institution_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_Educational_Details>()
                .Property(e => e.Medium_Of_Instruction)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_Exp_Details>()
                .Property(e => e.Designation)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_Exp_Details>()
                .Property(e => e.Subject_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_Type>()
                .Property(e => e.Staff_Type_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Roll_No)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Middle_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Father_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Mother_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Address_Line1)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Address_Line2)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Phone_No1)
                .IsFixedLength();

            modelBuilder.Entity<Student>()
                .Property(e => e.Phone_No2)
                .IsFixedLength();

            modelBuilder.Entity<Student>()
                .Property(e => e.LandLine)
                .IsFixedLength();

            modelBuilder.Entity<Student>()
                .Property(e => e.Email_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Pincode)
                .IsFixedLength();

            modelBuilder.Entity<Student_Other_Details>()
                .Property(e => e.Identification_Mark1)
                .IsUnicode(false);

            modelBuilder.Entity<Student_Other_Details>()
                .Property(e => e.Identification_Mark2)
                .IsUnicode(false);

            modelBuilder.Entity<Student_Other_Details>()
                .Property(e => e.Allergy_Details)
                .IsUnicode(false);

            modelBuilder.Entity<Student_Other_Details>()
                .Property(e => e.Caste)
                .IsUnicode(false);

            modelBuilder.Entity<Student_Other_Details>()
                .Property(e => e.Religion)
                .IsUnicode(false);

            modelBuilder.Entity<Student_Other_Details>()
                .Property(e => e.Languages_Known)
                .IsFixedLength();

            modelBuilder.Entity<Student_Prev_School_Details>()
                .Property(e => e.School_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Student_Prev_School_Details>()
                .Property(e => e.Address_Line1)
                .IsUnicode(false);

            modelBuilder.Entity<Student_Prev_School_Details>()
                .Property(e => e.Leaving_Reason)
                .IsUnicode(false);

            modelBuilder.Entity<Student_Prev_School_Details>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Student_Sibling_Details>()
                .Property(e => e.Sibling_Name)
                .IsUnicode(false);

            modelBuilder.Entity<spt_values>()
                .Property(e => e.type)
                .IsFixedLength();
        }
    }
}

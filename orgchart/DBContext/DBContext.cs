using orgchart.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace orgchart
{
    public class DBContext : DbContext
    {
        public DBContext() : base("DBModelConString")
        {
          // Database.SetInitializer(new MigrateDatabaseToLatestVersion<DBContext, EFConsole..Configuration>());
        }
        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }
        public DbSet<User> users { get; set; }

        public DbSet<Designation> designations { get; set; }

        public DbSet<Employee> employees { get; set; }

        public DbSet<company> companies { get; set; }

        public DbSet<Department> departments { get; set; }

        public DbSet<Module> modules { get; set; }

        public DbSet<RolePermission> rolePermissions { get; set; }

        public DbSet<Roles> roles { get; set; }

        public DbSet<UserRole> userRoles { get; set; }

        public DbSet<EmployeeUser> employee_users { get; set; }

        private void OnBeforeSaving()
        {
            int user_id = 0;
            try
            {
                user_id = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            }
            catch
            {

            }
            var entities = ChangeTracker.Entries();
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((IBaseModel)entity.Entity).created_at = DateTime.Now;
                    ((IBaseModel)entity.Entity).created_by = user_id;
                }

               ((IBaseModel)entity.Entity).modified_at = DateTime.Now;
                ((IBaseModel)entity.Entity).modified_by = user_id;
            }
            
        }
    }


    
}
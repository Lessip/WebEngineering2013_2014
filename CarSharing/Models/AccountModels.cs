using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace CarSharing.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public System.Data.Entity.DbSet<CarSharing.user_account> users { get; set; }
        public System.Data.Entity.DbSet<CarSharing.user_address> user_addresses { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int id { get; set; }
        [Display(Name = "Username")]
        public string login_name { get; set; }
        [Display(Name = "First name")]
        public String firstname { get; set; }
        [Display(Name = "Family Name")]
        public String name { get; set; }
        [Display(Name = "Date of birth")]
        public DateTime date_of_birth { get; set; }
        [Display(Name = "Password")]
        public String password { get; set; }
        [Display(Name = "Email")]
        public String email { get; set; }
        [Display(Name = "Access state")]
        public int access_state { get; set; }
        [Display(Name = "Identity number")]
        public Nullable<Guid> identity_number { get; set; }
        [Display(Name = "Timelimit")]
        public Nullable<DateTime> timelimit { get; set; }
        [Display(Name = "Street")]
        public String street { get; set; }
        [Display(Name = "Zip code")]
        public String post_code { get; set; }
        [Display(Name = "City")]
        public String city { get; set; }
        [Display(Name = "Deleted")]
        public Nullable<DateTime> deleteDate { get; set; }

        // Constructors for new userprofiles
        // Standard contrustor
        public UserProfile()
        {

        }

        // Constructor for admin user-profile view
        public UserProfile(CarSharing.user_account new_user, CarSharing.user_address new_user_address)
        {
            id = new_user.id;
            login_name = new_user.login_name;
            firstname = new_user.firstname;
            name = new_user.name;
            date_of_birth = new_user.date_of_birth;
            password = new_user.password;
            email = new_user.email;
            access_state = (int)new_user.access_state;
            identity_number = (Guid)new_user.identity_number;
            timelimit = (DateTime)new_user.timelimit;
            deleteDate = (DateTime)new_user.remove_date;
            street = new_user_address.street;
            post_code = new_user_address.post_code;
            city = new_user_address.city;            
        }
    }

    [Table("UserContractWrap")]
    public class WrapUserContractTables
    {
        public CarSharing.Models.UserProfile userTable;
        public List<CarSharing.contract> contractTable;

        public WrapUserContractTables()
        {
            userTable = null;
            contractTable = null;
        }

        public WrapUserContractTables(CarSharing.Models.UserProfile userProfile, List<contract> contractList)
        {
            userTable = userProfile;
            contractTable = contractList;
        }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "Benutzername")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Aktuelles Kennwort")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "&quote{0}&quote muss mindestens {2} Zeichen lang sein.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Neues Kennwort")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Neues Kennwort bestätigen")]
        [Compare("NewPassword", ErrorMessage = "Das neue Kennwort entspricht nicht dem Bestätigungskennwort.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Benutzername")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Kennwort")]
        public string Password { get; set; }

        [Display(Name = "Speichern?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Benutzername")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "&quote{0}&quote muss mindestens {2} Zeichen lang sein.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Kennwort")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Kennwort bestätigen")]
        [Compare("Password", ErrorMessage = "Das Kennwort entspricht nicht dem Bestätigungskennwort.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
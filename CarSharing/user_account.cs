//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarSharing
{
    using System;
    using System.Collections.Generic;
    
    public partial class user_account
    {
        public user_account()
        {
            this.contract = new HashSet<contract>();
            this.user_address = new HashSet<user_address>();
        }
    
        public int id { get; set; }
        public string login_name { get; set; }
        public string firstname { get; set; }
        public string name { get; set; }
        public System.DateTime date_of_birth { get; set; }
        public Nullable<System.Guid> identity_number { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public Nullable<int> access_state { get; set; }
        public Nullable<System.DateTime> timelimit { get; set; }
    
        public virtual ICollection<contract> contract { get; set; }
        public virtual ICollection<user_address> user_address { get; set; }
    }
}

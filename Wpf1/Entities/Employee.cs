//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wpf1.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations.Model;
    using System.Runtime.CompilerServices;

    public partial class Employee
    {
        public int ID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public System.DateTime BirthDate { get; set; }
        public int TitleID { get; set; }
    
        public virtual Title Title { get; set; }
        //public Create(ID, FN, Name, Patr)
        //{
        //    return Employee;
        //}

    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SisGerPro.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Etapas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Etapas()
        {
            this.Atividades = new HashSet<Atividades>();
        }
    
        public int id_Etapa { get; set; }
        public string Nome { get; set; }
        public bool Feita { get; set; }
        public Nullable<System.DateTime> DtFeita { get; set; }
        public int id_Projeto { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Atividades> Atividades { get; set; }
        public virtual Projetos Projetos { get; set; }
    }
}

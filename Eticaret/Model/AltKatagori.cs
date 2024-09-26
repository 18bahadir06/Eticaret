using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret.Model
{
    public class AltKatagori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AltKatgori()
        {
            this.Urun = new HashSet<Urun>();
        }

        public int AltKatagoriId { get; set; }
        public string AltKatagoriAd { get; set; }
        public Nullable<int> KatagoriId { get; set; }

        public virtual Katagori Katagori { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Urun> Urun { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Liga
{
    public int LigaNr { get; set; }

    public string Verband { get; set; }

    public DateTime Erstaustragung { get; set; }

    public int? Meister { get; set; }

    public string Rekordspieler { get; set; }

    public int? SpieleRekordspieler { get; set; }

    public virtual Verein MeisterNavigation { get; set; }

    public virtual ICollection<Verein> Vereins { get; set; } = new List<Verein>();
}

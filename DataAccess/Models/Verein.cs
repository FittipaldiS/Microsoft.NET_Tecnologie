using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Verein
{
    public int VId { get; set; }

    public string Name { get; set; }

    public int? Liga { get; set; }

    public virtual Liga LigaNavigation { get; set; }

    public virtual ICollection<Liga> Ligas { get; set; } = new List<Liga>();

    public virtual ICollection<Spiel> SpielGastNavigations { get; set; } = new List<Spiel>();

    public virtual ICollection<Spiel> SpielHeimNavigations { get; set; } = new List<Spiel>();

    public virtual ICollection<Spieler> Spielers { get; set; } = new List<Spieler>();
}

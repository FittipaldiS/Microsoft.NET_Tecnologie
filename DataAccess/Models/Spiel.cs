using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Spiel
{
    public int SpielId { get; set; }

    public int? Spieltag { get; set; }

    public DateTime? Datum { get; set; }

    public TimeSpan? Uhrzeit { get; set; }

    public int Heim { get; set; }

    public int Gast { get; set; }

    public int? ToreHeim { get; set; }

    public int? ToreGast { get; set; }

    public virtual Verein GastNavigation { get; set; }

    public virtual Verein HeimNavigation { get; set; }
}

using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Spieler
{
    public int SpielerId { get; set; }

    public int VereinsId { get; set; }

    public int? TrikotNr { get; set; }

    public string SpielerName { get; set; }

    public string Land { get; set; }

    public int? Spiele { get; set; }

    public int? Tore { get; set; }

    public int? Vorlagen { get; set; }

    public virtual Verein Vereins { get; set; }
}

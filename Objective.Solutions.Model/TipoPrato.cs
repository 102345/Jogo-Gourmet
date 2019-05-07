using System.Collections.Generic;

namespace Objective.Solutions.Model
{
    public class TipoPrato
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        List<Prato> Pratos { get; set; }
    }
}

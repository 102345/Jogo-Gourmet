using Objective.Solutions.Model;
using Objective.Solutions.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Objective.Solutions.Service
{
    public class PratoService : IPratoService
    {
        public List<TipoPrato> tipos = new List<TipoPrato>();
        public List<Prato> pratos = new List<Prato>();

        public PratoService()
        {

        }

        public void IncluirPrato(Prato prato)
        {
            pratos.Add(prato);
        }

        public void IncluirTipoPrato(TipoPrato tipoPrato)
        {
            tipos.Add(tipoPrato);
        }

        public Prato BuscarPrato(int idTipoPrato)
        {
            var maxId = 0;

            if(idTipoPrato > 0)  maxId = pratos.Where(x => x.IdTipoPrato == idTipoPrato).Max(x => x.Id);

            return pratos.Where(x => x.IdTipoPrato == idTipoPrato && x.Id == maxId).FirstOrDefault();
        }

        public List<TipoPrato> ListarTipoPrato()
        {
            return tipos.ToList();
        }

        public TipoPrato BuscarTipoPrato(string descTipo)
        {
            return tipos.Where(x => x.Tipo.ToLower() == descTipo.ToLower()).FirstOrDefault();
        }

        public int BuscarMaxIdTipoPrato()
        {
            return tipos.Max(x => x.Id);
        }
    }
}

using Objective.Solutions.Model;
using System.Collections.Generic;

namespace Objective.Solutions.Service.Interface
{
    public interface IPratoService
    {
        void IncluirTipoPrato(TipoPrato tipoPrato);
        void IncluirPrato(Prato prato);
        int BuscarMaxIdTipoPrato();
        List<TipoPrato> ListarTipoPrato();
        TipoPrato BuscarTipoPrato(string descTipo);
        Prato BuscarPrato(int idTipoPrato);
    }
}

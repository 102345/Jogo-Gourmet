using Microsoft.VisualBasic;
using Objective.Solutions.Model;
using Objective.Solutions.Service;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Objetive.Solutions.View
{
    public partial class frmJogoGoumert : Form
    {
        List<TipoPrato> tipos = new List<TipoPrato>();
        List<Prato> pratos = new List<Prato>();
        private PratoService service = new PratoService();
        const string TITULOMESSAGEM = "Jogo Gourmet";

        public frmJogoGoumert()
        {
            InitializeComponent();
            IniciarDadosJogo();
        }

        private void btnIniciarJogo_Click(object sender, EventArgs e)
        {
            try
            {
                this.PreverPrato();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, TITULOMESSAGEM, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

          
        }


        private void PreverPrato()
        {
            bool Acao = false;

            MessageBox.Show("Pense em um prato que gosta", TITULOMESSAGEM, MessageBoxButtons.OK, MessageBoxIcon.Information);


            List<TipoPrato> tipos = service.ListarTipoPrato();

            foreach (var tipo in tipos)
            {
                int retTipoResp = PerguntarTipoPrato(tipo.Tipo);

                if(retTipoResp == 0)
                {
                    Prato prato = service.BuscarPrato(tipo.Id);

                    int retRespPrato = PerguntarPrato(prato.Descricao);
                    
                    if(retRespPrato == 0)
                    {
                        Acertei();
                        Acao = true;
                    }
                    else
                    {
                       string descPratoRet =  MostrarCaixaDialogoPrato();

                        MostrarCaixaDialogoTrocaPrato(prato.IdTipoPrato, descPratoRet);
                        Acao = true;
                    }

                }
                else 
                {
                    if (tipos.Count == 1)
                    {

                        Acao = IniciarCasoBoloChocolate();
                      
                    }
                    
                }
                

            }


            if (!Acao)
            {
                Acao = IniciarCasoBoloChocolate();

            }



        }

        private bool IniciarCasoBoloChocolate()
        {
            bool Acao;
            int retRespBoloChocolate = PerguntarPrato("Bolo de Chocolate");

            if (retRespBoloChocolate == 0)
            {
                Acertei();
                Acao = true;
            }
            else
            {
                string pratoNovoRet = MostrarCaixaDialogoPrato();

                MostrarCaixaDialogoTrocaPrato(0, pratoNovoRet);
                Acao = true;
            }

            return Acao;
        }

        private void Acertei()
        {

            MessageBox.Show("Acertei de novo!", TITULOMESSAGEM, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            PreverPrato();
           
        }




        private string MostrarCaixaDialogoPrato()
        {
            string prato = Interaction.InputBox("Qual prato você pensou ?", TITULOMESSAGEM, "");

            return prato;

        }


        private void MostrarCaixaDialogoTrocaPrato(int idTipoPrato, string descPratoNovo)
        {
            string descPrato = "Bolo de Chocolate";

            var prato = service.BuscarPrato(idTipoPrato);

            if (prato != null) descPrato = prato.Descricao;


            string msg = string.Format("{0} é _______________ mas {1} não", descPratoNovo, descPrato);

            string tipoPrato = Interaction.InputBox(msg, TITULOMESSAGEM, "");

            AdicionarPrato(descPratoNovo, tipoPrato);

            PreverPrato();
        }

        private void AdicionarPrato(string descPratoNovo, string tipoPrato)
        {
            TipoPrato tipoPratoRet = service.BuscarTipoPrato(tipoPrato);

            if (tipoPratoRet != null)
            {
                Prato pratoRet = service.BuscarPrato(tipoPratoRet.Id);

                Prato pratoNovo = new Prato
                {
                    Id = pratoRet.Id + 1,
                    IdTipoPrato = tipoPratoRet.Id,
                    Descricao = descPratoNovo
                };

                service.IncluirPrato(pratoNovo);
            }
            else
            {
                TipoPrato tipo = new TipoPrato()
                {
                    Id = service.BuscarMaxIdTipoPrato() + 1,
                    Tipo = tipoPrato

                };


                Prato pratoNovo = new Prato()
                {
                    Id = 1,
                    IdTipoPrato = tipo.Id,
                    Descricao = descPratoNovo
                };

                service.IncluirTipoPrato(tipo);
                service.IncluirPrato(pratoNovo);

            }
        }

        private int PerguntarTipoPrato(string descTipo)
        {
            string msg = string.Format("O prato que você pensou é {0}", descTipo);
            int ret = 1;

            if(MessageBox.Show(msg, TITULOMESSAGEM, MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ret = 0;

            }
          
            return ret;
        }

        private int PerguntarPrato(string descPrato)
        {
            string msg = string.Format("O prato que você pensou é {0}", descPrato);
            int ret = 1;

            if (MessageBox.Show(msg, TITULOMESSAGEM, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ret = 0;

            }

            return ret;
        }



        private void IniciarDadosJogo()
        {
            TipoPrato tipo = new TipoPrato()
            {
                Id = 1,
                Tipo = "Massa"

            };

            Prato prato = new Prato()
            {
                Id = 1,
                IdTipoPrato = tipo.Id,
                Descricao = "Lasanha"
            };

            service.IncluirTipoPrato(tipo);
            service.IncluirPrato(prato);

        }

     
    }
}

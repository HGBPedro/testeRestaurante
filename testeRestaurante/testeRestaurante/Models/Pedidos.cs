using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace testeRestaurante.Models
{
    public class Pedidos
    {
        public string Entrada { get; set; }
        public string PratoPrincipal { get; set; }
        public string Bebida { get; set; }
        public string Sobremesa { get; set; }

        public class JsonPedidos
        {
            public string Entrada { get; set; }
            public string PratoPrincipal { get; set; }
            public string Bebida { get; set; }
            public string Sobremesa { get; set; }
        }

        enum manha
        {
            [Description("Ovos")]
            Ovos = 1,
            [Description("Torradas")]
            Torradas = 2,
            [Description("Café")]
            Cafe = 3
        }
        enum noite
        {
            [Description("Bife")]
            Bife = 1,
            [Description("Batatas")]
            Batatas = 2,
            [Description("Vinho")]
            Vinho = 3,
            [Description("Bolo")]
            Bolo = 4
        }

        public List<string> ValidarPedido(string pedido)
        {
            List<string> _pedido = new List<string>(pedido.Split(','));
            List<string> oPedido = new List<string>();
            string periodo = _pedido.First();
            _pedido.Remove(_pedido.First());
            _pedido.Sort();
            _pedido = FormatarLista(_pedido);

            if (RemoverAcentos(periodo.ToLower()) == "manha")
            {
                oPedido = VerificarRepeticoes(_pedido, "manha");
            }
            else if(RemoverAcentos(periodo.ToLower()) == "noite")
            {
                oPedido = VerificarRepeticoes(_pedido, "noite");
            }

            return oPedido;
        }

        public static string RemoverAcentos(string texto)
        {
            string s = texto.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < s.Length; k++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(s[k]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(s[k]);
                }
            }
            return sb.ToString();
        }

        public List<string> VerificarRepeticoes(List<string> Lista, string periodo)
        {
            List<string> Pedidos = new List<string>();

            if(periodo == "manha")
            {
                for(int i = 0; i < Lista.Count(); i++)
                {
                    if (Lista[i] == "1")
                    {
                        if (Pedidos.Contains(Enum.GetName(typeof(manha), 1).ToString()))
                        {
                            AdicionarItem(Pedidos, "erro");
                            break;
                        }
                        else
                        {
                            AdicionarItem(Pedidos, Enum.GetName(typeof(manha), Int32.Parse(Lista[i])));
                        }
                    }
                    else if(Lista[i] == "2")
                    {
                        if (Pedidos.Contains(Enum.GetName(typeof(manha), 2).ToString()))
                        {
                            AdicionarItem(Pedidos, "erro");
                            break;
                        }
                        else
                        {
                            AdicionarItem(Pedidos, Enum.GetName(typeof(manha), Int32.Parse(Lista[i])));
                        }
                    }
                    else
                    {
                        AdicionarItem(Pedidos, Enum.GetName(typeof(manha), Int32.Parse(Lista[i])));
                    }
                }

            }
            else if(periodo == "noite")
            {
                for (int i = 0; i < Lista.Count(); i++)
                {
                    if (Lista[i] == "1")
                    {
                        if (Pedidos.Contains(Enum.GetName(typeof(noite), 1).ToString()))
                        {
                            AdicionarItem(Pedidos, "erro");
                            break;
                        }
                        else
                        {
                            AdicionarItem(Pedidos, Enum.GetName(typeof(noite), Int32.Parse(Lista[i])));
                        }
                    }
                    else if (Lista[i] == "3")
                    {
                        if (Pedidos.Contains(Enum.GetName(typeof(noite), 3).ToString()))
                        {
                            AdicionarItem(Pedidos, "erro");
                            break;
                        }
                        else
                        {
                            AdicionarItem(Pedidos, Enum.GetName(typeof(noite), Int32.Parse(Lista[i])));
                        }
                    }
                    else if (Lista[i] == "4")
                    {
                        if (Pedidos.Contains(Enum.GetName(typeof(noite), 4).ToString()))
                        {
                            AdicionarItem(Pedidos, "erro");
                            break;
                        }
                        else
                        {
                            AdicionarItem(Pedidos, Enum.GetName(typeof(noite), Int32.Parse(Lista[i])));
                        }
                    }
                    else
                    {
                        AdicionarItem(Pedidos, Enum.GetName(typeof(noite), Int32.Parse(Lista[i])));
                    }
                }
            }
            else
            {
                throw new Exception("Período inválido");
            }

            return Pedidos;

        }

        public List<string> AdicionarItem(List<string>Lista, string item)
        {
            Lista.Add(item);

            return Lista;
        }

        public List<string> VerificarIntegridadePedido(List<string> Lista)
        {
            foreach (string item in Lista)
            {
                if (Int32.Parse(item) > 5)
                {
                    Lista.Add("erro");
                    break;
                }
            }

            return Lista;
        }

        public List<string> FormatarLista(List<string> Lista)
        {
            for(int i = 0; i < Lista.Count(); i++)
            {
                Lista[i] = Lista[i].Trim();
            }

            return Lista;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace TesteDomvs.Negocio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int opcao = 0;
            bool sair = false;
            double latitude = 0f;
            double longitude = 0f;

            while (!sair)
            {
                Console.WriteLine("[ 1 ] Cadastrar Amigo");
                Console.WriteLine("[ 2 ] Procurar Amigos Proximos");
                Console.WriteLine("[ 0 ] Sair do Programa");
                Console.WriteLine("-------------------------------------");

                Console.Write("Digite uma opção: ");

                if (Int32.TryParse(Console.ReadLine(), out opcao))
                {
                    switch (opcao)
                    {
                        case 0:
                            sair = true;
                            break;
                        case 1:
                            CadastrarAmigo(out latitude, out longitude);
                            break;
                        case 2:
                            ProcurarAmigoProximo(out latitude, out longitude);
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("Opcao invalida");
                            break;
                    }
                }
                else
                    Console.WriteLine("Opcao invalida");

                Console.Clear();
            }
        }

        private static void ProcurarAmigoProximo(out double latitude, out double longitude)
        {
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Entre com a sua latitude:"); ;

                if (double.TryParse(Console.ReadLine(), out latitude))
                {
                    if (new Util.Localizacao().IsLatitude(latitude))
                        break;
                }

                Console.WriteLine("");
                Console.WriteLine("Digite uma latitude valida");
            }

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Entre com a sua longitude:");

                if (double.TryParse(Console.ReadLine(), out longitude))
                {
                    if (new Util.Localizacao().IsLongitude(longitude))
                        break;
                }

                Console.WriteLine("");
                Console.WriteLine("Digite uma longitude valida");
            }

            List<Model.Amigo> lstAmigos = new Model.Amigo().ListarAmigos(latitude, longitude);

            Console.WriteLine("Amigos mais proximos:");

            foreach (var item in lstAmigos.OrderByDescending(o => o.Distancia).Take(3))
            {
                Console.WriteLine("Amigo: " + item.Nome);
                Console.WriteLine("Distancia KM: " + item.Distancia);
            }
        }

        private static void CadastrarAmigo(out double latitude, out double longitude)
        {
            Console.WriteLine("");
            Console.WriteLine("## CADASTRO DE AMIGO ##");

            string nomeAmigo = "";

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Digite o Nome do Amigo: ");
                nomeAmigo = Console.ReadLine();

                if (nomeAmigo.Trim().Length >= 3)
                    break;

                Console.WriteLine("");
                Console.WriteLine("Digite um Nome de Amigo valido");
            }

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Entre com a latitude da casa do Amigo: "); ;

                if (double.TryParse(Console.ReadLine(), out latitude))
                {
                    if (new Util.Localizacao().IsLatitude(latitude))
                        break;
                }

                Console.WriteLine("");
                Console.WriteLine("Digite uma latitude valida");
            }

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Entre com a longitude da casa do Amigo: ");

                if (double.TryParse(Console.ReadLine(), out longitude))
                {
                    if (new Util.Localizacao().IsLongitude(longitude))
                        break;
                }

                Console.WriteLine("");
                Console.WriteLine("Digite uma longitude valida");
            }

            string resposta = "";

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Deseja Salvar (S/N)?");

                resposta = Console.ReadLine();

                if (resposta.ToUpper().StartsWith("S") && resposta.Length == 1)
                {
                    new Model.Amigo().InserirAmigo(nomeAmigo, latitude, longitude);
                    Console.WriteLine("");
                    Console.WriteLine("Registro salvo com sucesso");
                    break;
                }
                else if (resposta.ToUpper().StartsWith("N") && resposta.Length == 1)
                    break;
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Digite uma opcao valida");
                }
            }
        }
    }
}
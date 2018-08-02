using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ConsoleTables;
using DB;
using Modelo;
using Persistencia;

namespace SistemaFinanceiroSoN
{
    class Program
    {
        private List<Conta> contas;
        private List<Categoria> categorias;
        private CategoriaDAL categoria;
        private ContaDAL conta;

        public Program()
        {
            var strConn = Conexao.getStringConnection();
            this.conta = new ContaDAL(new SqlConnection(strConn));
            this.categoria = new CategoriaDAL(new SqlConnection(strConn));
        }

        static void Main(string[] args)
        {
            Program p = new Program();

            int opcao;
            do
            {
                Uteis.MontaMenu();
                opcao = Convert.ToInt32(Console.ReadLine());
                if (opcao < 1 || opcao > 6)
                {
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Uteis.MontaHeader("INFORME UMA OPÇÃO VALIDA", 'X', 30);
                    Console.ResetColor();
                }
                else
                {
                    Console.Clear();
                    switch (opcao)
                    {
                        case 1:
                            Uteis.MontaHeader("LISTAGEM DE CONTAS");
                            p.contas = new List<Conta>();
                            p.contas = p.conta.ListarTodos();
                            ListarContas();
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 2:
                            Uteis.MontaHeader("NOVA CONTA");
                            CadastrarConta();
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 3:
                            Console.WriteLine("Editar");
                            break;
                        case 4:
                            Console.WriteLine("Excluir");
                            break;
                        case 5:
                            Uteis.MontaHeader("RELATÓRIO");
                            GerarRelatorio();
                            Console.ReadLine();
                            Console.Clear();
                            break;
                    }
                }


            } while (opcao != 6);

            void GerarRelatorio()
            {
                Console.Write("Digite a data inicial: ");
                DateTime dataInicial = Convert.ToDateTime(Console.ReadLine());
                Console.Write("Digite a data final: ");
                DateTime dataFinal = Convert.ToDateTime(Console.ReadLine());
                p.contas = new List<Conta>();
                p.contas = p.conta.ListarTodos(dataInicial, dataFinal);
                ListarContas();
            }

            void ListarContas()
            {
                ConsoleTable table = new ConsoleTable("Id", "Descrição", "Tipo", "Valor", "Data Vencimento");
                foreach (var cont in p.contas)
                {
                    table.AddRow(cont.Id, cont.Descricao, cont.Tipo.Equals('R') ? "Receber" : "Pagar", cont.Valor, cont.DataVencimento);
                }
                table.Write();
            }

            void ListarCategorias()
            {
                ConsoleTable tableCategorias = new ConsoleTable("Id", "Nome");
                foreach (var cat in p.categorias)
                {
                    tableCategorias.AddRow(cat.Id, cat.Nome);
                }
                tableCategorias.Write();
            }

            void CadastrarConta()
            {
                var descricaoConta = "";
                do
                {
                    Console.Write("Informe a descrição da conta: ");
                    descricaoConta = Console.ReadLine();
                    if (descricaoConta != null && descricaoConta.Equals(""))
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Uteis.MontaHeader("INFORME UMA DESCRIÇÃO VALIDA", 'X', 30);
                        Console.ResetColor();
                    }

                } while (descricaoConta.Equals(""));

                Console.Write("Informe o valor da conta: ");
                double valorConta = Convert.ToDouble(Console.ReadLine());

                Console.Write("Informe a data de vencimento da conta (dd/mm/aaaa): ");
                DateTime dataVencimentoConta = Convert.ToDateTime(Console.ReadLine());

                Console.Write("Informe o tipo (R para Receber e P para pagar)");
                char tipoConta = Convert.ToChar(Console.ReadLine());

                Console.WriteLine("Selecione a categoria da conta");
                p.categorias = new List<Categoria>();
                p.categorias = p.categoria.ListarTodos();
                ListarCategorias();
                Console.Write("Informe o código da categoria desejada:");
                int categoriaConta = Convert.ToInt32(Console.ReadLine());
                Categoria categoriaCadastro = p.categoria.GetCategoria(categoriaConta);

                Conta conta = new Conta()
                {
                    Categoria = categoriaCadastro,
                    DataVencimento = dataVencimentoConta,
                    Descricao = descricaoConta,
                    Valor = valorConta,
                    Tipo = tipoConta
                };
                p.conta.Salvar(conta);
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
                Uteis.MontaHeader("CADASTRO REALIZADO COM SUCESSO", 'X', 30);
                Console.ResetColor();

            }


        }
    }
}

using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace SistemaFinanceiroSoN
{
    public static class Uteis
    {
        public static void MontaMenu()
        {
            MontaHeader("CONTROLE FINANCEIRO");
            Console.WriteLine("Selecione uma opção abaixo:");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("1 - Listar");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("3 - Editar");
            Console.WriteLine("4 - Excluir");
            Console.WriteLine("5 - Relatório");
            Console.WriteLine("6 - Sair");
            Console.Write("Opção: ");
        }

        public static void MontaHeader(string titulo, char cod = '=', int len = 30)
        {
            var linha = "";
            var comprimento = len - (titulo.Length / 2);
            for (var i = 0; i < comprimento; i++)
                linha = cod + linha;
            Console.WriteLine(linha + titulo + linha);
        }
    }
}

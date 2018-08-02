using System;

namespace Modelo
{
    public class Conta
    {
        private char _tipo;
        public int? Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public Categoria Categoria { get; set; }

        public char Tipo
        {
            get => _tipo;
            set => _tipo = !value.Equals('P') && !value.Equals('R') ? throw new Exception("Use P para pagar e R para receber") : value;
        }

        

    }
}

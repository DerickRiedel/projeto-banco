using System;
using AppBanco.Banco;

namespace AppBanco
{
    class Program
    {
        static void Main(string[] args)
        {
            var banco = new InterfaceBanco();
            banco.Executar();
        }
    }
}

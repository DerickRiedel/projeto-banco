using System;
namespace AppBanco.Banco
{
    public class InterfaceBase
    {
        protected void Escrever(string mensagem){
            Console.WriteLine(mensagem);
        }

        protected void EscreverSó(string mensagem){
            Console.Write(mensagem);
        }
        
        protected string LerString(){
            return Console.ReadLine();
        }

        protected string LerString(string mensagem){
            Escrever(mensagem);
            return LerString();
        }

        protected int LerInt(){
            var retorno = 0;
            var executando = true;
            do{
                try
                {
                    retorno = int.Parse(Console.ReadLine());
                    executando = false;
                }
                catch
                {
                    Escrever("Opção inválida. Digite novamente.");
                }
            }while(executando == true);
            return retorno;
        }

        protected void AguardarTecla()
        {
            Console.ReadKey();
        }

        protected void AguardarTecla(string mensagem)
        {
            Escrever(mensagem);
            Console.ReadKey();
        }

        protected int LerInt(string mensagem){
            Escrever(mensagem);
            return LerInt();
        }

        protected void LimparTela(){
            Console.Clear();
        }
    }
}
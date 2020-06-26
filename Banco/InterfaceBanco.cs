using System;
using System.Linq;
namespace AppBanco.Banco
{
    public class InterfaceBanco : InterfaceBase
    {
        private Banco banco = new Banco(); // Instanciando um banco
        private int valorNaMao = 1000; // Dinheiro na mão do indivíduo

        public void Executar(){
            bool executando = true;
            do{
                LimparTela();
                Escrever("| Caixa eletrônico |");
                Escrever("");
                Escrever("|Saldo disponível| "+Convert.ToString(banco.valorDisponivel)+" |");
                Escrever("|Dinheiro na mão | "+Convert.ToString(valorNaMao)+" |");
                Escrever("");
                Escrever("1 - Depositar");
                Escrever("2 - Retirar");
                Escrever("3 - Sair");
                Escrever("");
                int opt = LerInt("Escreva um número de uma opçao para realizar alguma ação.");
                switch(opt){
                    case 1:
                        Depositar();
                        break;
                    case 2:
                        Retirar();
                        break;
                    case 3:
                        executando = false;
                        break;
                }
            }while(executando == true);
        }

        public void Retirar(){
            bool executando = true;
            do{
                banco.Atualizar(); // Atualizando o valor do banco
                LimparTela();
                Escrever("|Saldo disponível| "+Convert.ToString(banco.valorDisponivel)+" |");
                Escrever("|Dinheiro na mão | "+Convert.ToString(valorNaMao)+" |");
                Escrever("");
                ListarCedulas(); // Mostrar as cédulas disponíveis
                Escrever("");
                Escrever("Digite a cédula que deseja Retirar \n(Digite 0 se quiser voltar)");

                var opt = LerInt();
                switch(opt){
                    case 0:
                        executando = false;
                        break;
                    case 1:
                        SacarCédulas(2);
                        break;
                    case 2:
                        SacarCédulas(5);
                        break;
                    case 3:
                        SacarCédulas(10);
                        break;
                    case 4:
                        SacarCédulas(20);
                        break;
                    case 5:
                        SacarCédulas(50);
                        break;
                    default:
                        AguardarTecla("Opção inválida");
                        break;
                }
            }while(executando == true);
        }

        public string[] ValoresCedulas(){ // Pegar a quantidade de cada tipo de cédula
            string[] valores = new string[5];
            valores[0] = (Convert.ToString(banco.ListarCedulas()[0]));
            valores[1] = (Convert.ToString(banco.ListarCedulas()[1]));
            valores[2] = (Convert.ToString(banco.ListarCedulas()[2]));
            valores[3] = (Convert.ToString(banco.ListarCedulas()[3]));
            valores[4] = (Convert.ToString(banco.ListarCedulas()[4]));

            return valores;
        }

        public void ListarCedulas(){
                Escrever("Cédulas disponíveis: ");
                Escrever("");
                EscreverSó("|'1' =  2R$");
                EscreverSó("("+ValoresCedulas()[0]+")");
                EscreverSó(" |'2' =  5R$");
                EscreverSó("("+ValoresCedulas()[1]+")");
                EscreverSó(" |'3' = 10R$");
                Escrever("("+ValoresCedulas()[2]+")");
                EscreverSó("|'4' = 20R$");
                EscreverSó("("+ValoresCedulas()[3]+")");
                EscreverSó(" |'5' = 50R$");
                EscreverSó("("+ValoresCedulas()[4]+")");
                Escrever("");
        }

        public void SacarCédulas(int valor){ // Método que retira cédula do banco e, consequentemente, por ironia do destino, adiciona o valor à mão do indivíduo
            banco.Atualizar();
            var valorRetirado = banco.Retirar(valor);
            if(valorRetirado == 0){ // Caso a cédula não esteja disponível
                AguardarTecla("Não foi possível realizar o saque");
            }else{
                valorNaMao += valorRetirado;
                AguardarTecla("Saque efeituado com sucesso");
            }
        }

        public void Depositar(){
            bool executando = true;
            do{
                LimparTela();
                banco.Atualizar();
                Escrever("|Saldo disponível| "+Convert.ToString(banco.valorDisponivel)+" |");
                Escrever("|Dinheiro na mão | "+Convert.ToString(valorNaMao)+" |");
                Escrever("");
                var opt = LerString("Digite a qtd. de cédulas que deseja Depositar \n(Digite 'n' se quiser voltar)(Enter para continuar)");
                Escrever("");
                if(opt == "n"){
                    executando = false;
                    break;
                }
                var valor0 = LerInt("Cédulas de 02,00RS (qtd.) - (Digite 0 para pular)");
                if(valor0 != 0)AdicionarCedulas(valor0,2); // Adicionar {valor0}(qtd.) cédulas de 2 reais
                                            /* (Qtd,Valor) */
                var valor1 = LerInt("Cédulas de 05,00RS (qtd.) - (Digite 0 para pular)");
                if(valor1 != 0)AdicionarCedulas(valor1,5);

                var valor2 = LerInt("Cédulas de 10,00RS (qtd.) - (Digite 0 para pular)");
                if(valor2 != 0)AdicionarCedulas(valor2,10);

                var valor3 = LerInt("Cédulas de 20,00RS (qtd.) - (Digite 0 para pular)");
                if(valor3 != 0)AdicionarCedulas(valor3,20);

                var valor4 = LerInt("Cédulas de 50,00RS (qtd.) - (Digite 0 para pular)");
                if(valor4 != 0)AdicionarCedulas(valor4,50);
            }while(executando == true);
        }

        private void AdicionarCedulas(int qtd, int valor){ // Adicionar objeto Cédula no banco
            if(valor*qtd > valorNaMao){ // Caso o valor em cédulas seja maior que o valor na mão do indivíduo
                LimparTela();
                Escrever("|Dinheiro na mão         | "+Convert.ToString(valorNaMao));
                Escrever("|Valor que quer Depositar| "+Convert.ToString(valor*qtd));
                Escrever("");

                AguardarTecla("Você é pobre, não tem esse dinheiro");
            }else{
                for(int i = 0; i < qtd; i++){ // Para cada rodada, adicionar uma cédula no banco
                    banco.Depositar(new Cedula(valor)); // Adicionar uma cédula com o valor passado como parâmetro
                    valorNaMao -= valor; // Descontar do dinheiro na mão
                }
            }
        }
    }
}
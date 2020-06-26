using System.Collections.Generic;
using System.Linq;
namespace AppBanco.Banco
{
    public class Banco
    {
        public int valorDisponivel{get; private set;}
        public List<Cedula> cedulasDisponiveis{get; private set;}

        public Banco(){
            cedulasDisponiveis = new List<Cedula>();
        }

        public void Atualizar(){
            valorDisponivel = 0;
            foreach(var item in cedulasDisponiveis){
                valorDisponivel += item.Valor;
            }
        }

        public int[] ListarCedulas(){
            var valores = new int[5];
            foreach(var item in cedulasDisponiveis){
                if(item.Valor == 2)
                    valores[0] += 1;
                else if(item.Valor == 5)
                    valores[1] += 1;
                else if(item.Valor == 10)
                    valores[2] += 1;
                else if(item.Valor == 20)
                    valores[3] += 1;
                else if(item.Valor == 50)
                    valores[4] += 1;
            }
            return valores;
        }

        public void Depositar(Cedula cedula){
            cedulasDisponiveis.Add(cedula);
        }

        public int Retirar(int valor){
            var result = cedulasDisponiveis.Where(x => x.Valor == valor).FirstOrDefault();
            if(result != null){
                cedulasDisponiveis.Remove(result);
                return valor;
            }else{
                return 0;
            }
        }
    }
}
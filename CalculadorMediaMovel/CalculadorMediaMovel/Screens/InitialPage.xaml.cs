using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalculadorMediaMovel.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InitialPage : ContentPage
    {
        private static bool primeiraVez = true;
        private static ObservableCollection<int> vetor;
        private static int mediaMovel = 0;
        private static int mediaSimples = 0;
        private static int valorDiario = 0;
        private static int tamanhoDoVetor = 0;


        public InitialPage()
        {
            InitializeComponent();
        }

        private bool coletarDados() 
        {
            bool error = false;

            //Verifica se as entradas estao vazias.
            if ((primeiraVez && entryValorTamanhoVetor.Text == "") || entryValorDiaro.Text == "")
            {
                error = true;
                labelMensagemParaUsuario.Text = "Preencha os valores";
            } 

            //Verifica se as entradas sao numeros
            else if (!stringSomenteNumeros(entryValorTamanhoVetor.Text) || !stringSomenteNumeros(entryValorDiaro.Text))
            {
                error = true;
                labelMensagemParaUsuario.Text = "Somente coloque numeros nas entradas";
                entryValorTamanhoVetor.Text = "";
                entryValorDiaro.Text = "";
            }


            if(error == false)
            {
                
                if (vetor == null)
                {
                    tamanhoDoVetor = int.Parse(entryValorTamanhoVetor.Text);
                    //Inicializando o 'vetor'.
                    //TO DO: Implementar verificacao da variavel 'tamanhoDoVetor' para verificar se o valor esta zero ou negativo.
                    vetor = new ObservableCollection<int>();
                    vetor.CollectionChanged += vetor_MudancaNaLista;
                   
                }

                valorDiario = int.Parse(entryValorDiaro.Text);

            }

            return error;
        }

        private void vetor_MudancaNaLista(object sender, NotifyCollectionChangedEventArgs e)
        {
            calcularMediaMovel();
            calcularMediaSimples();
            mostrarMedias();
        }

        private void popularVetor(int valor, int tamanhaDoVetorAPopular)
        {
            //Populando array 'vetor' com o mesmo valor.
            //TO DO: Verificar se 'valor' esta com valor zero ou negativo.
            //TO DO: Verificar se o vetor foi inicializado.

            for (int i = 0; i < tamanhaDoVetorAPopular - 1; i++)
            {
                vetor.Add(valor);
            }

        }
     
        public void calcularMediaSimples()
        {
            labelMensagemParaUsuario.Text = "Calculando Media Simples";

            int somaDosValores = 0, quantidadeDeValores = vetor.Count;

            for (int i = 0; i < vetor.Count - 1; i++)
            {
                somaDosValores += (int) vetor[i];
            }

            mediaSimples = somaDosValores / quantidadeDeValores;

            labelMensagemParaUsuario.Text = "Terminado Calculando Media Simples";
        }

        public void calcularMediaMovel()
        {
            if (vetor.Count >= 7)
            {          
                labelMensagemParaUsuario.Text = "Calculando Media Movel";

                int somaDosValores = 0, quantidadeDeValores = 7, indexUltimoItem = vetor.Count - 1;

                for (int i = indexUltimoItem; i > indexUltimoItem - quantidadeDeValores; i--)
                {
                    somaDosValores += (int)vetor[i];
                }

                mediaMovel = somaDosValores / quantidadeDeValores;

                labelMensagemParaUsuario.Text = "Terminado Calculando Media Movel";
            }
        }

        private void mostrarMedias()
        {
            labelMensagemParaUsuario.Text = "Media Movel: " + mediaMovel +
                "\nMedia Simples: " + mediaSimples;
        }

        private void colocarNovoValorDiario(int novoValorDiario)
        {
            vetor.Add(novoValorDiario);
        }

        private bool stringSomenteNumeros(string s)
        {
            bool ehNumero = true;

            for (int i = 0; i < s.Length; i++)
            {

                if (!char.IsNumber(s[i]))
                {
                    ehNumero = false;
                    return ehNumero;
                }
            }

            return ehNumero;
        }

        private void botaoEnviar_Clicked(object sender, EventArgs e)
        {
            
            labelMensagemParaUsuario.Text = "";

            if (!coletarDados())
            {
                if (primeiraVez == true)
                {
                    popularVetor(valorDiario, tamanhoDoVetor);
                    labelSubTituloTamanhoVetor.IsEnabled = false;
                    labelSubTituloTamanhoVetor.IsVisible = false;
                    entryValorTamanhoVetor.IsEnabled = false;
                    entryValorTamanhoVetor.IsVisible = false;
                    entryValorTamanhoVetor.Text = "";
                    entryValorDiaro.Text = "";
                    primeiraVez = false;

                }
                else
                {
                    colocarNovoValorDiario(valorDiario);
                    entryValorDiaro.Text = "";
                }
                
            }

        }

        private void botao_reset_Clicked(object sender, EventArgs e)
        {
            primeiraVez = true;
            vetor = null;
            mediaMovel = 0;
            valorDiario = 0;
            labelSubTituloTamanhoVetor.Text = "Informe a quantidade de valore usados na media movel:";
            labelSubTituloTamanhoVetor.IsVisible = true;
            labelSubTituloTamanhoVetor.IsEnabled = true;
            entryValorTamanhoVetor.Text = "";
            entryValorTamanhoVetor.IsEnabled = true;
            entryValorTamanhoVetor.IsVisible = true;
            labelSubTituloValorDiario.Text = "Preencha o valor Diario:";
            entryValorDiaro.Text = "";
            labelMensagemParaUsuario.Text = "";

        }

        private void botao_sair_Clicked(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

    }

}
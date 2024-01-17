using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V118.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Linq.Expressions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumBot
{
    public class AutomationWeb
    {
    // instancinado o driver do selenium 
        public IWebDriver driver;

        public AutomationWeb()
        {
            driver = new ChromeDriver();
        }
        public string TesteWeb()
        {
            try
             {
                //Acessando o site  e procurando um elemento na tela 
                driver.Navigate().GoToUrl("https://10fastfingers.com/typing-test/portuguese");
                var text = driver.FindElement(By.Id("row1")).Text;

                //separando o texto com base nos espaço  
                string[] partesDoTexto = text.Split(" ");

                // Declarando um vetor para armazenar as informações
                string[] vet = new string[partesDoTexto.Length];

                for (int i = 0; i < partesDoTexto.Length; i++)
                {
                    // Armazena a string no vetor
                    vet[i] = partesDoTexto[i];

                    // Seleciona o campo de texto
                    WebElement input = (WebElement)driver.FindElement(By.XPath("//*[@id='inputfield']"));

                    // Insere o texto no campo
                    input.SendKeys(vet[i]);

                    // Pressiona a tecla espaço
                    input.SendKeys(Keys.Space);

                }

                // salvando os resultados
                var ppm = driver.FindElement(By.XPath("//*[@id='wpm']")).Text;   
                var teclas = driver.FindElement(By.XPath("//*[@id='keystrokes']/td[2]/small/span[1]")).Text;
                var precisao = driver.FindElement(By.XPath("//*[@id='accuracy']/td[2]/strong")).Text;
                var palavrasCorretas = driver.FindElement(By.XPath("//*[@id='correct']/td[2]/strong")).Text;
                var palavrasErradas = driver.FindElement(By.XPath("//*[@id='wrong']/td[2]/strong")).Text;


                // criando o objeto ConexaoSql com os resultados para armazer no BD 
                var result = new ConexaoSql
                {
                    ppm = int.Parse(ppm),
                    teclas = int.Parse(teclas),
                    precisao = decimal.Parse(precisao),
                    palavrasCorretas = int.Parse(palavrasCorretas),
                    palavrasErradas = int.Parse(palavrasErradas)
                };

                // Inserindo no banco de dados
                InserirDadosNoBanco(result);

                return text;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Erro ao executar o teste web: " + ex.Message);
                return null;
            }
        }

        // Método para inserir as informações no banco de dados
        private void InserirDadosNoBanco(ConexaoSql resultado)
        {

            try
            {
                string connectionString = "Data Source=DESKTOP-BOQ9G78\\SQLEXPRESS;Initial Catalog=ResultadosDeDigitacao;Integrated Security=True;";

                // Criando uma string de comando SQL
                string insertCommand = @"INSERT INTO ResultadosDeDigitacao (ppm, teclas, precisao, palavrasCorretas, palavrasErradas) 
                                                VALUES (@ppm, @teclas, @precisao, @palavrasCorretas, @palavrasErradas)";

                // Criando uma conexão e um comando SQL
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(insertCommand, connection))
                {
                    // Adicionando parâmetros ao comando SQL e associando o valor da pripriedade 
                    command.Parameters.AddWithValue("@ppm", resultado.ppm);
                    command.Parameters.AddWithValue("@teclas", resultado.teclas);
                    command.Parameters.AddWithValue("@precisao", resultado.precisao);
                    command.Parameters.AddWithValue("@palavrasCorretas", resultado.palavrasCorretas);
                    command.Parameters.AddWithValue("@palavrasErradas", resultado.palavrasErradas);

                    // Executando o comando SQL
                    command.ExecuteNonQuery();
                 }
             }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao inserir dados no banco de dados: " + ex.Message);
            }
        }
        //criando o modelo do banco de dados
        public class ConexaoSql
        {
            public int Id { get; set; }
            public int ppm { get; set; }
            public int teclas { get; set; }
            public decimal precisao { get; set; }
            public int palavrasCorretas { get; set; }
            public int palavrasErradas { get; set; }
        }
    }
}

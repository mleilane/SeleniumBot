using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SeleniumBot
{
    public class AutomationWeb
    {
    // instanciando o driver do selenium 
        public IWebDriver driver;

    //construtor e instancia do Driver do Chrome
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
                Thread.Sleep(6000);

                // Verificando se o anúncio está presente na tela e fechando-o, se necessário
                if (driver.FindElement(By.Id("closeIconHit")).Displayed)
                {
                    driver.FindElement(By.Id("closeIconHit")).Click();
                    // Aguarda um tempo para o anúncio ser fechado
                    Thread.Sleep(3000);
                }
                if (driver.FindElement(By.XPath("//*[@id='fs-slot-footer-wrapper']/button")).Displayed)
                {
                    driver.FindElement(By.XPath("//*[@id='fs-slot-footer-wrapper']/button")).Click();
                    Thread.Sleep(3000);
                }
                if (driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll")).Displayed)
                {
                    driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll")).Click();
                    Thread.Sleep(3000);
                }


                var text = driver.FindElement(By.XPath("//*[@id='words']")).Text;

                //separando o texto com base nos espaço  
                string[] partesDoTexto = text.Split(" ");

                // Declarando um vetor para armazenar as informações
                string[] vet = new string[partesDoTexto.Length];

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                for (int i = 0; i < partesDoTexto.Length; i++)
                {
                    if (stopwatch.Elapsed > TimeSpan.FromMinutes(1))
                    {
                        Console.WriteLine("Tempo excedido. O loop foi interrompido.");
                        break;
                    }
                    // Armazena a string no vetor
                    vet[i] = partesDoTexto[i];

                    // Seleciona o campo de texto
                    WebElement input = (WebElement)driver.FindElement(By.XPath("//*[@id='inputfield']"));
                    

                    // Insere o texto no campo
                    input.SendKeys(vet[i]);
                    

                    // Pressiona a tecla espaço
                    input.SendKeys(Keys.Space);
                    Thread.Sleep(3000);

                }
                stopwatch.Stop();

                Thread.Sleep(2000);

                // salvando os resultados da pontuacao
                var ppm = driver.FindElement(By.XPath("//*[@id='wpm']/strong")).Text;   
                var teclas = driver.FindElement(By.XPath("//*[@id='keystrokes']/td[2]/small/span[1]")).Text;
                var precisao = driver.FindElement(By.XPath("//*[@id='accuracy']/td[2]/strong")).Text;
                var palavrasCorretas = driver.FindElement(By.XPath("//*[@id='correct']/td[2]/strong")).Text;
                var palavrasErradas = driver.FindElement(By.XPath("//*[@id='wrong']/td[2]/strong")).Text;

                // criando o objeto ConexaoSql e atribuindo os valores a variavel result
                var result = new ConexaoSql
                {
                    ppm = ppm,
                    teclas = teclas,
                    precisao = precisao,
                    palavrasCorretas = palavrasCorretas,
                    palavrasErradas = palavrasErradas
                };

                // Chamando o metodo e passando como argumento a variavel result 
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
            //interagindo com o banco de dados
            string connectionString = "Data Source=DESKTOP-BOQ9G78\\SQLEXPRESS,64921;Initial Catalog=master;Integrated Security=True;";

            try
            {

                // Criando uma conexão e um comando SQL / concluindo fecha a conexão com o BD 
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrindo a conexão com o banco de dados
                    connection.Open();

                    // Comando de inserção dos valores nas colunas
                    string insertCommand = @"INSERT INTO dbo.ResultadosDeDigitacao (ppm, teclas, precisao, palavrasCorretas, palavrasErradas) 
                                                VALUES (@ppm, @teclas, @precisao, @palavrasCorretas, @palavrasErradas)";

                    // Criando um comando SQL 
                    using (SqlCommand command = new SqlCommand(insertCommand, connection))
                    {
                        // Adicionando parâmetros ao comando SQL e associando o valor da propriedade 
                        command.Parameters.AddWithValue("@ppm", resultado.ppm);
                        command.Parameters.AddWithValue("@teclas", resultado.teclas);
                        command.Parameters.AddWithValue("@precisao", resultado.precisao);
                        command.Parameters.AddWithValue("@palavrasCorretas", resultado.palavrasCorretas);
                        command.Parameters.AddWithValue("@palavrasErradas", resultado.palavrasErradas);

                        // Executando o comando SQL
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)

            {
                Console.WriteLine("Erro ao inserir dados no banco de dados: " + ex.Message);
            }
        }
        //definindo o modelo do banco de dados
        public class ConexaoSql
        {
            public int Id { get; set; }
            public string ppm { get; set; }
            public string teclas { get; set; }
            public string precisao { get; set; }
            public string palavrasCorretas { get; set; }
            public string palavrasErradas { get; set; }
        }
    }
}

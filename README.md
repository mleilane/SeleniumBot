<h1> SeleniumBot </h1>
<br> 
<h2> Automação Web usando a biblioteca Selenium em C#.</h2>
<h2> Funcionalidades principais:</h2>

- **Acesso ao Site e Identificação de Elementos:** O código acessa o site <a href="https://10fastfingers.com/typing-test/portuguese" > digitação rápida </a>, identifica elementos na tela como anúncios e caixas de diálogo de cookies e fecha-os quando necessário. 
- **Coleta de Texto e Digitação:** Após acessar o site, o código coleta o texto da página e o divide em palavras. Em seguida, ele simula a digitação dessas palavras em um campo de texto, pressionando a tecla espaço entre cada palavra. 
- **Coleta do resultdo e Armazenamento em Banco de Dados:** Após finalizar o teste de digitação, o código coleta as informações do resultado (words per minute (WPM), Keystrokes, Accuracy, Correct words e Wrong words) e armazena esses resultados em um banco de dados SQL Serve.

<h2> Componentes principais:</h2>

- **AutomationWeb Class:** Esta classe contém métodos para realizar o teste de digitação (TesteWeb) e para inserir os resultados no banco de dados (InserirDadosNoBanco). 

- **ConexaoSql Class:** Esta classe define o modelo de dados para os resultados do teste de digitação, incluindo propriedades como PPM, teclas, precisão, palavras corretas e palavras erradas. 

## Bibliotecas Utilizadas

- Selenium.WebDriver.ChromeDriver (v121.0.6167.8500): Utilizada para interagir com o navegador Chrome.
- Selenium.WebDriver (v4.17.0): Utilizada para automação de testes web com Selenium.
- System.Data.SqlClient (v4.8.6): Utilizada para interagir com o banco de dados SQL Server.



<h3>Resumo sobre o desafio: </h3>
<h3>  Realizar a captura das informações no campo indicado abaixo </h3>
<img src= "https://github.com/mleilane/SeleniumCSharpAutomation/blob/master/img/imagem1.png"/>
<br>
<h3>  O site requer que cada palavra capturada seja inserida no campo inferior, e para avançar para a próxima palavra, é necessário pressionar a barra de espaço. </h3>
<img src= "https://github.com/mleilane/SeleniumCSharpAutomation/blob/master/img/imagem2.png"/>
<br>
<h3>  Ao final do teste, o site apresenta os resultados obtidos, e é necessário registrar essas informações em um banco de dados para posterior análise e armazenamento. </h3>
<img src= "https://github.com/mleilane/SeleniumCSharpAutomation/blob/master/img/imagem3.png"/>
<br>

<h3>  Resultados disponiveis no banco de dados: </h3>
<img src= "https://github.com/mleilane/SeleniumCSharpAutomation/blob/master/img/resultado_BD.PNG?raw=true"/>
<br>

##
### Tecnologias utilizadas:
<div style="display: inline_block"><br>
  <img align="center" alt="Maria-C#" height="30" width="40" src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg">
  <img align="center" alt="Maria-SQLserver" height="30" width="40" src="https://raw.githubusercontent.com/devicons/devicon/master/icons/microsoftsqlserver/microsoftsqlserver-plain-wordmark.svg">
  <img align="center" alt="Maria-Selenium" height="30" width="40" src="https://github.com/devicons/devicon/blob/master/icons/selenium/selenium-original.svg">
</div>
<br>

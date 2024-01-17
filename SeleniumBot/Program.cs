using SeleniumBot;
using System;
using System.IO;

// Criando a instancia da classe AutomationWeb()
var web = new AutomationWeb();

// chamando o método TesteWeb() para acessar o site e imprimir o resultado no console 
var text = web.TesteWeb();
Console.WriteLine(text);





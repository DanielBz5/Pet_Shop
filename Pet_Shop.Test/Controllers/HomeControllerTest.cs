using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Pet_Shop.Controllers;
using Pet_Shop.Dao;
using Pet_Shop.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pet_Shop.Test.Controllers
{
    public class HomeControllerTest : IClassFixture<WebApplicationFactory<Startup>>//confg ambiente de inicialização AspNet
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public HomeControllerTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory; //para criar um cliente HTTP
        }

        //[Fact]
        //public async Task Test_MyAction_Returns_OK()// TESTE DO TESTE
        //{
        //    // Crie um cliente HTTP para fazer a solicitação
        //    var client = _factory.CreateClient();

        //    // Faça uma solicitação HTTP para o método de ação que você deseja testar
        //    var response = await client.GetAsync("/api/mycontroller/myaction");

        //    // Verifique se a resposta tem um status HTTP 200 OK
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //    // Verifique se o conteúdo da resposta, se aplicável
        //    var content = await response.Content.ReadAsStringAsync();
        //    Assert.Contains("Expected Content", content);
        //}

        [Fact(DisplayName = "Request Home/Index retorna View com Sucesso")]
        public void HomeIndex_ReturnView_Sucesso()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal("Index", result.ViewName); 
        }

        [Fact(DisplayName = "Request Home/Login retorna View com Sucesso")]
        public void HomeLogin_ReturnView_Sucesso()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Login() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal("_Login", result.ViewName); 
        }

        [Fact(DisplayName = "IncluirRegister com ModelState Invalido retorna View Register")]
        public void IncluiRegister_ModelStateInvalid_ReturnView()
        {
            // Arrange
            var controller = new HomeController(); 
            controller.ModelState.AddModelError("Nome", "Campo obrigatório");//cria um model state não valiado

            // Act
            var result = controller.IncluiRegister(new Cliente(), new Pet()) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("_Register", result.ViewName);
        }

        [Fact(DisplayName = "IncluirRegister com ModelState Valido e Insert True retorna View Login")]
        public void IncluiRegister_InsertTrue_ReturnLogin()// Teste de Integração que faz Insert no Banco
        {
            // Arrange
            var controller = new HomeController();
            var cliente = new Cliente()
            { Cpf = "99999999999", Nome = "TeteAuto", Senha = "999", Telefone = "999999999", Endereco = "TesTeAuto" };
            var pet = new Pet()
            {Nome = "TesteAuto", Especie = "TesteAuto", Raca = "TesteAuto" };
            var homedao = new HomeDao();
            controller.homedao = homedao;

            // Act
            var result = controller.IncluiRegister(cliente, pet) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("_Login", result.ViewName);
        }

        [Fact(DisplayName = "IncluirRegister com ModelState Valido e Insert False retorna View Register")]
        public void IncluiRegister_InsertFalse_ReturnRegister()// Teste de Integração que faz Insert no Banco
        {
            // Arrange
            var controller = new HomeController();
            var cliente = new Cliente()
            { Cpf = "99999999999", Nome = "TeteAuto", Senha = "999", Telefone = "999999999", Endereco = "TesTeAuto" };
            var pet = new Pet()
            { Nome = "TesteAuto", Especie = "TesteAuto", Raca = "TesteAuto" };
            var homedao = new HomeDao();
            controller.homedao = homedao;

            // Act
            var result = controller.IncluiRegister(cliente, pet) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("_Register", result.ViewName);
        }

        [Fact(DisplayName =  "Post Login com Usuario Valido Autoriza acesso e Retorna View Agendamento")]
        public async Task PostLogin_UsuarioValido_AutorizaAsync()
        {
            // Arrange
            var client = _factory.CreateClient();

            var cliente = new Cliente { Nome = "Daniel", Senha = "123"};

            
            var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "Nome", cliente.Nome },
                { "Senha", cliente.Senha },
            });

            // Act
            var response = await client.PostAsync("/Home/Login", formContent);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal("/Agendamento", response.Headers.Location.OriginalString);

        }
    }

}





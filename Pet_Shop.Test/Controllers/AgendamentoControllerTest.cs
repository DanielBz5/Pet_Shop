using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pet_Shop.Controllers;
using Pet_Shop.Dao;
using Pet_Shop.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Pet_Shop.Test.Controllers
{
    public class AgendamentoControllerTest
    {
        AgendamentoController controller;
        AgendamentoDao agendamentodao;
        public AgendamentoControllerTest()
        {
            controller = new AgendamentoController();
            agendamentodao = new AgendamentoDao();
        }

        [Fact(DisplayName = "Post Agendamento sem usuario autenticado Retorna View Error")]
        public void PostAgendamento_LoginInvalid_ReturnError()
        {
            // Arrange
            Servicos servicos = new Servicos();
            Agendamento agendamento = new Agendamento();

            controller.ControllerContext = new ControllerContext //simula solicitação HTTP sem usuario autenticado
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var result = controller.Agendamento(servicos, agendamento) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Error", result.ViewName);
        }

        [Fact(DisplayName = "Post Agendamento com usuario autenticado e Insert True Retorna View AgendamentoSucesso")]
        public void PostAgendamento_InsertTrue_ReturnAgendamentoSucesso()
        {
            // Arrange
            Servicos servicos = new Servicos() { Cod = 1 };
            Agendamento agendamento = new Agendamento { Data = new DateTime(2023, 11, 12, 13, 21, 0) };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Daniel"),
                new Claim(ClaimTypes.NameIdentifier, "123") 
            };

            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext // simula solicitação HTTP com usuario autenticado
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            // Act
            var result = controller.Agendamento(servicos, agendamento) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("AgendamentoSucesso", result.ViewName);
        }

        [Theory(DisplayName ="Get DeleteAgendamento Busca Agendamentos e Retrona View Delete com Agendamento a Deletar")]
        [InlineData(2)]//Arrange
        [InlineData(3)]
        [InlineData(5)]
        public void DeleteAgendamento_BuscaAgendamento_RetornaViewcomDelete(int Cod)//Esse metodo vai testar os valores passados no Inlinedata
        {
            //Act
            var result = controller.DeleteAgendamento(Cod) as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(null, result.ViewName);

            Assert.NotNull(result.Model);
            Assert.IsType<Agendamento>(result.Model);
        }


        [Fact(DisplayName ="Get Agenda Busca Agendamentos com Sucesso e Retorna View Agenda com ListaAgenda")]
        public void Agenda_BuscaSucesso_RetornaViewcomListaAgenda()
        {
            //Act
            var result = controller.Agenda() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(null, result.ViewName);

            Assert.NotNull(result.Model);
            Assert.IsType<List<Agendamento>>(result.Model);

        }
       
    }
}

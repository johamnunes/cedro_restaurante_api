using CedroRestaurante.ApplicationService.Services;
using CedroRestaurante.DataObjects.Models;
using CedroRestaurante.Persistence.Data;
using CedroRestaurante.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CedroRestaurante.Tests
{
    [TestClass]
    public class PratoTest
    {
        PratoService service;
        RestauranteService restauranteService;
        DataContext context;

        void InitContext()
        {
            DbContextOptionsBuilder<DataContext> builder = new DbContextOptionsBuilder<DataContext>()
               .UseSqlServer("Data Source=LAPTOP-DO-JOHAM;Initial Catalog=cedro_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
               .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            context = new DataContext(builder.Options);
            service = new PratoService(context);
            restauranteService = new RestauranteService(context);
        }

        Prato MakeEntity()
        {
            Restaurante restaurante = restauranteService.Get().Where(x => x.Nome.Contains("UNIT TEST")).FirstOrDefault();
            return new Prato
            {
                Descricao = $"PRATO UNIT TEST {DateTime.Now}",
                Valor = 9.99M,
                RestauranteId = restaurante.Id
            };
        }

        [TestMethod]
        [TestCategory("Prato")]
        public void Salva_Prato()
        {
            InitContext();
            Prato entity = MakeEntity();
            entity = service.Add(entity);
            Assert.AreNotEqual(null, entity, string.Join(";", Notification.Messages));
        }

        [TestMethod]
        [TestCategory("Prato")]
        public void Nao_Salva_Prato_Incompleto()
        {
            InitContext();
            Prato entity = MakeEntity();
            entity.Descricao = null;
            entity = service.Add(entity);
            Assert.AreEqual(null, entity, string.Join(";", Notification.Messages));
        }

        [TestMethod]
        [TestCategory("Prato")]
        public void Atualiza_Restaurante()
        {
            InitContext();
            Prato entity = service.Get().Find(x => x.Descricao.Contains("UNIT TEST"));
            if (entity == null)
            {
                entity = MakeEntity();
                entity = service.Add(entity);
            }
            entity.Descricao = $"PRATO UNIT TEST MODIFY {DateTime.Now}";
            entity = service.Update(entity.Id, entity);
            Assert.AreNotEqual(null, entity, string.Join(";", Notification.Messages));
        }

        [TestMethod]
        [TestCategory("Prato")]
        public void Nao_Atualiza_Prato_Incompleto()
        {
            InitContext();
            Prato entity = service.Get().Find(x => x.Descricao.Contains("UNIT TEST"));
            if (entity == null)
            {
                entity = MakeEntity();
                entity = service.Add(entity);
            }
            entity.Descricao = null;
            entity = service.Update(entity.Id, entity);
            Assert.AreEqual(null, entity, string.Join(";", Notification.Messages));
        }

        [TestMethod]
        [TestCategory("Prato")]
        public void Obter_Todos_Pratod()
        {
            InitContext();
            List<Prato> restaurantes = service.Get();
            Assert.AreNotEqual(null, restaurantes, string.Join(";", Notification.Messages));
        }

        [TestMethod]
        [TestCategory("Prato")]
        public void Obter_Prato_Por_Id()
        {
            InitContext();
            Prato entidadeTeste = service.Get().Find(x => x.Descricao.Contains("UNIT TEST"));
            Prato prato = service.Get(entidadeTeste.Id);
            Assert.AreNotEqual(null, prato, string.Join(";", Notification.Messages));
        }

        [TestMethod]
        [TestCategory("Prato")]
        public void Remove_Prato()
        {
            InitContext();
            Prato entidadeTeste = service.Get().Find(x => x.Descricao.Contains("UNIT TEST"));
            bool resposta = service.Delete(entidadeTeste.Id);
            Assert.AreNotEqual(false, resposta, string.Join(";", Notification.Messages));
        }
    }
}

using CedroRestaurante.ApplicationService.Services;
using CedroRestaurante.DataObjects.Models;
using CedroRestaurante.Persistence.Data;
using CedroRestaurante.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CedroRestaurante.Tests
{
    [TestClass]
    public class RestauranteTest
    {
        RestauranteService service;
        DataContext context;

        void InitContext()
        {
            DbContextOptionsBuilder<DataContext> builder = new DbContextOptionsBuilder<DataContext>()
               .UseSqlServer("Data Source=LAPTOP-DO-JOHAM;Initial Catalog=cedro_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
               .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            context = new DataContext(builder.Options);
            service = new RestauranteService(context);
        }

        Restaurante MakeEntity()
        {
            return new Restaurante
            {
                Nome = $"RESTAURANTE UNIT TEST {DateTime.Now}"
            };
        }

        [TestMethod]
        [TestCategory("Restaurante")]
        public void Salva_Restaurante()
        {
            InitContext();
            Restaurante entity = MakeEntity();
            entity = service.Add(entity);
            Assert.AreNotEqual(null, entity, string.Join(";", Notification.Messages));
        }

        [TestMethod]
        [TestCategory("Restaurante")]
        public void Nao_Salva_Restaurante_Incompleto()
        {
            InitContext();
            Restaurante entity = MakeEntity();
            entity.Nome = null;
            entity = service.Add(entity);
            Assert.AreEqual(null, entity, string.Join(";", Notification.Messages));
        }

        [TestMethod]
        [TestCategory("Restaurante")]
        public void Atualiza_Restaurante()
        {
            InitContext();
            Restaurante entity = service.Get().Find(x => x.Nome.Contains("UNIT TEST"));
            if (entity == null)
            {
                entity = MakeEntity();
                entity = service.Add(entity);
            }
            entity.Nome = $"RESTAURANTE UNIT TEST MODIFY {DateTime.Now}";
            entity = service.Update(entity.Id, entity);
            Assert.AreNotEqual(null, entity, string.Join(";", Notification.Messages));
        }

        [TestMethod]
        [TestCategory("Restaurante")]
        public void Nao_Atualiza_Restaurante_Incompleto()
        {
            InitContext();
            Restaurante entity = service.Get().Find(x => x.Nome.Contains("UNIT TEST"));
            if (entity == null)
            {
                entity = MakeEntity();
                entity = service.Add(entity);
            }
            entity.Nome = null;
            entity = service.Update(entity.Id, entity);
            Assert.AreEqual(null, entity, string.Join(";", Notification.Messages));
        }

        [TestMethod]
        [TestCategory("Restaurante")]
        public void Obter_Todos_Restaurantes()
        {
            InitContext();
            List<Restaurante> restaurantes = service.Get();
            Assert.AreNotEqual(null, restaurantes, string.Join(";", Notification.Messages));
        }

        [TestMethod]
        [TestCategory("Restaurante")]
        public void Obter_Restaurante_Por_Id()
        {
            InitContext();
            Restaurante entidadeTeste = service.Get().Find(x => x.Nome.Contains("UNIT TEST"));
            Restaurante restaurante = service.Get(entidadeTeste.Id);
            Assert.AreNotEqual(null, restaurante, string.Join(";", Notification.Messages));
        }

        [TestMethod]
        [TestCategory("Restaurante")]
        public void Remove_Restaurante()
        {
            InitContext();
            Restaurante entidadeTeste = service.Get().Find(x => x.Nome.Contains("UNIT TEST"));
            bool resposta = service.Delete(entidadeTeste.Id);
            Assert.AreNotEqual(false, resposta, string.Join(";", Notification.Messages));
        }
    }
}

using CedroRestaurante.ApplicationService.Helpers;
using CedroRestaurante.DataObjects.Models;
using CedroRestaurante.Persistence.Data;
using CedroRestaurante.Persistence.Repositories;
using CedroRestaurante.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CedroRestaurante.ApplicationService.Services
{
    public class PratoService : IService<Prato>
    {
        IBaseRepository<Prato> pratoRepository;
        DataContext context;

        public PratoService(DataContext context)
        {
            pratoRepository = new BaseRepository<Prato>(context);
            this.context = context;
        }

        public Prato Add(Prato entity)
        {
            if (entity == null)
            {
                Notification.CreateMessage(HttpStatusCode.BadRequest, DefaultMessage.NENHUM_DADO);
                return null;
            }

            List<string> validation = DataValidation.Validate(entity).ToList();
            if (validation.Count == 0)
            {
                Prato pratoExistente = pratoRepository.Get(x => x.Descricao == entity.Descricao && x.RestauranteId == entity.RestauranteId).FirstOrDefault();
                if (pratoExistente != null)
                {
                    Notification.CreateMessage(HttpStatusCode.Conflict, string.Format(DefaultMessage.DADO_EXISTENTE, "um prato", "a descricao e o restaurante informados"));
                    return null;
                }
                pratoRepository.Add(entity);
                pratoRepository.SaveAll();
                return entity;
            }
            else
            {
                Notification.CreateMessage(HttpStatusCode.BadRequest, validation);
                return null;
            }
        }

        public bool Delete(string id)
        {
            Prato prato = Get(id);
            if (prato == null)
                return false;

            pratoRepository.Delete(prato);
            pratoRepository.SaveAll();
            return true;
        }

        public List<Prato> Get() => pratoRepository.Get().Include("Restaurante").ToList();

        public Prato Get(string id)
        {
            Prato prato = pratoRepository.Get(x => x.Id == id).FirstOrDefault();
            if (prato == null)
            {
                Notification.CreateMessage(HttpStatusCode.NotFound, string.Format(DefaultMessage.NAO_ENCONTRADO, "Prato"));
                return null;
            }
            return prato;
        }

        public Prato Update(string id, Prato entity)
        {
            if (entity == null)
            {
                Notification.CreateMessage(HttpStatusCode.BadRequest, DefaultMessage.NENHUM_DADO);
                return null;
            }

            Prato prato = pratoRepository.Get(x => x.Id == id).FirstOrDefault();
            if (prato == null)
            {
                Notification.CreateMessage(HttpStatusCode.NotFound, string.Format(DefaultMessage.NAO_ENCONTRADO, "Prato"));
                return null;
            }

            List<string> validation = DataValidation.Validate(entity).ToList();
            if (validation.Count == 0)
            {
                Prato pratoExistente = pratoRepository.Get(x => x.Descricao == entity.Descricao && x.RestauranteId == entity.RestauranteId && x.Id != id).FirstOrDefault();
                if (pratoExistente != null)
                {
                    Notification.CreateMessage(HttpStatusCode.Conflict, string.Format(DefaultMessage.DADO_EXISTENTE, "um prato", "a descrição e o restaurante informados"));
                    return null;
                }

                PatchHelper.Patch(prato, entity);
                pratoRepository.Update(prato);
                pratoRepository.SaveAll();
                return entity;
            }
            else
            {
                Notification.CreateMessage(HttpStatusCode.BadRequest, validation);
                return null;
            }
        }
    }
}

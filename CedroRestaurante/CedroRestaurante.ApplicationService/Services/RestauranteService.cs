using CedroRestaurante.ApplicationService.Helpers;
using CedroRestaurante.DataObjects.Models;
using CedroRestaurante.Persistence.Data;
using CedroRestaurante.Persistence.Repositories;
using CedroRestaurante.Services.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CedroRestaurante.ApplicationService.Services
{
    public class RestauranteService : IService<Restaurante>
    {
        IBaseRepository<Restaurante> restauranteRepository;
        DataContext context;

        public RestauranteService(DataContext context)
        {
            restauranteRepository = new BaseRepository<Restaurante>(context);
            this.context = context;
        }

        public Restaurante Add(Restaurante entity)
        {
            if (entity == null)
            {
                Notification.CreateMessage(HttpStatusCode.BadRequest, DefaultMessage.NENHUM_DADO);
                return null;
            }

            List<string> validation = DataValidation.Validate(entity).ToList();
            if (validation.Count == 0)
            {
                Restaurante restauranteExistente = restauranteRepository.Get(x => x.Nome == entity.Nome).FirstOrDefault();
                if (restauranteExistente != null)
                {
                    Notification.CreateMessage(HttpStatusCode.Conflict, string.Format(DefaultMessage.DADO_EXISTENTE, "um restaurante", "o nome informado"));
                    return null;
                }
                restauranteRepository.Add(entity);
                restauranteRepository.SaveAll();
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
            Restaurante restaurante = Get(id);
            if (restaurante == null)
                return false;

            restauranteRepository.Delete(restaurante);
            restauranteRepository.SaveAll();
            return true;
        }

        public List<Restaurante> Get() => restauranteRepository.Get().ToList();

        public Restaurante Get(string id)
        {
            Restaurante restaurante = restauranteRepository.Get(x => x.Id == id).FirstOrDefault();
            if (restaurante == null)
            {
                Notification.CreateMessage(HttpStatusCode.NotFound, string.Format(DefaultMessage.NAO_ENCONTRADO, "Restaurante"));
                return null;
            }
            return restaurante;
        }

        public Restaurante Update(string id, Restaurante entity)
        {
            if (entity == null)
            {
                Notification.CreateMessage(HttpStatusCode.BadRequest, DefaultMessage.NENHUM_DADO);
                return null;
            }

            Restaurante restaurante = restauranteRepository.Get(x => x.Id == id).FirstOrDefault();
            if (restaurante == null)
            {
                Notification.CreateMessage(HttpStatusCode.NotFound, string.Format(DefaultMessage.NAO_ENCONTRADO, "Restaurante"));
                return null;
            }

            List<string> validation = DataValidation.Validate(entity).ToList();
            if (validation.Count == 0)
            {
                Restaurante restauranteExistente = restauranteRepository.Get(x => x.Nome == entity.Nome && x.Id != id).FirstOrDefault();
                if (restauranteExistente != null)
                {
                    Notification.CreateMessage(HttpStatusCode.Conflict, string.Format(DefaultMessage.DADO_EXISTENTE, "um restaurante", "o nome informado"));
                    return null;
                }

                PatchHelper.Patch(restaurante, entity);
                restauranteRepository.Update(restaurante);
                restauranteRepository.SaveAll();
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

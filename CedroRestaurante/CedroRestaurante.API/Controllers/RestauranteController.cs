using CedroRestaurante.API.Helpers;
using CedroRestaurante.ApplicationService.Services;
using CedroRestaurante.DataObjects.Models;
using CedroRestaurante.Persistence.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CedroRestaurante.API.Controllers
{
    [Produces("application/json")]
    [Route("api/restaurante")]
    public class RestauranteController : Controller
    {
        RestauranteService service;

        public RestauranteController(DataContext context)
        {
            service = new RestauranteService(context);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                return await MessageHelper.CreateMessageAsync(HttpStatusCode.OK, service.Get());
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError(e.Message.ToString());
                return MessageHelper.CreateMessage(HttpStatusCode.InternalServerError, e.Message.ToString());
            }
        }

        [HttpGet("{id}")]
        public async Task<HttpResponseMessage> Get(string id)
        {
            try
            {
                Restaurante response = service.Get(id);
                if (response == null)
                    return await MessageHelper.CreateMessageAsync();
                else
                    return await MessageHelper.CreateMessageAsync(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError(e.Message.ToString());
                return MessageHelper.CreateMessage(HttpStatusCode.InternalServerError, e.Message.ToString());
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] Restaurante entity)
        {
            try
            {
                Restaurante response = service.Add(entity);
                if (response == null)
                    return await MessageHelper.CreateMessageAsync();
                else
                    return await MessageHelper.CreateMessageAsync(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError(e.Message.ToString());
                return MessageHelper.CreateMessage(HttpStatusCode.InternalServerError, e.Message.ToString());
            }
        }

        [HttpPut("{id}")]
        public async Task<HttpResponseMessage> Put(string id, [FromBody] Restaurante entity)
        {
            try
            {
                Restaurante response = service.Update(id, entity);
                if (response == null)
                    return await MessageHelper.CreateMessageAsync();
                else
                    return await MessageHelper.CreateMessageAsync(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError(e.Message.ToString());
                return MessageHelper.CreateMessage(HttpStatusCode.InternalServerError, e.Message.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<HttpResponseMessage> Delete(string id)
        {
            try
            {
                bool response = service.Delete(id);
                if (!response)
                    return await MessageHelper.CreateMessageAsync();
                else
                    return await MessageHelper.CreateMessageAsync(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError(e.Message.ToString());
                return MessageHelper.CreateMessage(HttpStatusCode.InternalServerError, e.Message.ToString());
            }
        }
    }
}
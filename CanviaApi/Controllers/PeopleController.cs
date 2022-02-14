using AutoMapper;
using Canvia.Core.Interfaces;
using Canvia.Model;
using CanviaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CanviaApi.Controllers
{
    public class PeopleController : ApiController
    {
        #region Fields

        private readonly IPeopleService Service;

        #endregion

        #region Constructor

        public PeopleController(IPeopleService peopleService)
        {
            this.Service = peopleService;
        }

        #endregion

        // GET: api/People/5
        [Route("{id}")]
        [HttpGet()]
        public async Task<PersonDto> GetById(int id)
        {
            var result = await this.Service.GetById(id);
            return result != null ? Mapper.Map<PersonDto>(result) : new PersonDto();
        }

        [HttpPut()]
        public async Task<int> Person([FromBody] PersonDto value)
        {
            return await this.Service.Save(Mapper.Map<Person>(value));
        }

        [Route("{id}")]
        [HttpPost()]
        public async Task<int> Update(int id, [FromBody] PersonDto value)
        {
            return await this.Service.Update(id, Mapper.Map<Person>(value));
        }

        [Route("all")]
        [HttpGet]
        public IEnumerable<PersonDto> GetAll()
        {
            var result = this.Service.GetAll();
            return result != null ? Mapper.Map<IEnumerable<PersonDto>>(result) : new List<PersonDto>();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<int> Delete(int id)
        {
            return await this.Service.Delete(id);
        }

        [Route("delete-table/{id}")]
        [HttpDelete]
        public async Task<int> DeleteTable(int id)
        {
            return await this.Service.DeleteTable(id);
        }
    }
}

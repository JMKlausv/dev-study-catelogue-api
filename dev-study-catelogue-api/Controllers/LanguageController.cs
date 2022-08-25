﻿using Application.Languages.Commands.CreateLanguage;
using Application.Languages.Commands.DeleteLanguage;
using Application.Languages.Queries.GetAllLanguages;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dev_study_catelogue_api.Controllers
{
   
    public class LanguageController : ApiControllerBase
    {
        // GET: api/<LanguageController>
        [HttpGet]
        public async Task<IEnumerable<LanguageDto>> Get()
        {
            return await Mediator.Send(new GetAllLanguagesQuery());
        }

/*
        // GET api/<LanguageController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
  */
        // POST api/<LanguageController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLanguageCommand  command)
        {
            return  Ok( await Mediator.Send(command));
        }
 /*
        // PUT api/<LanguageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
 */

        // DELETE api/<LanguageController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteLanguageCommand
            {
                Id = id
            };
            return Ok(await Mediator.Send(command));
        }
    }
}
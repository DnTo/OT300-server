﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using System.Linq;
using System.Threading.Tasks;
using System;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Business;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class NewsController : ControllerBase
    {
        private readonly INewsBusiness _newsService;

        public NewsController(INewsBusiness service)
        {
            _newsService = service;
        }

        [HttpGet("/api/news")]
        public IActionResult GetAllNews()
        {
            var news = _newsService.GetAll();
            return Ok(news);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByIdNews(int id)
        {
            News news = await _newsService.GetById(id);

            if (news is null)
            {
                return NotFound("New does not exist");
            }

            return Ok(news);
        }

        [HttpPost]       
        public async Task<IActionResult> InsertNews([FromForm] NewsPostDTO dto, [Required]IFormFile imageFile)
        {
            dto.ImageFile =imageFile.OpenReadStream();

            NewsFullDTO created = await _newsService.Add(dto);

            if (created.IdCategory == 0)
                return Conflict($"Category doesn't exists : {dto.IdCategory} ");
            if (created.Id == 0)
                return Conflict($"There was an error");
            return Created("", created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews(int id, [FromQuery] News news)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                }
                else
                {
                    var result = await _newsService.Update(news);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {   
            if (await _newsService.GetById(id) is null)
            {
                return NotFound();
            }
            await _newsService.Delete(id);

            return Ok("Deleted succesfully");
        }
    }
}

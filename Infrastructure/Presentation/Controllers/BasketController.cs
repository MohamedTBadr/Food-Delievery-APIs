﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.DataTransferObject.Basket;

namespace Presentation.Controllers
{
    public class BasketController(IServiceManager serviceManger): APIController
    {
        //get Basket By Id
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> Get(string id)
        {
            var Basket=serviceManger.BasketService.GetAsync(id);
            return Ok(Basket);
        }

        //Update Basket

        [HttpPost]
        public async Task<ActionResult<BasketDTO>> Update(BasketDTO BasketDto)
        {
            //var basket = await serviceManger.BasketService.UpdateBasketAsync(BasketDto);
            var basket = await serviceManger.BasketService.UpdateAsync(BasketDto);
            return Ok(basket);

        }


        //Delete Basket
        [HttpDelete("{id}")]
        public async Task<ActionResult<BasketDTO>> Delete(string Id)
        {
             await serviceManger.BasketService.DeleteAsync(Id);
            return NoContent();
        }


    }
}

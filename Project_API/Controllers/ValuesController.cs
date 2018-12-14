﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project_Infastructure.services;
using Project_Infastructure.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Project_API.Controllers
{
	[Route("api/Hampers")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private IDataService<Hamper> _hamperService;

		private IDataService<Product> _productService;

		private IDataService<Category> _categoryService;

		private IDataService<HamperProduct> _hpService;

		public ValuesController(IDataService<Hamper> hamperService,
								IDataService<Product> productService,
								IDataService<HamperProduct> hpService,
								IDataService<Category> categoryService)
		{
			_hamperService = hamperService;
			_productService = productService;
			_hpService = hpService;
			_categoryService = categoryService;
		}

		// GET api/values
		[HttpGet]
		public ActionResult<string> Get()
		{
			IList<Hamper> hampers = _hamperService.GetAll().ToList();



			return JsonConvert.SerializeObject(hampers);
		}

		// GET api/values/5
		

		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
		
			var hamper = _hamperService.Query(h=> h.CategoryId == id);
			if(hamper == null)
			{
				return NotFound(id);
			}

			return JsonConvert.SerializeObject(hamper);
		}
		[HttpGet("search/{q}")]
		public ActionResult<string> Get(string q)
		{
			var hamper = _hamperService.Query(h => h.HamperName.ToLower().Contains(q.ToLower()));

			if(hamper == null)
			{
				return NotFound(q);
			}
			return JsonConvert.SerializeObject(hamper);
		}
		[HttpGet("categories")]
		public ActionResult<string> GetCats()
		{
			var cats = _categoryService.GetAll();

			if (cats == null)
			{
				return NotFound();
			}
			return JsonConvert.SerializeObject(cats);
		}
		[HttpGet("products/{id}")]
		public ActionResult<string> GetProducts(int id)
		{
			var hps = _hpService.GetAll().
				Include(p => p.Product)
				.Where(prd => prd.HamperId == id)
				.Select(pr => pr.Product);

			return JsonConvert.SerializeObject(hps);
		}
	}
}

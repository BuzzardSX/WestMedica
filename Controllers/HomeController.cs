using System;
using static System.IntPtr;
using static System.Console;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Drawing.Image;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WestMedica.Models;
using WestMedica.ViewModels.HomeViewModels;

namespace WestMedica.Controllers
{
	public class HomeController : Controller
	{
		private readonly IImageCropService _cropService;
		public HomeController(IImageCropService cropService) => _cropService = cropService;
		public IActionResult Index()
		{
			var model = new IndexViewModel
			{
				ImageSource = "https://cdn.wallscloud.net/converted/1427032223-listya-pozadi-ptitsi-jK24-1600x1200-MM-100.jpg"
			};
			return View(model);
		}
		[HttpPost]
		public IActionResult Index(IndexViewModel model)
		{
			_cropService.Load(model.ImageSource, model.Rows, model.Columns);
			model.IsImageLoaded = _cropService.IsImageLoaded;
			model.Segments = new IndexSegmentsViewModel
			{
				Count = _cropService.Rows * _cropService.Columns,
				SegmentHeight = _cropService.SegmentHeight,
				SegmentWidth = _cropService.SegmentWidth
			};
			return View(model);
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

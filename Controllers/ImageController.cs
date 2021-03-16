using Microsoft.AspNetCore.Mvc;
using WestMedica.Models;

namespace WestMedica.Controllers
{
	public class ImageController : Controller
	{
		private readonly IImageCropService _cropService;
		public ImageController(IImageCropService cropService) => _cropService = cropService;
		public IActionResult Segment(int part) => File(fileContents: _cropService.GetSegment(part), contentType: "image/png");
	}
}

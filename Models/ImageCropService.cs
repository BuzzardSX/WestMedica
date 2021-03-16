using System;
using static System.Console;
using System.Collections.Generic;
using System.Drawing;
using static System.Drawing.Bitmap;
using static System.Drawing.Graphics;
using static System.Drawing.Image;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Hosting;

namespace WestMedica.Models
{
	public class ImageCropService : IImageCropService
	{
		private Bitmap _image;
		public bool IsImageLoaded => _image != null;
		public int Rows { get; private set; }
		public int Columns { get; private set; }
		public int SegmentHeight { get; private set; }
		public int SegmentWidth { get; private set; }
		public void Load(string source, int rows, int columns)
		{
			using (var webClient = new WebClient())
			{
				using (var stream = webClient.OpenRead(source))
				{
					_image = FromStream(stream) as Bitmap;
				}
			}
			Rows = rows;
			Columns = columns;
			SegmentHeight = _image.Height / Rows;
			SegmentWidth = _image.Width / Columns;
		}
		public byte[] GetSegment(int part)
		{
			var rect = new Rectangle
			{
				X = ((part + Columns - 1) % Columns) * SegmentWidth,
				Y = ((part - 1) / Columns) * SegmentHeight,
				Width = SegmentWidth,
				Height = SegmentHeight
			};
			lock (_image)
			{
				using (var segment = _image.Clone(rect, _image.PixelFormat))
				{
					using (var graphics = FromImage(segment))
					{
						var font = new Font(FontFamily.GenericSansSerif, 12);
						var brush = new SolidBrush(Color.Red);
						graphics.DrawString($"{rect.X},{rect.Y}", font, brush, 0, 0);
					}
					using (var memory = new MemoryStream())
					{
						segment.Save(memory, ImageFormat.Png);
						return memory.ToArray();
					}
				}
			}
		}
	}
}

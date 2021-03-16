using System.Collections.Generic;
using System.Drawing;

namespace WestMedica.Models
{
	public interface IImageCropService
	{
		bool IsImageLoaded { get; }
		int Rows { get; }
		int Columns { get; }
		int SegmentHeight { get; }
		int SegmentWidth { get; }
		void Load(string source, int rows, int columns);
		byte[] GetSegment(int part);
	}
}

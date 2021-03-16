using System.ComponentModel.DataAnnotations;

namespace WestMedica.ViewModels.HomeViewModels
{
	public class IndexViewModel
	{
		[Display(Name = "URL картинки", Prompt = "Введите URL картинки")]
		[DataType(DataType.Url)]
		public string ImageSource { get; set; }
		[Display(Name = "Количество строк", Prompt = "1")]
		[Range(1, int.MaxValue)]
		public int Rows { get; set; } = 1;
		[Display(Name = "Количество столбцов", Prompt = "1")]
		[Range(1, int.MaxValue)]
		public int Columns { get; set; } = 1;
		public bool IsImageLoaded { get; set; }
		public IndexSegmentsViewModel Segments { get; set; }
	}
}

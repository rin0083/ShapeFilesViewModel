using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls.Map;

namespace ShapeFilesViewModel
{
	public partial class MainPage : UserControl
	{
		private ShapefileViewModel Model
		{
			get
			{
				return this.Resources["DataContext"] as ShapefileViewModel;
			}
		}

		public MainPage()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (this.Model.Region != "africa")
			{
				this.Model.Region = "africa";
			}
			else
			{
				this.Model.Region = "south_america";
			}
		}

		private void MapShapeReader_PreviewReadCompleted(object sender, PreviewReadShapesCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				MessageBox.Show(e.Error.Message);
			}
			else
			{
				// e.Items contains the list of objects which are created by MapShapeReader
				foreach (object item in e.Items)
				{
					MapShape shape = item as MapShape;
					if (shape != null)
					{
						shape.Fill = new SolidColorBrush(Color.FromArgb(0x7f, 0x1f, 0x7f, 0x3f));
						shape.StrokeThickness = 3;
						shape.Stroke = new SolidColorBrush(Colors.Blue);
					}
				}
			}
		}

		private void MapShapeReader_ReadCompleted(object sender, ReadShapesCompletedEventArgs e)
		{
			if (e.Error == null)
			{
				LocationRect bestView = this.informationLayer.GetBestView(this.informationLayer.Items);
				this.RadMap1.SetView(bestView);
			}
		}
	}
}

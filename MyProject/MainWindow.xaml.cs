using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;


namespace MyProject
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		bool sirB =  false;
		bool sirP =  false;
		bool isSave = false;
		bool isRenameTitle = false;
		string filename = "";

		public MainWindow()
		{
			InitializeComponent();
			panel_file.Visibility = Visibility.Hidden;
		}


		public string saveasfile() {
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			if (saveFileDialog.ShowDialog() == false)
				return "";
			// получаем выбранный файл
			string filename = saveFileDialog.FileName;
			// сохраняем текст в файл
			System.IO.File.WriteAllText(filename, textInput.Text);
			panel_file_visible(false);
			isRenameTitle = false;
			return filename;

		}

		public bool wantyousavefile() {
			if (MessageBox.Show("Save this file?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				return true;
			}
			else {
				return false;
			}
		}



		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
		}

		public void panel_file_visible(bool bo) {

			if (bo)
				panel_file.Visibility = Visibility.Visible;
			else
				panel_file.Visibility = Visibility.Hidden;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			my_Window.Close();
		}


		private void my_Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton.ToString() == "Pressed") {
				my_Window.DragMove();
			}
		}


		private void my_Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (my_Window.Height < 200) {
				my_Window.Height = 200;
				return;
			}
			
			if (my_Window.Width < 280)
			{
				my_Window.Width = 280;
				return;
			}

			textInput.Width = my_Window.Width;
			
			textInput.Height = my_Window.Height - 27;
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			my_Window.Close();
		}

		private void btn_info_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Hello! This is my Simple Editor. I make it for my github repository.", "SimpleEditor U1 Info", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void btn_file_Click(object sender, RoutedEventArgs e)
		{
		}

		private void btn_file_MouseEnter(object sender, MouseEventArgs e)
		{
			sirB = true;
			panel_file_visible(sirB );
		}

		private void panel_file_MouseEnter(object sender, MouseEventArgs e)
		{
			sirP = true;
			panel_file_visible(sirP);
		}

		private void btn_file_MouseLeave(object sender, MouseEventArgs e)
		{
			Thread.Sleep(500);
			sirB = false;
			if (sirP)
				return;
			else
				panel_file_visible(false);
		}

		private void panel_file_MouseLeave(object sender, MouseEventArgs e)
		{
			sirP = false;
			if (sirB)
				return;
			else
				panel_file_visible(false);
		}

		private void btn_file_newfile_Click(object sender, RoutedEventArgs e)
		{
			if (isRenameTitle)
			{
				if (wantyousavefile())
					saveasfile();
			}

			textInput.Text = "";
			// File.CreateText(path + "/1.txt");
			panel_file_visible(false);

			fileNameBar.Content = "Untitled";
		}

		private void btn_file_openfile_Click(object sender, RoutedEventArgs e)
		{
			//string path = Directory.GetCurrentDirectory();

			if (isRenameTitle)
			{
				if (wantyousavefile())
					saveasfile();
			}


			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Text files (*.txt)|*.txt|Python files (*.py)|*.py|C files (*.c)|*.c|C# files (*.cs)|*.cs|C++ files(*.cpp)|*.cpp|All files (*.*)|*.*";
			if (openFileDialog.ShowDialog() == true)
				textInput.Text = File.ReadAllText(openFileDialog.FileName);

			fileNameBar.Content = openFileDialog.FileName;
			filename = openFileDialog.FileName;
			isSave = true;
			panel_file_visible(false);
			isRenameTitle = false;
		}

		private void btn_file_savefaile_Click(object sender, RoutedEventArgs e)
		{
			

			if (isSave) {
				File.WriteAllText(filename, textInput.Text);

				return;
			}
			if (filename == "") {
				filename = saveasfile();
			}
			/*
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

			if (saveFileDialog.ShowDialog() == false)
				return;
			// получаем выбранный файл
			filename = saveFileDialog.FileName;*/
			isSave = true;
			// сохраняем текст в файл
			File.WriteAllText(filename, textInput.Text);

			fileNameBar.Content = filename;
			panel_file_visible(false);
			isRenameTitle = false;
		}

		private void btn_file_saveasfile_Click(object sender, RoutedEventArgs e)
		{
			filename = saveasfile();
		}

		private void textInput_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			
		}

		private void textInput_PreviewKeyDown(object sender, KeyEventArgs e)
		{

			if (!isRenameTitle)
				fileNameBar.Content += "*";
			isSave = false;
			isRenameTitle = true;
			
		}
	}
}

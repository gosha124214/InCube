using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppInCube.Classes;
using Microsoft.Maui.Controls;
using static AppInCube.Classes.MetodMetodsGet;
using System.ComponentModel;
using AppInCube.View.Pages.Programs.UnderPages; // ���������, ��� ��� ������������ ���� ����������
using Microsoft.Maui.Controls;
using AppInCube.Classes.SQLBD;
namespace AppInCube.View.Pages.Programs
{
    public partial class ProgramsPage : ContentPage, INotifyPropertyChanged
    {
        private Conect _dbConnection;
        public ObservableCollection<TableBird> Programs { get; set; } // ��������� ��� �������� ��������

        private uint _currentOffset = 1; // ���������� ��� ������������ �������� ��������
        private const int _pageSize = 2; // ���������� ����������� �������� �� ���


        private uint? _numberOfAllObjects = 0; // ����� ���������� ��������

        private readonly ApiService _apiService = new ApiService();

        public ProgramsPage()
        {
            InitializeComponent();

            Programs = new ObservableCollection<TableBird>(); // ������������� ���������
            BindingContext = this; // ������������� �������� ��������

            // ��������� ����������� ����� ��� �������� ������
            LoadInitialPrograms();
        }

        public uint? NumberOfAllObjectsInDataBase
        {
            get { return _numberOfAllObjects; }
            set
            {
                
                _numberOfAllObjects = value;
                if (value != null)
                {
                    OnPropertyChanged(nameof(NumberOfAllObjectsInDataBase)); // ���������� �� ���������
                }
            }
        }

        public uint LoadedProgramsCount
        {
            get { return (uint)Programs.Count; }
        }

        private async void LoadInitialPrograms()
        {
            await LoadPrograms(_pageSize); // ��������� ������ 2 ���������
        }

        private async void OnLoadMoreClicked(object sender, EventArgs e)
        {
            await LoadPrograms(_pageSize); // ��������� ��� 2 ��������� ��� ������� ������
        }

        private async Task LoadPrograms(int count)
        {
            // ��������� �������������� � ������� ���������
            this.IsEnabled = false;
            
            NumberOfAllObjectsInDataBase = await _apiService.GetTableBirdCountAllProgram(); // �������� ����� ���������� ��������

            if (NumberOfAllObjectsInDataBase >= _currentOffset)
            {
                for (int i = 0; i < count; i++)
                {

                    TableBird? newProgram = await _apiService.GetTableBirdAsync(_currentOffset);

                    if ((_currentOffset <= _numberOfAllObjects) && (newProgram.Id != 0) && (newProgram != null)) // ���������, ��� ��������� ���� ���������
                    {
                        // ����������� ������� ������ � ImageSource
                        newProgram.ImageSource = ImageSource.FromStream(() => new MemoryStream(newProgram.ImageBirdFile));
                        // ���������� � ���������
                        Programs.Add(newProgram);
                        _currentOffset++;
                        OnPropertyChanged(nameof(LoadedProgramsCount));
                    }
                    else if ((newProgram == null) && (NumberOfAllObjectsInDataBase >= _currentOffset))
                    {
                        ShowMessage("�� ������� ��������� ���������.\n��������� ���������� � ����������"); // ��������� �������� ����� 5 ������
                        break;
                    }

                }
            }
            else if (NumberOfAllObjectsInDataBase == null)
            {
                ShowMessage("�� ������� ��������� ���������.\n��������� ���������� � ����������"); // ��������� �������� ����� 5 ������
            }
            else
            {
                ShowMessage("�������� ������ ���");
            }

            this.IsEnabled = true; // ������������ �������������� � ������� ���������
        }


        private async void ShowMessage(string message, int duration = 5000)
        {
            MessageLabel.Text = message; // ������������� ����� ���������
            MessageFrame.IsVisible = true; // ������ Frame �������

            // ���� ��������� �����
            await Task.Delay(duration);

            // �������� Frame
            MessageFrame.IsVisible = false;
        }


        private async void OnProgramTapped(object sender, EventArgs e)
        {

            // ��������� �������������� � ������� ���������
            this.IsEnabled = false;

            var tappedItem = (sender as StackLayout).BindingContext as TableBird;
            if (tappedItem != null)
            {
                await Navigation.PushAsync(new ProgramDetailPage(tappedItem));
            }


            this.IsEnabled = true; // ������������ �������������� � ������� ���������
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
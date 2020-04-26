using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Runtime.CompilerServices;
using DataGridBindingDataTable.Properties;

namespace DataGridBindingDataTable.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private DataTable _personDataTable;
        public DataTable PersonDataTable
        {
            get => _personDataTable;
            set
            {
                _personDataTable = value;
                OnPropertyChanged(nameof(PersonDataTable));
            }
        }
        public RelayCommand Save { get; }
        private readonly OleDbDataAdapter _dataAdapter;
        public MainWindowViewModel()
        {
            _dataAdapter = new OleDbDataAdapter
            {
                SelectCommand = new OleDbCommand
                {
                    Connection = new OleDbConnection(
                        "Provider=SQLOLEDB;Data Source=EPICPCGAEBSTER;Integrated Security=SSPI;Initial Catalog=playground"),
                    CommandText = "SELECT * FROM Person;"
                }
            };
            FillMyDataGrid();
            Save = new RelayCommand(() =>
            {
                try
                {
                    if (PersonDataTable.HasErrors)
                    {
                        PersonDataTable.RejectChanges();
                        return;
                    }
                    // Das ist unschön
                    var builder = new OleDbCommandBuilder(_dataAdapter);
                    _dataAdapter.Update(PersonDataTable);
                    PersonDataTable.AcceptChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }
        public void FillMyDataGrid()
        {
            var playgroundSet = new DataSet("Persons");
            _dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            _dataAdapter.TableMappings.Add("Table", "Person");
            _dataAdapter.Fill(playgroundSet);
            PersonDataTable = playgroundSet.Tables[0];
        }
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RandomFactApp.Domain.Models;
using RandomFactApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomFactApp.ViewModels
{
    public partial class StorageViewModel : ObservableObject
    {
        private readonly ITodoRepository toDoRepository;

        [ObservableProperty]
        private ObservableCollection<ToDo> toDos;

        [ObservableProperty]
        private string newToDoLabel;

        public StorageViewModel(ITodoRepository toDoRepository)
        {
            this.toDoRepository = toDoRepository;
            this.ToDos = new ObservableCollection<ToDo>();
        }

        [RelayCommand]
        public async Task FetchToDos()
        {
            this.ToDos.Clear();
            var todos = await this.toDoRepository.GetToDosAsync();

            foreach (var todo in todos)
                this.ToDos.Add(todo);
        }

        [RelayCommand]
        public async Task AddToDos()
        {
            if (!string.IsNullOrWhiteSpace(NewToDoLabel))
            {
                var newToDo = new ToDo { Label = NewToDoLabel };
                await this.toDoRepository.AddToDoAsync(newToDo);

                var toast = Toast.Make("To Do added");
                await toast.Show();

                this.ToDos.Add(newToDo);
            }
        }
    }
}
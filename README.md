# WPF_LibraryManagement_PetProject

## Пэт-проект реализованный в рамках задания по практике 1, предмета ПКС ( Програмирование корпаративных систем )

### Полный список команд для скачивания зависимостей и запуска проекта
#### 1 Создаём новый WPF-проект:
```
dotnet new wpf -n LibraryManagement
cd LibraryManagement
```
#### 2 Устанавливаем Entity Framework Core с SQLite:
```
dotnet add package Microsoft.EntityFrameworkCore --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.0
```
#### 3 Создаём базу данных (после написания LibraryContext.cs):
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```
#### 4 Запускаем проект:
```
dotnet run
```
#### 5 Если нужно удалить старую базу данных и пересоздать:
```
rm library.db
dotnet ef database update
```

#### Дополнительно:
#### Если что-то пошло не так, попробуйте очистить и пересобрать проект:
```
dotnet clean
dotnet restore
dotnet build
```

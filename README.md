# TaskAPI  

TaskAPI is a simple **ASP.NET Core Web API** project that demonstrates CRUD operations with **Entity Framework Core**.  
The API manages tasks, allowing you to create, read, update, and delete records from a **SQL Server** database.  

---

## 🚀 Features  

- Create, read, update, and delete tasks (CRUD)  
- SQL Server integration with **Entity Framework Core**  
- RESTful API endpoints with proper HTTP methods (GET, POST, PUT, DELETE)  
- Query optimization for performance  
- Clean code principles applied  
- Unit testing and integration testing included  

---

## 🛠 Tech Stack  

- **Language:** C#  
- **Framework:** .NET 6 / ASP.NET Core Web API  
- **Database:** SQL Server  
- **ORM:** Entity Framework Core  
- **Tools:** Visual Studio, Git  
- **API Documentation:** Swagger  

---

## 📂 Project Structure  

```
TaskAPI/  
│── Controllers/        # API Controllers (e.g., TaskController)  
│── Models/             # Data models (e.g., Task.cs)  
│── Data/               # Database context (e.g., AppDbContext.cs)  
│── Migrations/         # Entity Framework migrations  
│── Properties/         # Project properties and launch settings  
│── Program.cs          # Application entry point  
│── appsettings.json    # Configuration file  
│── TaskAPI.csproj      # Project file  
```

---

## ⚡ API Endpoints  

- `GET /api/tasks` → Get all tasks  
- `GET /api/tasks/{id}` → Get a specific task by ID  
- `POST /api/tasks` → Create a new task  
- `PUT /api/tasks/{id}` → Update an existing task  
- `DELETE /api/tasks/{id}` → Delete a task  

---

### 📄 JSON Response (for GET /api/tasks/{id})  

```json
{
  "id": 1,
  "title": "Finish README",
  "isCompleted": false
}
```

---

## 💻 Setup & Run  

1. Clone this repository  

```bash
git clone https://github.com/simona312/TaskAPI.git
cd TaskAPI
dotnet restore
dotnet ef database update
dotnet run
```

---

## 🚀 Future Improvements  

- Add authentication & authorization (JWT)  
- Implement pagination & filtering
- Add Swagger documentation improvements  
- Add more unit & integration tests 





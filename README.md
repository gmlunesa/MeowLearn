# 😺 MeowLearn

## About

MeowLearn is a concept project that uses the ASP.NET MVC framework to build a fullstack web application.

### Stack

| <img src="https://upload.wikimedia.org/wikipedia/commons/e/ee/.NET_Core_Logo.svg" width="100" height="100" alt="NET Core"> | <img src="https://raw.githubusercontent.com/yurijserrano/Github-Profile-Readme-Logos/master/cloud/docker.svg" width="100" height="100" alt="Docker"> | <img src="https://raw.githubusercontent.com/yurijserrano/Github-Profile-Readme-Logos/f994c418a134b58c4aec11152f6a4a33fa89da26/cloud/azure.svg" width="100" height="100" alt="Azure"> | <img src="https://symbols.getvecta.com/stencil_28/61_sql-database-generic.90b41636a8.svg" width="100" height="100" alt="SQL Server"> |
| -------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------ |

- ASP.NET MVC
- Entity Framework Core
- LINQ
- SQL Server
- Azure App Service

## Setup

Clone the repository.

```
git clone https://github.com/gmlunesa/MeowLearn.git
```

Check and modify [appsettings(.Development).json](https://github.com/gmlunesa/MeowLearn/blob/master/MeowLearn/appsettings.json) > ConnectionStrings > DefaultConnection properly to suit your database preferences.

```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=MeowLearnDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
```

Open solution on your preferred IDE. Build the project.

On .Net Core CLI

```
dotnet build
```

On Visual Studio

> Press Ctrl+Shift+B

Run the migrations using the following:

On .NET Core CLI

```
dotnet ef database update
```

On Visual Studio Package Manager Console

```
Update-Database
```

## License

[MIT 🌱 Fully open-source](https://github.com/gmlunesa/MeowLearn/blob/main/LICENSE)

## Credits

- Logo is from the open source project [SVGRepo](https://www.svgrepo.com/).
- Hero image is from [Lori Tsao](https://www.behance.net/loritsao).

---

Made with 💫✨ by [gmlunesa](https://gmlunesa.com) | Questions? Send me an email! I'd be happy to help.

 # ProductMananger Instruction
- Open your sql server and run the script provided to create all tables needed by the application.
- Change connection string in the project (1.UI(ProductMananger)) open appsettings.json and change DefaultConnection
- - Change connection string in the project (3.Service(ProductManangerAPI)) open appsettings.json and change ApplicationDbContext
- Open the application and run ProductManangerAPI inside 3.Service, get the url.
- Then Move to BLL, open app.config file, and change the Url Value to the open you copied above.
- Set <Multiple Startup Projects> and select ProductMananger,ProductManangerAPI and run the project.

 - System flow diagram is attached into the repository as well.

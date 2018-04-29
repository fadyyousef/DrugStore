# DrugStore
Drug Store
This application is built with MVC and entity framework 6 showing up to date procedures on object oriented projects.

The front end is using 
      1) bootstrap 3.0
      2) jQuery 3.3.1 and jQuery UI 1.12.1
      3) FontAwesome
      4) Razor Html and CSS 3.0
      
The back end is using
      1) Implementation of a "BaseController" to store global methods, functions, and variables.
      2) Implementation of a "UnitOfWork" to enable access to all repositories and keep only one context open to the database.
      3) Grouping is done in controllers (Index) using PagedList method.
      3) Custom Authorization and AuthenticAation to use roles like admin and user.
      4) Html Extensions to enable Modal pop ups (for example: forgot password pop up).
      5) Email services logging into sql database to enable tracking of application activity whenever applicable.
      
All model migrations are done using code first methods in the package console manager in visual studio.

# MovieSearch

### Add these endpoints to the end of your URL it will look something like this (your port # will be different: 
### https://localhost:44356/

##	HomeController (MVC)
### /	home page
### /Home/Privacy	GET	Privacy notice
##		MoviesController (MVC)
### /Movies	GET	list all movies
### /Movies/Edit/tt0034583	GET	edit movie
### /Movies/Details/tt0038461	GET	show movie details
##		MovieAPI (API)
### /api/MovieAPI/movie/{title}	GET	list movies with matching title from IMDB
##		Forecast (API)
### /api/forecast	GET	randomly generated 5 day forecast
##		TodoItems (API) (Postman will be needed for anything besides GET)
### /api/TodoItems	GET	List all to dos
### /api/TodoItems/{id}	GET	READ one to do
### /api/TodoItems/{id}	DEL	DELETE to do
### /api/TodoItems/{id}	PUT	UPDATE a to do
		PUT in Postman. Below is the body
		{ "id":2, "name":"order out to chapps", "isComplete":true }
### /api/TodoItems	POST	CREATE a to do item
		POST in Postman. Below is the body
		{ "name":"make dinner",  "isComplete":true }

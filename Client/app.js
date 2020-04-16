$(function () {

    var $movies = $('#movies');
    var $title = $('#title');
    var $genre = $('#genre');
    var $director = $('#director');

    var movieTemplate = "" +
    "<li>" +
    "<p><strong>Title : </strong> {{title}}</p>" +
    "<p><strong>Genre : </strong> {{genre}}</p>" +
    "<p><strong>Director : </strong>{{director}}</p>" +
    "<button data-id='{{movieId}}' class='remove'>Remove</button>" +
    "<button data-id='{{movieId}}' class='update'>Update</button>" +
    "</li>";

    function displayMovie(movie){
        $movies.append(Mustache.render(movieTemplate, movie));
    }

    $.ajax({
        type: 'GET',
        url: 'https://localhost:44325/api/movie/',
        contentType: 'application/json',
        success: function(movies) {
            $.each(movies, function(i, movie){
                displayMovie(movie);
            });
        },
        error: function() {
            alert('Error While Loading Movies');
        }
    });

    $('#add-movie').on('click', function(){

        var movie = {
            title: $title.val(),
            genre: $genre.val(),
            director: $director.val(),
        };

        $.ajax({
            type: 'POST',
            url: 'https://localhost:44325/api/movie/',
            contentType: 'application/json',
            data: JSON.stringify(movie),
            success: function(newMovie){
                displayMovie(newMovie);
            },
            error: function() {
                alert('Error Attempting to Save Movie to Database');
            }
        });
        
    })

    $movies.delegate('.remove','click', function(){

        var $li = $(this).closest('li');

        $.ajax({
            type: 'DELETE',
            url: 'https://localhost:44325/api/movie/' + $(this).attr('data-id'),
            success: function(){
                $li.remove();
            }
        });
    });

    $movies.delegate('.update', 'click', function(){

        var $li = $(this).closest('li');

        var updatedMovie = {            
            title: prompt('Change Title?'),
            genre: prompt('Change Genre?'),
            director: prompt('Change Director?'),            
        }

        $.ajax({
            type: 'PUT',
            url: 'https://localhost:44325/api/movie/' + $(this).attr('data-id'),
            contentType: 'application/json',
            data: JSON.stringify(updatedMovie),
            success: function(){
                alert('SUccess')
            }
        });

    });

});
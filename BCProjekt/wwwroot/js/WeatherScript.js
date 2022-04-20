var isMapShown; //boolean variable checking if map is shown or not
{
    var latitude;
    var longtitude;
    var weatherURL;
}

// function started when DOM is loaded
$(document).ready(function () {

    var isMapShown = false;
    var today = new Date(); //getting actual date and time
    var date = today.getDate();
    var time = ('0' + today.getHours()).substr(-2) + ":" + ('0' + today.getMinutes()).substr(-2);
    var dateTime = time + ' - ' + (today.toLocaleString('en-us', { weekday: 'long' })) + ', ' + date +
        ' ' + (today.toLocaleString('en-us', { month: 'long' }).substring(0, 3));
    
    $('#dateTime').html(dateTime);  //sending date and time into html structure

    //listener on submit button, get data via api and sends it into html structure
    $('#submitButton').click(function () {
        let inputCity = $('#cityInput').val();
        
        
        let geolocation = 'https://services.gisgraphy.com/geocoding/geocode?address=1 ' + inputCity + '&country=CZ';
        let test = ' https://nominatim.openstreetmap.org/search/' + inputCity + '?format=json&addressdetails=1&limit=1&polygon_svg=1';

        //fetching data from API
        fetch(test).then(response => response.json()).then(data => {
            
            latitude = data[0]['lat'];
            longtitude = data[0]['lon'];
            
            weatherURL = 'https://api.openweathermap.org/data/2.5/weather?lat=' + latitude + '&lon=' + longtitude + '&appid=f1a2685da05fe3b5355e7f7a70db4d4c&lang=en';

            fetch(weatherURL)
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                    let mapURL = 'https://maps.google.com/maps?q=' + inputCity + '&t=&z=13&ie=UTF8&iwloc=&output=embed';
                    $('#mapIFrame').attr("src", mapURL);
                    $("#city").html(inputCity);
                    $("#temperature").html(parseInt(data["main"]["temp"]) - 273 + "°C")
                    $("#description").html(data["weather"]["0"]["description"]);
                    checkWeatherCondition(data["weather"]["0"]["id"]);
                    $("#windSpeedOutput").html(data["wind"]["speed"]);
                    $("#windSpeed").html(data["wind"]["speed"] + " m/s");
                    $("#cloudiness").html(data["clouds"]["all"] + " %");
                    $("#feelTemp").html(parseInt(data["main"]["feels_like"]) - 273 + "°C");
                    $("#pressure").html(data["main"]["pressure"] + " hPa");
                    $("#humidity").html(data["main"]["humidity"] + "%");
                    var minTempInt = parseFloat(data["main"]["temp_min"] - 273);
                    var maxTempInt = parseFloat(data["main"]["temp_max"] - 273);
                    $("#tempMin").html(minTempInt.toFixed() + "°C");
                    $("#tempMax").html(maxTempInt.toFixed() + "°C");
                    $('#cityInput').val("");
                })
                //catching error on wrong input (city name)
                .catch(err => {
                    alert("Wrong city name")
                    $("#city").html("");
                    $("#temperature").html("");
                    $('.image-wrapper').hide();
                    $('#cityInput').val("");
                });
        });

        
        
    });

    //listener on button which shows map with entered city
    $('#showMapButton').click(function () {
        isMapShown = (isMapShown == true) ? isMapShown = false : isMapShown = true; //change boolean value on every click on button

        //changing text inside button and create animation on map
        if (!isMapShown) {
            $('#showMapButton').text('\u21e3 Mapa');
            $('.map-wrapper').removeClass('animation-slide-down-class');
            $('.map-wrapper').addClass('animation-slide-up-class');
            setTimeout(function () {
                $('.map-wrapper').hide();
            }, 1000);
        } else {
            $('#showMapButton').text('\u21E1 Mapa');
            $('.map-wrapper').removeClass('animation-slide-up-class');
            $('.map-wrapper').addClass('animation-slide-down-class');
            $('.map-wrapper').show();

            for (let i = 0; i < 1000; i++) {
                setTimeout(function () {
                    window.scrollTo(0, document.body.scrollHeight);
                }, i);
            }
        }
    });

    //listener on buttons with static city names
    $('.fav-cities-button').click(function () {
        let cityName = $(this).text();

        $('#cityInput').val(cityName);
        $('#submitButton').click();
        $('#cityInput').val("");
    });

});

//function making theme via condition id from API
function checkWeatherCondition(conditionParameter) {
    if (conditionParameter >= 200 && conditionParameter < 240) {
        $('.weather-icon').hide();
        $("#iconStorm").show();
        changeThemeRain();
    } else if (conditionParameter >= 300 && conditionParameter < 322) {
        $('.weather-icon').hide();
        $("#iconRain").show();
        changeThemeRain();
    } else if (conditionParameter >= 500 && conditionParameter < 532) {
        $('.weather-icon').hide();
        $("#iconRain").show();
        changeThemeRain();
    } else if (conditionParameter >= 600 && conditionParameter < 623) {
        $('.weather-icon').hide();
        $("#iconSnow").show();
    } else if (conditionParameter >= 701 && conditionParameter < 782) {
        $('.weather-icon').hide();
        $("#iconFog").show();
        changeThemeFog();
    } else if (conditionParameter == 800) {
        $('.weather-icon').hide();
        $("#iconSun").show();
        changeThemeSun();
    } else if (conditionParameter > 800 && conditionParameter < 805) {
        $('.weather-icon').hide();
        $("#iconCloud").show();
        changeThemeCloud();
    }
}

//functions changing theme images and font colours
function changeThemeRain() {
    $('.conent-wrapper').css("background-image", "url('../../WeatherBackground/rain_background.jpg')");
   

    document.documentElement.style
        .setProperty('--lightGrey', '#ababab');
}

function changeThemeFog() {
    $('.conent-wrapper').css("background-image", "url('../../WeatherBackground/fog_background.jpg')");


    document.documentElement.style
        .setProperty('--lightGrey', '#fff');
}

function changeThemeSun() {
    $('.conent-wrapper').css("background-image", "url('../../WeatherBackground/sun_background.jpg')");


    document.documentElement.style
        .setProperty('--lightGrey', '#fff');
}

function changeThemeCloud() {
    $('.conent-wrapper').css("background-image", "url('../../WeatherBackground/cloud_background.jpg')");

    document.documentElement.style
        .setProperty('--lightGrey', '#fff');
}
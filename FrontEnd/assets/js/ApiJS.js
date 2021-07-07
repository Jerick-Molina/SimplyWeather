var xhttp = new XMLHttpRequest();

//let url =  "https://68cd1f42-2b9a-4080-a386-bd1f5f8f70a9.mock.pstmn.io";
let url = "http://localhost:7071/api/WeatherApi";
var par_Json;
var weather_temp;
var isCelcius = false;
//xhttp.open("GET",url);
//xhttp.send();
xhttp.onreadystatechange = function (){
    if (xhttp.readyState == 4 && xhttp.status == 200){
    
        var button =  document.getElementById("city-button");
       par_Json = JSON.parse(this.responseText);
        

       if(par_Json.cod == "404"){
        console.log(par_Json.cod)
        cityNotFound();
       }else{
        button.disabled = false;
        button.innerHTML = "FIND";
        document.getElementById("err-city").hidden = true;
        weather_Temp = par_Json.Weather.Temp;
        
      
      
        currentWeatherIcon(par_Json.Weather.Icon)
        changeStats(par_Json);
       }
      
    }

}

function cityNotFound(){
   
    document.getElementById("err-city").hidden = false;
    document.getElementById("city-button").disabled = false
    document.getElementById("city-button").innerHTML = "FIND";
}

function changeStats(weather){


      document.getElementById("res-City").innerHTML = weather.City;
      if(weather.Weather.cur_Weather == "Clouds"){
        document.getElementById("res-Main").innerHTML = "Cloudy"
      }else{
        document.getElementById("res-Main").innerHTML = weather.Weather.cur_Weather
      }
    
      if(isCelcius == false){

        document.getElementById("res-Temp").innerHTML = weather.Weather.Temp +"F";
      }else

      {
        changeMetric();
      }
     
}

function changeMetric(){


    if(isCelcius == true){

    var value = parseFloat((weather_Temp - 32 )/ 1.8);
        
       
        var fixedValue = value.toFixed(2);
        console.log(value);
        document.getElementById("res-Temp").innerHTML = fixedValue +"C";
    }else{
        document.getElementById("res-Temp").innerHTML = weather_temp +"F";
    }
}


function currentWeatherIcon(icon){

   

    var _icon = document.getElementById("icon")
      
    _icon.style.backgroundImage = `url(/FrontEnd//images/weather_images/${icon}.svg)`
  
}




if(document.getElementById("city-button").disabled == false){
    document.getElementById("city-button").onclick = function(){
      
        var city = document.getElementById("client-input").value;
        var button = document.getElementById("city-button")
        
    
        if(city != ""){
           button.disabled = true;
            var createJson = {
                City : city
            }
        
            button.innerHTML = "Loading..."
           
           xhttp.open("POST",url);
           xhttp.send(JSON.stringify(createJson));
            
           document.getElementById("client-input").value = "";
           // console.log(city);
        }else{
            document.getElementById("err-city").hidden = false;
            console.log("NULL");
        }
      
    };
}



xhttp.response = function(){
    console.log("click?");
}



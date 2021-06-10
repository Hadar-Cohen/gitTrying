//READY////////
//READY////////

//https://api.themoviedb.org/3/tv/{tv_id}/season/{season_number}?api_key=<<api_key>>&language=en-US

function getTV() {
    i = 1;
    $("#seasonsList").html("");
    $("#episodeList").html("");
    let name = $("#tvShowName").val();
    let method = "3/search/tv?";
    let api_key = "api_key=" + key;
    let moreParams = "&language=en-US&page=1&include_adult=false&";
    let query = "query=" + encodeURIComponent(name);
    let apiCall = url + method + api_key + moreParams + query;
    ajaxCall("GET", apiCall, "", getTVSuccessCB, getTVErrorCB);
}

function getTVSuccessCB(tv) {
    buildTvSeriese(tv);
    console.log(tv);
    $("#Episodes").html("");
    seasonsList = "<span>";
    tvId = tv.results[0].id;
    posterURL = tv.results[0].poster_path
    let poster = imagePath + posterURL;
    str = "<img src='" + poster + "'/>";
    $("#ph").html(str);

    let method = "3/tv/";
    let api_key = "api_key=" + key;

    let apiCall = url + method + tvId + "/season/" + i + "?" + api_key;
    ajaxCall("GET", apiCall, "", getSeasonSuccessCB, getSeasonErrorCB);
}
//create obj for sql table - in button "add" we send it to the sql table

seriesObj = null;
function buildTvSeriese(tv) {
    seriesObj = {
        Id: tv.results[0].id,
        First_air_date: tv.results[0].first_air_date,
        Name: tv.results[0].name.replace(/'/g, ''),
        Origin_country: tv.results[0].origin_country[0],
        Original_language: tv.results[0].original_language,
        Overview: tv.results[0].overview.replace(/'/g, ''),
        Popularity: tv.results[0].popularity,
        Poster_path: imagePath + tv.results[0].poster_path.replace(/'/g, '')
    }
}

function getTVErrorCB(err) {
    console.log(err);
}

function getSeasonSuccessCB(season) {
    console.log(season);
    if (season.poster_path == null)
        season.poster_path = posterURL;
    seasonsList += "<div id= '" + i + "' class='card' onclick=showEpisode(this.id)>";
    seasonsList += "<img src='" + imagePath + season.poster_path + "' style='width:100%'>";
    seasonsList += "<h4><b>" + season.name + "</b></h4></div>";
    if (i % 5 == 0)
        seasonsList += "</span>";

    i++;
    if (i % 5 == 1) {
        seasonsList += "<span>";
    }
    $("#seasonsList").html(seasonsList);
    let method = "3/tv/";
    let api_key = "api_key=" + key;

    let apiCall = url + method + tvId + "/season/" + i + "?" + api_key;
    ajaxCall("GET", apiCall, "", getSeasonSuccessCB, getSeasonErrorCB);
}


function getSeasonErrorCB(err) {
    if (err.status == 404) {
        seasonsList += "</span>";
        $("#seasonsList").html(seasonsList);
        console.log(err);
    }
}

function showEpisode(seasonNum) {
    j = 1;
    saveSeasonNum = seasonNum;
    episodesList = "<tr>";
    $("#Episodes").html(episodesList);
    let method = "3/tv/";
    let api_key = "api_key=" + key;

    apiCall = url + method + tvId + "/season/" + seasonNum + "/episode/" + j + "?" + api_key;
    ajaxCall("GET", apiCall, "", getEpisodeSuccessCB, getEpisodeErrorCB);

}
c = 0;
epArr = [];
function getEpisodeSuccessCB(episodes) {
    let episode = {
    /* SeriesName: $('#tvShowName').val(),*/
        EpisodeId: episodes.id,
        SeriesId: seriesObj.id,//foreign key
        SeasonNum: episodes.season_number,
        EpisodeName: episodes.name,
        ImageURL: imagePath + episodes.still_path,
        Overview: episodes.overview,
        AirDate: episodes.air_date
    }

    epArr.push(episode);    //מערך של כל הפרקים
    episodesList += "<td class='card2'><img class= 'imgCard' id='" + j + "' src='" + imagePath + episodes.still_path + "'>";
    episodesList += "<div id='episodeBlock'><br><b>" + episodes.name + "</b></br> " + episodes.air_date + "</br></br><div id='episodeOverView'>" + (episodes.overview) + "</div></div>";//(episodes.overview).slice(0, 270) + "..."
    episodesList += "</br><button class='addBtn' id='" + c + "' type='button' onclick=PostToServer(epArr[this.id])> Add </button> </center></td>";
    //just login user can save episode
    if (localStorage.user == undefined) {
        $(".addBtn").prop('visible', false);
    }


    //episodesList += "<td class='card2' style='width:800px height: 700px'><center><b><p id='episodeTitle'>" + series[i].SeriesName + " season " + series[i].SeasonNum + "</p></b></center><img class= 'imgCard' src='" + series[i].ImageURL + "'>";
    //episodesList += "<div id='episodeBlock'><br><b>" + series[i].EpisodeName + "</b></br > " + series[i].AirDate + "</br></br><div id='episodeOverView'>" + series[i].Overview + "</div></div></td>";
    if ((c + 1) % 4 == 0)
        episodesList += "</tr>";

    c++;
    if ((c + 1) % 4 == 1) {
        episodesList += "<tr>";
    }

    $("#Episodes").html(episodesList);
    j++;
    let method = "3/tv/";
    let api_key = "api_key=" + key;
    let apiCall = url + method + tvId + "/season/" + saveSeasonNum + "/episode/" + j + "?" + api_key;
    ajaxCall("GET", apiCall, "", getEpisodeSuccessCB, getEpisodeErrorCB);
}

function getEpisodeErrorCB(err) {
    //if (err.status == 404)
    //    $("#Episodes").html(episodesList);
    c = 0;
    console.log(err);

}

function PostToServer(episodeToAdd) {
    let api = "../api/Seriess";
    console.log(seriesObj);
    ajaxCall("POST", api, JSON.stringify(seriesObj), postSeriesSqlSuccessCB, postSeriesSqlErrorCB)

   // console.log(episodeToAdd.SeasonNum);
    //let api = "../api/Episodes";
    //ajaxCall("POST", api, JSON.stringify(episodeToAdd), postEpisodeSuccessCB, postEpisodeErrorCB)
}

function postEpisodeSuccessCB(getEpisode) {
    alert("inserted " + getEpisode);
    alert(" The data inserted to table SeriesSql ");
    //add to sql table
    //let api = "../api/Seriess";
    //console.log(seriesObj);
    //ajaxCall("POST", api, JSON.stringify(seriesObj), postSeriesSqlSuccessCB, postSeriesSqlErrorCB)

}
function postSeriesSqlSuccessCB()
{
    let api = "../api/Episodes";
    ajaxCall("POST", api, JSON.stringify(episodeToAdd), postEpisodeSuccessCB, postEpisodeErrorCB)

    alert(" The data inserted to table SeriesSql ");
}
function postSeriesSqlErrorCB()
{
    alert("error!");
}

function postEpisodeErrorCB(err) {
    alert("ERROR");

}
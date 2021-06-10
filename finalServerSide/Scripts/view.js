


function getSeriesSuccessCB(episodes) {
    for (const e of episodes) {
        str += "<option value=" + e + ">" + e + "</option>";
    }
    str += "</select>";
    $("#phView").html(str);

}
function getSeriesErrorCB(err) {
    console.log(err);
}

function showEpisode(series) {
    var selectedText = series.options[series.selectedIndex].innerHTML;
    episodesList = "<tr>";
    console.log(selectedText);
    let api = "../api/Episodes?SeriesName=" + selectedText;
    ajaxCall("GET", api, "", getEpisodesSuccessCB, getEpisodesErrorCB);
}
function getEpisodesSuccessCB(series) {
    console.log(series);
    var i = 0;
    while (i < series.length) {
        episodesList += "<td class='card2' style='width:800px height: 700px'><center><b><p id='episodeTitle'>" + series[i].SeriesName + " season " + series[i].SeasonNum + "</p></b></center><img class= 'imgCard' src='" + series[i].ImageURL + "'>";
        episodesList += "<div id='episodeBlock'><br><b>" + series[i].EpisodeName + "</b></br > " + series[i].AirDate + "</br></br><div id='episodeOverView'>" + series[i].Overview + "</div></div></td>";

        if ((i + 1) % 4 == 0)
            episodesList += "</tr>";
        i++;
        if ((i + 1) % 4 == 1)
            episodesList += "<tr>";

    }
    episodesList += "</tr>";
    $("#episodesView").html(episodesList);

}
function getEpisodesErrorCB(err) {
    console.log(err);
}
function exit() {
    localStorage.clear();
    document.location = 'insert_signup.html'
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ex2.Models.DAL;

namespace Ex2.Models
{
    public class Episode
    {
        int episodeId;
        int seriesId;
        string seriesName;
        int seasonNum;
        string episodeName;
        string imageURL;
        string overview;
        string airDate;

        public int SeriesId { get => seriesId; set => seriesId = value; }
        public string SeriesName { get => seriesName; set => seriesName = value; }
        public int SeasonNum { get => seasonNum; set => seasonNum = value; }
        public string EpisodeName { get => episodeName; set => episodeName = value; }
        public string ImageURL { get => imageURL; set => imageURL = value; }
        public string Overview { get => overview; set => overview = value; }
        public string AirDate { get => airDate; set => airDate = value; }
        public int EpisodeId { get => episodeId; set => episodeId = value; }
        

        public Episode()
        {

        }

        public Episode(int episodeId, string seriesName, int seriesId, int seasonNum, string episodeName, string imageURL, string overview, string airDate)
        {
            this.episodeId = episodeId;
            this.seriesName = seriesName;
            this.seriesId = seriesId;
            this.seasonNum = seasonNum;
            this.episodeName = episodeName;
            this.imageURL = imageURL;
            this.overview = overview;
            this.airDate = airDate;
        }

        public int Insert()
        {
            EpisodeDataServices ds = new EpisodeDataServices();
            return ds.Insert(this);
        }

        public List<Episode> Get(string seriesName)
        {
            EpisodeDataServices d = new EpisodeDataServices();
            List<Episode> episodeList = d.GetEpisodes(seriesName);
            return episodeList;
        }
    }
}
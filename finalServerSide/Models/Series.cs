using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ex2.Models.DAL;

namespace Ex2.Models
{
    public class Series
    {
        int id;
        string first_air_date;
        string name;
        string origin_country;
        string original_language;
        string overview;
        float popularity;
        string poster_path;

        public Series(int id, string first_air_date, string name, string origin_country, string original_language, string overview, float popularity, string poster_path)
        {
            this.id = id;
            this.first_air_date = first_air_date;
            this.name = name;
            this.origin_country = origin_country;
            this.original_language = original_language;
            this.overview = overview;
            this.popularity = popularity;
            this.poster_path = poster_path;
        }
        public Series()
        { 

        }

        public int Id { get => id; set => id = value; }
        public string First_air_date { get => first_air_date; set => first_air_date = value; }
        public string Name { get => name; set => name = value; }
        public string Origin_country { get => origin_country; set => origin_country = value; }
        public string Original_language { get => original_language; set => original_language = value; }
        public string Overview { get => overview; set => overview = value; }
        public float Popularity { get => popularity; set => popularity = value; }
        public string Poster_path { get => poster_path; set => poster_path = value; }


        public void Insert()
        {
            SeriesDBServices dbs = new SeriesDBServices();
            dbs.Insert(this);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ex2.Models.DAL;
using Ex2.Models;

namespace Ex2.Models
{
    public class Total
    {
        Series series;
        Episode episode;
        int userId;

        public Total(Series series, Episode episode, int userId)
        {
            this.Series = series;
            this.Episode = episode;
            this.UserId = userId;
        }

        public Total()
        {
        }

        public Series Series { get => series; set => series = value; }
        public Episode Episode { get => episode; set => episode = value; }
        public int UserId { get => userId; set => userId = value; }

        public int Insert()
        {
            TotalDBServices ds = new TotalDBServices();
            Series.Insert();
            Episode.Insert();
            return ds.Insert(this);
        }

        public List<string> GetSeries(int userId)
        {
            TotalDBServices tDB = new TotalDBServices();
            return tDB.GetSeries(userId);
        }
    }
}
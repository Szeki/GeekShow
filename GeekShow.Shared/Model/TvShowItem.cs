﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Shared.Model
{
    public class TvShowItem : INotifyPropertyChanged
    {
        #region Members

        string _link;
        int _started;
        int? _ended;
        DateTime? _endDate;
        string _status;
        string _classification;
        int _seasons;

        #endregion

        #region Constructor

        public TvShowItem() { }

        public TvShowItem(int showId, string name)
        {
            ShowId = showId;
            Name = name;
        }

        #endregion

        #region Properties

        public int ShowId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Country
        {
            get;
            set;
        }

        public string Link
        {
            get
            {
                return _link;
            }
            set
            {
                if (_link == value) return;

                _link = value;

                RaisePropertyChanged("Link");
            }
        }

        public int Started
        {
            get
            {
                return _started;
            }
            set
            {
                if (_started == value) return;

                _started = value;

                RaisePropertyChanged("Started");
            }
        }

        public int? Ended
        {
            get
            {
                return _ended;
            }
            set
            {
                if (_ended == value) return;

                _ended = value;

                RaisePropertyChanged("Ended");
            }
        }

        public DateTime? EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if (_endDate == value) return;

                _endDate = value;

                RaisePropertyChanged("EndDate");
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status == value) return;

                _status = value;

                RaisePropertyChanged("Status");
            }
        }

        public string Classification
        {
            get
            {
                return _classification;
            }
            set
            {
                if (_classification == value) return;

                _classification = value;

                RaisePropertyChanged("Classification");
            }
        }

        public int Seasons
        {
            get
            {
                return _seasons;
            }
            set
            {
                if (_seasons == value) return;

                _seasons = value;

                RaisePropertyChanged("Seasons");
            }
        }

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Protected Methods

        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
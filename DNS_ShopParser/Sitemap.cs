using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNS_ShopParser
{
    class Sitemap
    {
        private Guid id;
        private string urlLoc;
        private DateTime lastMod;
        private string changeFreq;
        private float priority;

        public Sitemap()
        {
            
        }

        public Sitemap(Guid aId, string aUrlLoc, DateTime aLastMod, string aChangeFreq, float aPriority)
        {
            id = aId;
            urlLoc = aUrlLoc;
            lastMod = aLastMod;
            changeFreq = aChangeFreq;
            priority = aPriority;
        }

        #region Properties
        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Url
        {
            get { return urlLoc; }
            set { urlLoc = value; }
        }

        public DateTime LastModification
        {
            get { return lastMod; }
            set { lastMod = value; }
        }

        public string ChangeFreq
        {
            get { return changeFreq; }
            set { changeFreq = value; }
        }

        public float Priority
        {
            get { return priority; }
            set { priority = value; }
        }
        #endregion

    }
}

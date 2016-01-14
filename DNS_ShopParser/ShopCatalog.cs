using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNS_ShopParser
{
    class ShopCatalog
    {
        private Guid id;
        private Product product;
        private string linkToStore;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ShopCatalog()
        {
            
        }

        /// <summary>
        /// Constructor with full object initialization
        /// </summary>
        public ShopCatalog(Guid aId, Product aProduct, string aLinkToStore)
        {
            id = aId;
            product = aProduct;
            linkToStore = aLinkToStore;
        }

        public Guid ID
        {
            get { return id;}
            set { id = value;  }
        }

        public Product Product
        {
            get { return product; }
            set { product = value; }
        }

        public string LinkToStore
        {
            get { return linkToStore; }
            set { linkToStore = value; }
        }

    }
}

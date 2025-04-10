using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Nop.Plugin.Widgets.SpecialOffers.Models;
public class SpecialOffersModel
{
    public class SpecialOfferModel
    {
        public string Title { get; set; }
        public string Discount { get; set; }
        public int ProductId { get; set; }
        public string ProductSeName { get; set; } // SEO-friendly name for URL
    }

    public class SpecialOffersListModel
    {
        public IList<SpecialOfferModel> Offers { get; set; } = new List<SpecialOfferModel>();
    }
}

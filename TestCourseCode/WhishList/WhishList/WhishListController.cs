using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhishList.WhishList
{
    internal class WhishListController
    {
        private IWhishDAO _whishDAO;

        public WhishListController()
        {

        }

        public WhishListController(IWhishDAO whishDAO)
        {
            _whishDAO = whishDAO;
        }

        public string newWish(string namn, string leksak)
        {
            Whish whish = new Whish(namn, leksak);
            bool saved = _whishDAO.save(whish);
            if (saved)
            {
                return namn + "s önskan sparades!";
            }

            return "";
        }
    }
}

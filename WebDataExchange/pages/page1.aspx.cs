using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDataExchange.pages
{
	public partial class page1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Page.Application["ss"].ToString();
			Page.Cache.Add(key: "ss", value: "aaa", dependencies: null, absoluteExpiration: 
			System.Web.Caching.Cache.NoAbsoluteExpiration, slidingExpiration: 
			TimeSpan.FromMinutes(30), priority: System.Web.Caching.CacheItemPriority.Normal, 
			onRemoveCallback: null);
		}
	}
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace RacePhotosTestSupport
{
	public class FakeHttpContext : HttpContextBase
	{
		private HttpSessionStateBase _session = new FakeSession();
		public override HttpServerUtilityBase Server
		{
			get { return new FakeHttpServerUtility(); }
		}

		public override HttpSessionStateBase Session
		{
			get { return _session; }
		}
	}

	public class FakeHttpServerUtility : HttpServerUtilityBase
	{
		public override string MapPath(string path)
		{
			string returnPath;
			if (path.StartsWith("~/"))
			{
				returnPath = Path.Combine(@"..\..\..\PhotoServer", path.Substring((2)));
				return returnPath;
			}
			else
			{
				throw new ArgumentException(string.Format("path {0} does not start with \"~/\"", path));
			}
		}
	}

	public class FakeSession : HttpSessionStateBase
	{
		readonly Dictionary<string, object> _items = new Dictionary<string, object>();

		public override object this[string name]
		{
			get
			{
				if (_items.ContainsKey(name))
					return _items[name];
				return null;
			}
			set { _items[name] = value; }
		}
	
	}
}

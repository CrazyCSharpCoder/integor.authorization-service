using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using PrettyUserAuthorizationAspShared;

namespace PrettyUserAuthorizationAspServices
{
	public class HttpContextProcessedMarker : IHttpContextProcessedMarker
	{
		private const string _processedItemName = "IsComplete";

		private HttpContext _http;

		public HttpContextProcessedMarker(IHttpContextAccessor httpAccessor)
		{
			_http = httpAccessor.HttpContext;
		}

		public bool? GetProcessed()
		{
			if (!_http.Items.ContainsKey(_processedItemName))
				return null;

			return _http.Items.ContainsKey(_processedItemName);
		}

		public void SetProcessed(bool processed)
		{
			_http.Items[_processedItemName] = processed;
		}

		public bool IsProcessed()
		{
			if (!_http.Items.ContainsKey(_processedItemName))
				return false;

			return _http.Items.ContainsKey(_processedItemName);
		}
	}
}

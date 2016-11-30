using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication1
{
	public static class Common
	{
		public static bool HasData(this DataSet dataSetObject)
		{
			return (dataSetObject != null && dataSetObject.Tables.Count > 0 && dataSetObject.Tables[0].Rows.Count > 0);
		}

		public static int ToInt(this object value, int defaultValue)
		{
			int intValue;

			return (value is bool)
					? ((bool)value) ? 1 : 0
					: int.TryParse(value + "", out intValue) ? intValue : defaultValue;
		}
	}
}
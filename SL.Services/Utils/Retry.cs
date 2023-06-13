using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Services.Utils;

public static class Retry
{
	//Static class from GitHub; thanks to Lbushkin and VCD 
	//(https://stackoverflow.com/users/91671/lbushkin ; https://stackoverflow.com/users/1131246/vcd. 
	//Answer from: https://stackoverflow.com/questions/1563191/cleanest-way-to-write-retry-logic)


	public static void Do(
	Action action,
	TimeSpan retryInterval,
	int maxAttemptCount = 3)
	{
		Do<object>(() =>
		{
			action();
			return null;
		}, retryInterval, maxAttemptCount);
	}

	public static T Do<T>(
		Func<T> action,
		TimeSpan retryInterval,
		int maxAttemptCount = 3)
	{
		var exceptions = new List<Exception>();

		for (int attempted = 0; attempted < maxAttemptCount; attempted++)
		{
			try
			{
				if (attempted > 0)
				{
					Thread.Sleep(retryInterval);
				}
				return action();
			}
			catch (Exception ex)
			{
				exceptions.Add(ex);
			}
		}
		throw new AggregateException(exceptions);
	}
}

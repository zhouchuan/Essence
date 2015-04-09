using UnityEngine;
using System.Collections;

namespace Common
{
	public class TimerScript
	{
		private bool	isTimerStart = false;
		private float	startTime = 0;
		private int		endTime = 0;
		private int		second = 0;
		private int		minute = 0;

		public int CountDownTimer(int totalSeconds)
		{
			if(!isTimerStart)
			{
				isTimerStart = true;
				startTime = Time.time;
			}
			endTime = (int) (Time.time - startTime);
			second = endTime % 60;
			minute = endTime / 60;
			if(totalSeconds < minute * 60 + second)
			{
				ResetTimer();
				return -1 ;
			}

			return totalSeconds - (minute * 60 + second);
		}

		public void Timer(float totalTime,GameObject target, string funcName, object value )
		{
			if(!isTimerStart)
			{
				isTimerStart = true;
				startTime = Time.time;
			}
			endTime = (int) (Time.time - startTime);
			second = endTime % 60;
			minute = endTime / 60;

			if(totalTime <= minute * 60 + second)
			{
				isTimerStart = false;
				target.SendMessage(funcName,value);
				ResetTimer();
			}
		}

		public void ResetTimer()
		{
			this.startTime = 0;
			this.endTime = 0;
			this.second = 0;
			this.minute = 0;
			this.isTimerStart = false;
		}
	}
}

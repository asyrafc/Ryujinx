using Ryujinx.Core.OsHle.Ipc;
using System;
using System.Collections.Generic;

namespace Ryujinx.Core.OsHle.Services.Time
{
    class ITimeZoneService : IpcService
    {
        private Dictionary<int, ServiceProcessRequest> m_Commands;

        public override IReadOnlyDictionary<int, ServiceProcessRequest> Commands => m_Commands;

        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public ITimeZoneService()
        {
            m_Commands = new Dictionary<int, ServiceProcessRequest>()
            {
                { 101,  ToCalendarTimeWithMyRule }
            };
        }

        public long ToCalendarTimeWithMyRule(ServiceCtx Context)
        {
            long PosixTime = Context.RequestData.ReadInt64();

            DateTime CurrentTime = Epoch.AddSeconds(PosixTime).ToLocalTime();

            Context.ResponseData.Write((ushort)CurrentTime.Year);
            Context.ResponseData.Write((byte)CurrentTime.Month);
            Context.ResponseData.Write((byte)CurrentTime.Day);
            Context.ResponseData.Write((byte)CurrentTime.Hour);
            Context.ResponseData.Write((byte)CurrentTime.Minute);
            Context.ResponseData.Write((byte)CurrentTime.Second);
            Context.ResponseData.Write((byte)0);

            /* Thanks to TuxSH
                struct CalendarAdditionalInfo {
	                u32 tm_wday; //day of week [0,6] (Sunday = 0)
	                s32 tm_yday; //day of year [0,365]
	                struct timezone {
		                char[8] tz_name;
		                bool isDaylightSavingTime;
		                s32 utcOffsetSeconds;
	                };
                };
            */
            Context.ResponseData.Write((int)CurrentTime.DayOfWeek);

            Context.ResponseData.Write(CurrentTime.DayOfYear);

            //TODO: Find out the names used.
            Context.ResponseData.Write(new byte[8]);

            Context.ResponseData.Write((byte)(CurrentTime.IsDaylightSavingTime() ? 1 : 0));

            Context.ResponseData.Write((int)TimeZoneInfo.Local.GetUtcOffset(CurrentTime).TotalSeconds);

            return 0;
        }
    }
}
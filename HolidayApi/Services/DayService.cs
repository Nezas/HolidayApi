﻿namespace HolidayApi.Services
{
    public class DayService : IDayService
    {
        private readonly DataContext _db;

        public DayService(DataContext db)
        {
            _db = db;
        }

        public async Task<DayStatusDto> GetDayStatus(string country, int year, int month, int day)
        {
            var dayStatusDto = GetDayStatusFromDb(country, year, month, day);
            if (dayStatusDto != null)
            {
                return dayStatusDto;
            }

            var isPublicHoliday = await IsPublicHoliday(country, year, month, day);
            var isWorkDay = await IsWorkDay(country, year, month, day);

            if (isPublicHoliday == null || isWorkDay == null)
            {
                return null;
            }

            if (isPublicHoliday["isPublicHoliday"].ToString() == "True")
            {
                AddDayStatusToDb(country, year, month, day, "holiday");
                return new DayStatusDto("holiday");
            }
            else if (isWorkDay["isWorkDay"].ToString() == "True")
            {
                AddDayStatusToDb(country, year, month, day, "workday");
                return new DayStatusDto("workday");
            }
            AddDayStatusToDb(country, year, month, day, "free day");
            return new DayStatusDto("free day");
        }

        public async Task<JToken> IsPublicHoliday(string country, int year, int month, int day)
        {
            return await RestService.Get<JToken>($"isPublicHoliday&date={day}-{month}-{year}&country={country}");
        }

        public async Task<JToken> IsWorkDay(string country, int year, int month, int day)
        {
            return await RestService.Get<JToken>($"isWorkDay&date={day}-{month}-{year}&country={country}");
        }

        public async Task<IEnumerable<HolidayDto>> GetHolidaysForYear(string country, int year)
        {
            return await RestService.Get<IEnumerable<HolidayDto>>($"getHolidaysForYear&year={year}&country={country}&holidayType=public_holiday");
        }

        public async Task<MaximumFreeDaysDto> GetMaximumFreeDays(string country, int year)
        {
            var maximumFreeDaysDto = GetMaximumFreeDaysFromDb(country, year);
            if (maximumFreeDaysDto != null)
            {
                return maximumFreeDaysDto;
            }

            IEnumerable<HolidayDto> holidays = await GetHolidaysForYear(country, year);

            if (holidays == null)
            {
                return null;
            }

            // Calculate february days in the month
            int febDays = 28;
            if (year % 4 == 0)
            {
                febDays = 29;
            }

            // Holds array of month converted to days
            int[] monthToDays = new int[15];
            monthToDays[0] = 0;
            monthToDays[1] = 31;
            monthToDays[2] = 31 + febDays;
            int daySum = 31 + febDays;

            for (int i = 3; i <= 12; i++)
            {
                switch (i)
                {
                    case 3:
                        {
                            daySum += 31;
                            break;
                        }
                    case 4:
                        {
                            daySum += 30;
                            break;
                        }
                    case 5:
                        {
                            daySum += 31;
                            break;
                        }
                    case 6:
                        {
                            daySum += 30;
                            break;
                        }
                    case 7:
                        {
                            daySum += 31;
                            break;
                        }
                    case 8:
                        {
                            daySum += 31;
                            break;
                        }
                    case 9:
                        {
                            daySum += 30;
                            break;
                        }
                    case 10:
                        {
                            daySum += 31;
                            break;
                        }
                    case 11:
                        {
                            daySum += 30;
                            break;
                        }
                    case 12:
                        {
                            daySum += 31;
                            break;
                        }
                    default:
                        break;
                }
                monthToDays[i] = daySum;
            }

            // For each holiday in the year, insert its day to freeDays list.
            List<int> freeDays = new();
            foreach (var holiday in holidays)
            {
                // Calculate holiday's day in the year value.
                int dayInYear = holiday.Date.Day + monthToDays[holiday.Date.Month - 1];
                // If the holiday is at Friday or Monday, add Saturday and Sunday in to the free days list as well.
                if (holiday.Date.DayOfWeek == 5)
                {
                    freeDays.Add(dayInYear);
                    freeDays.Add(dayInYear + 1);
                    freeDays.Add(dayInYear + 2);
                }
                else if (holiday.Date.DayOfWeek == 1)
                {
                    freeDays.Add(dayInYear);
                    freeDays.Add(dayInYear - 1);
                    freeDays.Add(dayInYear - 2);
                }
                else
                {
                    freeDays.Add(dayInYear);
                }
            }

            // Remove same days in the free days list and sort it.
            List<int> uniqueDays = freeDays.Distinct().ToList();
            uniqueDays.Sort();

            // Iterate through the list of uniqueDays and search for the maximum free days in the row.
            int maximumFreeDays = 2;
            int freeDaysInRow;
            for (int i = 0; i < uniqueDays.Count - 1; i++)
            {
                freeDaysInRow = 1;
                for (int j = i + 1; j < uniqueDays.Count; j++)
                {
                    if (uniqueDays[i] + freeDaysInRow == uniqueDays[j])
                    {
                        freeDaysInRow++;
                    }
                    else
                    {
                        break;
                    }

                    if (freeDaysInRow > maximumFreeDays)
                    {
                        maximumFreeDays = freeDaysInRow;
                    }
                }
            }

            AddMaximumFreeDaysToDb(country, year, maximumFreeDays);
            return new MaximumFreeDaysDto(maximumFreeDays);
        }

        public void AddDayStatusToDb(string country, int year, int month, int day, string dayStatusResult)
        {
            var dayStatus = new DayStatus();
            dayStatus.Country = country;
            dayStatus.Year = year;
            dayStatus.Month = month;
            dayStatus.Day = day;
            dayStatus.DayStatusResult = dayStatusResult;

            _db.DayStatuses.Add(dayStatus);
            _db.SaveChanges();
        }

        public DayStatusDto GetDayStatusFromDb(string country, int year, int month, int day)
        {
            var dayStatus = _db.DayStatuses.FirstOrDefault(ds => ds.Country == country && ds.Year == year && ds.Month == month && ds.Day == day);
            if (dayStatus == null)
            {
                return null;
            }

            return new DayStatusDto(dayStatus.DayStatusResult);
        }

        public void AddMaximumFreeDaysToDb(string country, int year, int dayStatusResult)
        {
            var maximumFreeDays = new MaximumFreeDays();
            maximumFreeDays.Country = country;
            maximumFreeDays.Year = year;
            maximumFreeDays.MaximumFreeDaysResult = dayStatusResult;

            _db.MaximumFreeDays.Add(maximumFreeDays);
            _db.SaveChanges();
        }

        public MaximumFreeDaysDto GetMaximumFreeDaysFromDb(string country, int year)
        {
            var maximumFreeDays = _db.MaximumFreeDays.FirstOrDefault(mfd => mfd.Country == country && mfd.Year == year);
            if (maximumFreeDays == null)
            {
                return null;
            }

            return new MaximumFreeDaysDto(maximumFreeDays.MaximumFreeDaysResult);
        }
    }
}

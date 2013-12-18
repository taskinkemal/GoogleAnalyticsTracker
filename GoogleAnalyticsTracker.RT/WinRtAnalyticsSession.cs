﻿using Windows.Foundation.Collections;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.RT
{
    public class WinRtAnalyticsSession
        : AnalyticsSession
    {
        private const string StorageKeyUniqueId = "GoogleAnalytics.UniqueID";
        private const string StorageKeyFirstVisitTime = "GoogleAnalytics.FirstVisitTime";
        private const string StorageKeyPreviousVisitTime = "GoogleAnalytics.PrevVisitTime";
        private const string StorageKeySessionCount = "GoogleAnalytics.SessionCount";

        private readonly IPropertySet _settings = Windows.Storage.ApplicationData.Current.LocalSettings.Values;

        protected override string GetUniqueVisitorId()
        {
            if (!_settings.ContainsKey(StorageKeyUniqueId))
            {
                _settings.Add(StorageKeyUniqueId, base.GetUniqueVisitorId());
            }
            return (string) _settings[StorageKeyUniqueId];
        }

        protected override int GetFirstVisitTime()
        {
            if (!_settings.ContainsKey(StorageKeyFirstVisitTime))
            {
                _settings.Add(StorageKeyFirstVisitTime, base.GetFirstVisitTime());
            }
            return (int) _settings[StorageKeyFirstVisitTime];
        }

        protected override int GetPreviousVisitTime()
        {
            if (!_settings.ContainsKey(StorageKeyPreviousVisitTime))
            {
                _settings.Add(StorageKeyPreviousVisitTime, base.GetPreviousVisitTime());
            }

            var previousVisitTime = (int) _settings[StorageKeyPreviousVisitTime];
            _settings[StorageKeyPreviousVisitTime] = GetCurrentVisitTime();
            return previousVisitTime;
        }

        protected override int GetSessionCount()
        {
            if (!_settings.ContainsKey(StorageKeySessionCount))
            {
                _settings.Add(StorageKeySessionCount, base.GetPreviousVisitTime());
            }
            var sessionCount = (int) _settings[StorageKeySessionCount];
            _settings[StorageKeySessionCount] = sessionCount++;
            return sessionCount;
        }
    }
}

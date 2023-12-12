using System.Collections.Generic;
using AppsFlyerSDK;

public partial class IronSourceManager
{
    private void RegisterRevenuePaidCallback()
    {
        IronSourceEvents.onImpressionDataReadyEvent += OnImpressionDataReady;
    }

    private void OnImpressionDataReady(IronSourceImpressionData impressionData)
    {
        if (impressionData != null && !string.IsNullOrEmpty(impressionData.adNetwork) && impressionData.revenue != null)
        {
            AnalyticsRevenueAds.SendEvent(impressionData);

            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "country", impressionData.country },
                { "ad_unit", impressionData.adUnit },
                { "ad_type", impressionData.instanceName },
                { "placement", impressionData.placement }
            };

            AppsFlyerAdRevenue.logAdRevenue(impressionData.adNetwork,
                AppsFlyerAdRevenueMediationNetworkType.AppsFlyerAdRevenueMediationNetworkTypeIronSource,
                impressionData.revenue.Value, "USD", parameters);
        }
    }
}
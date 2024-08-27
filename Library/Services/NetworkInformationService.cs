using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace Library.Services
{
    /// <summary>
    /// szolgáltatás, amely információt nyújt arról, hogy csatlakozvav vagyunk-e az internetre
    /// </summary>
    public class NetworkService
    {
        /// <summary>
        /// megmondja, hogy a felhasználó csatlakozva van-e az internetre
        /// </summary>
        /// <returns></returns>
        public bool IsInternetAvailable()
        {
            var connectionProfile = NetworkInformation.GetInternetConnectionProfile();
            return connectionProfile != null && connectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
        }
    }
}

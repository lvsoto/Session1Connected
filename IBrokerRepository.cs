using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session1Connected
{
    public interface IBrokerRepository
    {
        List<Broker> GetAllBrokers();

        void AddNewBroker(Broker brokerToAdd);

        void UpdateBroker(Broker brokerToUpdate);

        void RemoveBroker(Broker brokerToRemove);
    }

    
}

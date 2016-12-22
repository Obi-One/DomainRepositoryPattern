using System;
using ObiOne.DomainRepositoryPattern.Specialized.DI.DataContext;

namespace ObiOne.DomainRepositoryPattern.Specialized.DI.Infra{
    public static class CompanyEx{
        public static void AsException(this DIContext self, int Actual){
            self.AsException(0, Actual);
        }

        public static void AsException<T>(this DIContext self, T Success, T Actual)
        {
            if (!Equals(Success, Actual))
            {
                int lErrCode;
                string lErrMsg;
                self.SboCompany.GetLastError(out lErrCode, out lErrMsg);
                throw new ApplicationException($"{lErrCode} - {lErrMsg}");
            }
        }
    }
}
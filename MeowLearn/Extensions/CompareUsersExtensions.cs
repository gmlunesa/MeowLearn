using MeowLearn.Areas.Admin.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MeowLearn.Extensions
{
    public class CompareUsersExtensions : IEqualityComparer<UserModel>
    {
        public bool Equals(UserModel x, UserModel y)
        {
            if (y == null)
            {
                return false;
            }

            if (x.Id == y.Id)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode([DisallowNull] UserModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}

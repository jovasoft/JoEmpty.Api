using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete
{
    public class PersonalDal : EntityRepositoryBase<Personal, PostgresContext>, IPersonalDal
    {
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCHomeWork01.Models
{   
	public  class ReportRepository : EFRepository<Report>, IReportRepository
	{

	}

	public  interface IReportRepository : IRepository<Report>
	{

	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiMed.Clinic;

namespace ClinicScheduler1
{
    public enum CustomDateValue
    {
        ///<summary>
        ///Необходимо брать сегодняшнюю дату
        ///</summary>
        Today = 0,

        ///<summary>
        ///Необходимо брать вчерашнюю дату
        ///</summary>
        Yeasterday,

        ///<summary>
        ///Необходимо брать завтрашнюю дату
        ///</summary>
        Tomorrow,

        ///<summary>
        ///Необходимо брать дату на начало недели
        ///</summary>
        BeginWeek,

        ///<summary>
        ///Необходимо брать дату на конец недели
        ///</summary>
        EndWeek,

        ///<summary>
        ///Необходимо брать дату на начало месяца
        ///</summary>
        BeginMonth,

        ///<summary>
        ///Необходимо брать дату на конец месяца
        ///</summary>
        EndMonth,

        ///<summary>
        ///Необходимо брать дату на начало предыдущего месяца
        ///</summary>
        BeginPrevMonth,

        ///<summary>
        ///Необходимо брать дату на конец предыдущего месяца
        ///</summary>
        EndPrevMonth
    }

    public class ReportParameters
    {
        public List<Params> Parameters;
        public ReportParameters()
        {
            Parameters = new List<Params>();
        }
    }
}

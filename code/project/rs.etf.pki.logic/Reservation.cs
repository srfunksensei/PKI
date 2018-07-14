using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PKI_project.rs.etf.pki.logic
{
    class Reservation
    {
        private int startTime, endTime; // start and end time of
                                        // reservation 

        private string user,            // who made reservation
            classroomNum,              // number of classroom
            course,                     // for which course reservation was made
            date;                       // reservation for specific date

        
        
        public string User
        {
            get { return user; }
            set { user = value; }
        }

        public string ClassroomNum
        {
            get { return classroomNum; }
            set { classroomNum = value; }
        }

        public string Course
        {
            get { return course; }
            set { course = value; }
        }

        public int StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public int EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        // constructor
        public Reservation()
        {
            startTime = endTime = -1;

            classroomNum = user = course = date = "";
        }

        public Reservation(string username, string classroomNum, int startTime, int endTime, string date)
        {
            this.user = username;
            this.classroomNum = classroomNum;
            this.startTime = startTime;
            this.endTime = endTime;
            this.date = date;
        }
    }
}

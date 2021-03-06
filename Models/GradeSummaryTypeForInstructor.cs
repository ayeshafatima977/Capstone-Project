﻿namespace AZLearn.Models
{
    /// <summary>
    ///     Purpose of this class is to define a custom type for dat object retrieved to show in Grade Summary page for
    ///     Instructors;
    /// </summary>
    public class GradeSummaryTypeForInstructor
    {
        public GradeSummaryTypeForInstructor(string totalMarks, string marksInRequirement, string marksInChallenge,
            string totalTimeSpentOnHomework, string studentName, int studentId)
        {
            TotalMarks = totalMarks;
            MarksInRequirement = marksInRequirement;
            MarksInChallenge = marksInChallenge;
            TotalTimeSpentOnHomework = totalTimeSpentOnHomework;
            StudentName = studentName;
            StudentId = studentId;
        }

        public string TotalMarks { get; set; }
        public string MarksInRequirement { get; set; }
        public string MarksInChallenge { get; set; }
        public string TotalTimeSpentOnHomework { get; set; }
        public string StudentName { get; set; }
        public int StudentId { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using AZLearn.Data;
using AZLearn.Models;
using AZLearn.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AZLearn.Controllers
{
    public class GradeController : ControllerBase
    {
        /// <summary>
        ///     CreateGradingByStudentId
        ///     Description: Controller action that creates new gradings for a student
        ///     It expects below parameters, and would populate the students grades for a specific rubric / homework
        /// </summary>
        /// <param name="studentId">string provided from frontend, and parsed to int to match model property data type</param>
        /// <param name="gradings">
        ///     Dictionary with rubricId as key and Tuple as value, the Tuple has two parameters i.e., mark and
        ///     instructor comment
        /// </param>
        public static void CreateGradingByStudentId(string studentId,
                Dictionary<string, Tuple<string, string>> gradings)
            /*Dictionary<rubricId,Tuple<mark ,instructorComment>>*/
        {
            using var context = new AppDbContext();
            var parsedRubricId = 0;
            var parsedStudentId = 0;
            var parsedMark = 0;
            var exception = new ValidationException();

            #region Validation

            studentId = string.IsNullOrEmpty(studentId) || string.IsNullOrWhiteSpace(studentId)
                ? null
                : studentId.Trim();

            if (string.IsNullOrWhiteSpace(studentId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(studentId),
                    nameof(studentId) + " is null."));
            }
            else
            {
                if (!int.TryParse(studentId, out parsedStudentId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for studentId"));
                if (parsedStudentId > 2147483647 || parsedStudentId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("studentId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Users.Any(key => key.UserId == parsedStudentId && key.IsInstructor == false))
                    exception.ValidationExceptions.Add(new Exception("studentId does not exist"));
                else if (!context.Users.Any(key => key.UserId == parsedStudentId && key.Archive == false))
                    exception.ValidationExceptions.Add(new Exception("studentId is Archived"));
            }

            if (gradings.Count == 0)
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(gradings),
                    nameof(gradings) + " is null."));

            foreach (var (tempRubricId, (tempMark, tempInstructorComment)) in gradings)
            {
                var rubricId = string.IsNullOrEmpty(tempRubricId) || string.IsNullOrWhiteSpace(tempRubricId)
                    ? null
                    : tempRubricId.Trim();
                var mark = string.IsNullOrEmpty(tempMark) || string.IsNullOrWhiteSpace(tempMark)
                    ? null
                    : tempMark.Trim();

                var instructorComment =
                    string.IsNullOrEmpty(tempInstructorComment) || string.IsNullOrWhiteSpace(tempInstructorComment)
                        ? null
                        : tempInstructorComment.Trim();

                if (string.IsNullOrWhiteSpace(rubricId))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(rubricId),
                        nameof(rubricId) + " is null."));
                }
                else
                {
                    if (!int.TryParse(rubricId, out parsedRubricId))
                        exception.ValidationExceptions.Add(new Exception("Invalid value for rubricId"));
                    if (parsedRubricId > 2147483647 || parsedRubricId < 1)
                        exception.ValidationExceptions.Add(
                            new Exception("rubricId value should be between 1 & 2147483647 inclusive"));
                    else if (!context.Rubrics.Any(key => key.RubricId == parsedRubricId))
                        exception.ValidationExceptions.Add(new Exception("rubricId does not exist"));

                    else if (!context.Rubrics.Any(key => key.RubricId == parsedRubricId && key.Archive == false))
                        exception.ValidationExceptions.Add(new Exception("rubricId is Archived"));
                }

                /*To check whether Grade for  given Rubric id and student id already exists*/

                if (context.Grades.Any(key => key.StudentId == parsedStudentId && key.RubricId == parsedRubricId))
                    exception.ValidationExceptions.Add(new Exception(
                        "Grade Already Exists for this rubricId and studentId"));

                if (string.IsNullOrWhiteSpace(mark))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(mark),
                        nameof(mark) + " is null."));
                }
                else
                {
                    if (!int.TryParse(mark, out parsedMark))
                    {
                        exception.ValidationExceptions.Add(new Exception("Invalid value for mark"));
                    }
                    else
                    {
                        if (parsedMark > 999 || parsedMark < 0)
                            exception.ValidationExceptions.Add(
                                new Exception("Marks should be between 0 & 999 inclusive."));
                    }
                }

                if (!string.IsNullOrEmpty(instructorComment))
                    if (instructorComment.Length > 250)
                        exception.ValidationExceptions.Add(new Exception("Comment can only be 250 characters long."));

                if (exception.ValidationExceptions.Count > 0) throw exception;

                #endregion

                if (instructorComment == null)
                {
                    context.Grades.Add(new Grade
                    {
                        RubricId = parsedRubricId,
                        StudentId = parsedStudentId,
                        Mark = parsedMark
                    });
                }
                else if (instructorComment != null)
                {
                    instructorComment = instructorComment.ToLower();
                    context.Grades.Add(new Grade
                    {
                        RubricId = parsedRubricId,
                        StudentId = parsedStudentId,
                        Mark = parsedMark,
                        InstructorComment = char.ToUpper(instructorComment[0]) + instructorComment.Substring(1)
                    });
                }
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            context.SaveChanges();
        }

        /// <summary>
        ///     UpdateGradingByStudentId
        ///     Description: Controller action that updates new gradings for a student i.e., grades and instructor comments
        ///     It expects below parameters, and would populate the updated students grades for a specific rubric / homework
        /// </summary>
        /// <param name="studentId">string provided from frontend, and parsed to int to match model property data type</param>
        /// <param name="gradings">
        ///     Dictionary with rubricId as key and Tuple as value, the Tuple has two parameters i.e., mark and
        ///     instructor comment
        /// </param>
        public static void UpdateGradingByStudentId(string studentId,
            Dictionary<string, Tuple<string, string>> gradings)
        {
            var parsedRubricId = 0;
            var parsedStudentId = 0;
            var parsedMark = 0;
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            studentId = string.IsNullOrEmpty(studentId) || string.IsNullOrWhiteSpace(studentId)
                ? null
                : studentId.Trim();
            if (string.IsNullOrWhiteSpace(studentId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(studentId),
                    nameof(studentId) + " is null."));
            }
            else
            {
                if (!int.TryParse(studentId, out parsedStudentId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for studentId"));
                if (parsedStudentId > 2147483647 || parsedStudentId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("studentId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Users.Any(key => key.UserId == parsedStudentId && key.IsInstructor == false))
                    exception.ValidationExceptions.Add(new Exception("studentId does not exist"));
                else if (!context.Users.Any(key => key.UserId == parsedStudentId && key.Archive == false))
                    exception.ValidationExceptions.Add(new Exception("studentId is Archived"));
            }

            if (gradings.Count == 0)
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(gradings),
                    nameof(gradings) + " is null."));

            foreach (var (tempRubricId, (tempMark, tempInstructorComment)) in gradings)
            {
                var rubricId = string.IsNullOrEmpty(tempRubricId) || string.IsNullOrWhiteSpace(tempRubricId)
                    ? null
                    : tempRubricId.Trim();
                var mark = string.IsNullOrEmpty(tempMark) || string.IsNullOrWhiteSpace(tempMark)
                    ? null
                    : tempMark.Trim();

                var instructorComment =
                    string.IsNullOrEmpty(tempInstructorComment) || string.IsNullOrWhiteSpace(tempInstructorComment)
                        ? null
                        : tempInstructorComment.Trim();
                if (string.IsNullOrWhiteSpace(rubricId))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(rubricId),
                        nameof(rubricId) + " is null."));
                }
                else
                {
                    if (!int.TryParse(rubricId, out parsedRubricId))
                        exception.ValidationExceptions.Add(new Exception("Invalid value for rubricId"));
                    if (parsedRubricId > 2147483647 || parsedRubricId < 1)
                        exception.ValidationExceptions.Add(
                            new Exception("rubricId value should be between 1 & 2147483647 inclusive"));

                    else if (!context.Rubrics.Any(key => key.RubricId == parsedRubricId))
                        exception.ValidationExceptions.Add(new Exception("rubricId does not exist"));

                    else if (!context.Rubrics.Any(key => key.RubricId == parsedRubricId && key.Archive == false))
                        exception.ValidationExceptions.Add(new Exception("rubricId is Archived"));
                }

                /*To check whether Grade for  given Rubric id and student id already exists*/

                if (!context.Grades.Any(key => key.StudentId == parsedStudentId && key.RubricId == parsedRubricId))
                    exception.ValidationExceptions.Add(new Exception(
                        "Grade that you are trying to update doesn't exists for this rubricId and studentId"));
                if (string.IsNullOrWhiteSpace(mark))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(mark),
                        nameof(mark) + " is null."));
                }
                else
                {
                    if (!int.TryParse(mark, out parsedMark))
                    {
                        exception.ValidationExceptions.Add(new Exception("Invalid value for mark"));
                    }
                    else
                    {
                        if (parsedMark > 999 || parsedMark < 0)
                            exception.ValidationExceptions.Add(
                                new Exception("Marks should be between 0 & 999 inclusive."));
                    }
                }

                if (!string.IsNullOrEmpty(instructorComment))
                    if (instructorComment.Length > 250)
                        exception.ValidationExceptions.Add(new Exception("Comment can only be 250 characters long."));

                if (exception.ValidationExceptions.Count > 0) throw exception;

                #endregion

                var grade = context.Grades.Find(parsedRubricId, parsedStudentId);
                if (instructorComment != null)
                {
                    instructorComment = instructorComment.ToLower();
                    instructorComment = char.ToUpper(instructorComment[0]) + instructorComment.Substring(1);
                }

                grade.Mark = parsedMark;
                grade.InstructorComment = instructorComment;
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;
            context.SaveChanges();
        }

        /// <summary>
        ///     UpdateGradingByStudentId
        ///     Description: Controller action that updates new gradings for a student i.e., student comments
        ///     It expects below parameters, and would populate the updated student comments for a specific rubric / homework
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="studentComment">Dictionary with rubricId as key and studentComment as value</param>
        public static void UpdateGradingByStudentId(string studentId, Dictionary<string, string> studentComment)
        {
            var parsedRubricId = 0;
            var parsedStudentId = 0;
            var grade = new Grade();
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            studentId = string.IsNullOrEmpty(studentId) || string.IsNullOrWhiteSpace(studentId)
                ? null
                : studentId.Trim();

            if (string.IsNullOrWhiteSpace(studentId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(studentId),
                    nameof(studentId) + " is null."));
            }
            else
            {
                if (!int.TryParse(studentId, out parsedStudentId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for studentId"));
                if (parsedStudentId > 2147483647 || parsedStudentId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("studentId value should be between 1 & 2147483647 inclusive"));
                /*To check if the user is an instructor or student*/

                else if (!context.Users.Any(key => key.UserId == parsedStudentId && key.IsInstructor == false))
                    exception.ValidationExceptions.Add(new Exception("Student Id does not exist"));
                else if (!context.Users.Any(key => key.UserId == parsedStudentId && key.Archive == false))
                    exception.ValidationExceptions.Add(new Exception("Student Id is Archived"));
            }

            if (studentComment.Count == 0)
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(studentComment),
                    nameof(studentComment) + " is null."));

            foreach (var (tempRubricId, tempStudentComment) in studentComment)
            {
                var rubricId = string.IsNullOrEmpty(tempRubricId) || string.IsNullOrWhiteSpace(tempRubricId)
                    ? null
                    : tempRubricId.Trim();

                var comment = string.IsNullOrEmpty(tempStudentComment) || string.IsNullOrWhiteSpace(tempStudentComment)
                    ? null
                    : tempStudentComment.Trim();


                if (string.IsNullOrWhiteSpace(rubricId))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(rubricId),
                        nameof(rubricId) + " is null."));
                }
                else
                {
                    if (!int.TryParse(rubricId, out parsedRubricId))
                        exception.ValidationExceptions.Add(new Exception("Invalid value for rubricId"));
                    if (parsedRubricId > 2147483647 || parsedRubricId < 1)
                        exception.ValidationExceptions.Add(
                            new Exception("rubricId value should be between 1 & 2147483647 inclusive"));
                    else if (!context.Rubrics.Any(key => key.RubricId == parsedRubricId))
                        exception.ValidationExceptions.Add(new Exception("rubricId does not exist"));

                    else if (!context.Rubrics.Any(key => key.RubricId == parsedRubricId && key.Archive == false))
                        exception.ValidationExceptions.Add(new Exception("rubricId is Archived"));
                }

                /*To check whether Grade for given rubricId and studentId already exists*/

                if (!context.Grades.Any(key => key.StudentId == parsedStudentId && key.RubricId == parsedRubricId))
                    exception.ValidationExceptions.Add(new Exception(
                        "Grade that you are trying to update doesn't exists for this rubricId and studentId"));

                if (!string.IsNullOrEmpty(comment))
                    if (comment.Length > 250)
                        exception.ValidationExceptions.Add(new Exception("Comment can only be 250 characters long."));

                if (exception.ValidationExceptions.Count > 0) throw exception;

                #endregion

                if (comment != null)
                {
                    comment = comment.ToLower();
                    comment = char.ToUpper(comment[0]) + comment.Substring(1);
                }

                grade = context.Grades.Find(parsedRubricId, parsedStudentId);
                grade.StudentComment = comment;
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            context.SaveChanges();
        }

        /// <summary>
        ///     GetGradesByStudentId
        ///     This Action takes in Student Id and Homework Id and returns List of Grades associated to that student in the
        ///     specified Homework.
        /// </summary>
        /// <param name="studentId">Student Id</param>
        /// <param name="homeworkId">Homework Id</param>
        /// <returns>List of Grades associated with specified student for specified Homework</returns>
        public static List<Grade> GetGradesByStudentId(string studentId, string homeworkId)
        {
            var parsedStudentId = 0;
            var parsedHomeworkId = 0;
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            studentId = string.IsNullOrEmpty(studentId) || string.IsNullOrWhiteSpace(studentId)
                ? null
                : studentId.Trim();
            homeworkId = string.IsNullOrEmpty(homeworkId) || string.IsNullOrWhiteSpace(homeworkId)
                ? null
                : homeworkId.Trim();

            if (string.IsNullOrWhiteSpace(studentId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(studentId),
                    nameof(studentId) + " is null."));
            }
            else
            {
                if (!int.TryParse(studentId, out parsedStudentId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for studentId"));
                if (parsedStudentId > 2147483647 || parsedStudentId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("studentId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Users.Any(key => key.UserId == parsedStudentId && key.IsInstructor == false))
                    exception.ValidationExceptions.Add(new Exception("studentId does not exist"));
            }

            if (string.IsNullOrWhiteSpace(homeworkId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(homeworkId),
                    nameof(homeworkId) + " is null."));
            }
            else
            {
                if (!int.TryParse(homeworkId, out parsedHomeworkId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for homeworkId"));
                if (parsedHomeworkId > 2147483647 || parsedHomeworkId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("homeworkId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Homeworks.Any(key => key.HomeworkId == parsedHomeworkId))
                    exception.ValidationExceptions.Add(new Exception("homeworkId does not exist"));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            var grades = context.Grades.Include("Rubric.Homework")
                .Where(key => key.Rubric.HomeworkId == parsedHomeworkId && key.StudentId == parsedStudentId)
                .ToList();

            return grades;
        }

        /// <summary>
        ///     GetGradeSummaryForInstructor
        ///     Description: This action returns List of custom objects of data (related to a Homework and grades for all students
        ///     in specified
        ///     cohort) required in Grade Summary Screen for instructor.
        ///     The screen needs data as per the following Format:
        ///     Student Name, Total Marks, Marks Obtained in all requirements/ Total Requirement Marks, Marks obtained in all
        ///     challenges/ Total Challenge Marks
        /// </summary>
        /// <param name="cohortId">Cohort Id</param>
        /// <param name="homeworkId">Homework Id</param>
        /// <returns>list of grades for a particular homework for all students</returns>
        public static List<GradeSummaryTypeForInstructor> GetGradeSummaryForInstructor(string cohortId,
            string homeworkId)
        {
            var gradeSummaries = new List<GradeSummaryTypeForInstructor>();
            var parsedCohortId = 0;
            var parsedHomeworkId = 0;
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            cohortId = string.IsNullOrEmpty(cohortId) || string.IsNullOrWhiteSpace(cohortId) ? null : cohortId.Trim();
            homeworkId = string.IsNullOrEmpty(homeworkId) || string.IsNullOrWhiteSpace(homeworkId)
                ? null
                : homeworkId.Trim();

            if (string.IsNullOrWhiteSpace(cohortId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(cohortId),
                    nameof(cohortId) + " is null."));
            }
            else
            {
                if (!int.TryParse(cohortId, out parsedCohortId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for cohortId"));
                if (parsedCohortId > 2147483647 || parsedCohortId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("cohortId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Cohorts.Any(key => key.CohortId == parsedCohortId))
                    exception.ValidationExceptions.Add(new Exception("cohortId does not exist"));
                else if (!context.Users.Any(key => key.IsInstructor == false && key.CohortId == parsedCohortId))
                    exception.ValidationExceptions.Add(new Exception("There is no student registered in this cohort"));
            }

            if (string.IsNullOrWhiteSpace(homeworkId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(homeworkId),
                    nameof(homeworkId) + " is null."));
            }
            else
            {
                if (!int.TryParse(homeworkId, out parsedHomeworkId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for homeworkId"));
                if (parsedHomeworkId > 2147483647 || parsedHomeworkId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("homeworkId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Homeworks.Any(key => key.HomeworkId == parsedHomeworkId))
                    exception.ValidationExceptions.Add(new Exception("homeworkId does not exist"));
                else if (!string.IsNullOrWhiteSpace(cohortId) && int.TryParse(cohortId, out parsedCohortId))
                    if (!context.Homeworks.Any(key =>
                        key.HomeworkId == parsedHomeworkId && key.CohortId == parsedCohortId))
                        exception.ValidationExceptions.Add(new Exception("Homework does not exist for this cohort."));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            var studentsByCohort = UserController.GetStudentsByCohortId(cohortId);
            if (RubricController.GetRubricsByHomeworkId(homeworkId).Any())
            {
                /*rubricWeightByGroup is an array with first element- total weight of requirements, second element- total weight of challenges for a specified Homework*/

                var rubricWeightByGroup = RubricController.GetRubricsByHomeworkId(homeworkId)
                    .GroupBy(key => key.IsChallenge).Select(key => key.Sum(s => s.Weight)).ToArray();

                /*If there are no challenges, then weight of challenge = 0 to avoid NullValueException*/

                if (rubricWeightByGroup.Length == 1)
                    rubricWeightByGroup = rubricWeightByGroup.Concat(new[] {0}).ToArray();

                /*Loop to get GradeSummary for all students in a Cohort*/

                foreach (var student in studentsByCohort)
                {
                    string totalTimeSpentOnHomework;
                    GradeSummaryTypeForInstructor gradeSummary;
                    var studentName = student.Name;
                    var timesheet = TimesheetController.GetTimesheetByHomeworkId(homeworkId, $"{student.UserId}");
                    if (timesheet == null)
                        totalTimeSpentOnHomework = " ";
                    else
                        totalTimeSpentOnHomework = (timesheet.SolvingTime + timesheet.StudyTime).ToString();

                    var gradesOfStudent = GetGradesByStudentId($"{student.UserId}", homeworkId);

                    /*If grades do not exist for that student (in case Instructor has not added/marked grades for that student)- show empty string for grades */

                    if (gradesOfStudent.Count == 0)
                    {
                        gradeSummary = new GradeSummaryTypeForInstructor(" ", $" /{rubricWeightByGroup[0]}",
                            $" /{rubricWeightByGroup[1]}", totalTimeSpentOnHomework, studentName, student.UserId);
                    }
                    else
                    {
                        var total = gradesOfStudent.Sum(key => key.Mark);

                        /*Group marks of student in a homework by requirements and challenges and store them in array*/

                        var marksByGroup = gradesOfStudent.GroupBy(key => key.Rubric.IsChallenge)
                            .Select(key => key.Sum(s => s.Mark))
                            .ToArray();

                        /*In case there are no challenges, we will show 0/0 for challenges' marks*/

                        if (marksByGroup.Length == 1) marksByGroup = marksByGroup.Concat(new[] {0}).ToArray();


                        gradeSummary = new GradeSummaryTypeForInstructor($"{total}",
                            $"{marksByGroup[0]}/{rubricWeightByGroup[0]}",
                            $"{marksByGroup[1]}/{rubricWeightByGroup[1]}", totalTimeSpentOnHomework, studentName,
                            student.UserId);
                    }

                    gradeSummaries.Add(gradeSummary);
                }
            }
            else
            {
                var gradeSummary = new GradeSummaryTypeForInstructor(" ",
                    " ",
                    " ", " ", "No rubrics found for this homework ",
                    0);
                gradeSummaries.Add(gradeSummary);
            }

            return gradeSummaries;
        }

        /// <summary>
        ///     ArchiveGradingByStudentId
        ///     Description: This action archives a course by studentId and rubricId
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="rubrics"></param>
        public static void ArchiveGradingByStudentId(string studentId, List<string> rubrics)
        {
            var parsedRubricId = 0;
            var parsedStudentId = 0;
            var exception = new ValidationException();
            using var context = new AppDbContext();

            studentId = string.IsNullOrEmpty(studentId) || string.IsNullOrWhiteSpace(studentId)
                ? null
                : studentId.Trim();

            if (string.IsNullOrWhiteSpace(studentId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(studentId),
                    nameof(studentId) + " is null."));
            }
            else
            {
                if (!int.TryParse(studentId, out parsedStudentId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for studentId"));
                /*To check if user is an instructor or student*/

                else if (!context.Users.Any(key => key.UserId == parsedStudentId && key.IsInstructor == false))
                    exception.ValidationExceptions.Add(new Exception("studentId does not exist"));
            }

            #region Validation

            foreach (var tempRubricId in rubrics)
            {
                var rubricId = string.IsNullOrEmpty(tempRubricId) || string.IsNullOrWhiteSpace(tempRubricId)
                    ? null
                    : tempRubricId.Trim();


                if (string.IsNullOrWhiteSpace(rubricId))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(rubricId),
                        nameof(rubricId) + " is null."));
                }
                else
                {
                    if (!int.TryParse(rubricId, out parsedRubricId))
                        exception.ValidationExceptions.Add(new Exception("Invalid value for rubricId"));

                    else if (!context.Rubrics.Any(key => key.RubricId == parsedRubricId))
                        exception.ValidationExceptions.Add(new Exception("rubricId does not exist"));
                }

                /*To check whether Grade for given Rubric id and student id already exists*/

                if (!context.Grades.Any(key => key.StudentId == parsedStudentId && key.RubricId == parsedRubricId))
                    exception.ValidationExceptions.Add(new Exception(
                        "Grade that you are trying to archive doesn't exists for this rubricId and studentId"));
                else if (context.Grades.Any(key =>
                    key.StudentId == parsedStudentId && key.RubricId == parsedRubricId && key.Archive))
                    exception.ValidationExceptions.Add(new Exception(
                        "Grade that you are trying to archive for this rubricId and studentId is already archived"));

                #endregion

                if (exception.ValidationExceptions.Count > 0) throw exception;

                var grade = context.Grades.Find(parsedRubricId, parsedStudentId);
                grade.Archive = true;
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            context.SaveChanges();
        }
    }
}
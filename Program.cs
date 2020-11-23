using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AZLearn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Testing Controllers Actions

            #region Create Course

            /*CourseController.CreateCourseByCohortId("1", "1","React","React Basics","3","https://reactjs.org/tutorial/tutorial.html", "2020-08-10","2020-08-10" );*/
            /*Test Passed*/

            #endregion

            #region Assign CourseByCohort

            /* CourseController.AddCourseByCohortId("1","4");*/
            /*Test Passed (Was able to Assign CourseID 4 to Cohort 1 in Course Cohort*/

            #endregion

            #region Update Course

            /*For testing updated Description and Duration Hours*/
            /*CourseController.UpdateCourseById("3","1","React","React Props","5","https://reactjs.org/tutorial/tutorial.html");*/
            /*Test Passed*/

            #endregion

            #endregion Testing Controllers Action

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}
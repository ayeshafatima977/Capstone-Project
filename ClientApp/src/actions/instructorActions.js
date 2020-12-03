import axios from "axios";
import querystring from "querystring";  

export const cohortSummaryInstructor = () => {
  return async (dispatch) => {
    try {
      dispatch({ type: "COHORT_SUMMARY_INSTRUCTOR_REQUEST" });
      const { data } = await axios.get(
        "https://localhost:5001/application/getcohorts"
      );
      dispatch({
        type: "COHORT_SUMMARY_INSTRUCTOR_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COHORT_SUMMARY_INSTRUCTOR_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const cohortGet = (id) => {
  return async (dispatch) => {
    try {
      dispatch({ type: "COHORT_GET_REQUEST" });
      const { data } = await axios.get(
        "https://localhost:5001/application/getcohort",
        {
          params: { cohortId: id },
        }
      );
      dispatch({
        type: "COHORT_GET_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COHORT_GET_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const getHomeworkSummaryInstructor = (courseId, cohortId) => {
  return async (dispatch) => {
    try {
      dispatch({ type: "HOMEWORKSUMMARY_INSTRUCTOR_REQUEST" });
      const params = { courseId, cohortId };
      const { data } = await axios.request({
        url:
          "https://localhost:5001/application/homeworksummary?" +
          querystring.stringify(params),
        method: "get",
        data: params,
      });
      dispatch({
        type: "HOMEWORKSUMMARY_INSTRUCTOR_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "HOMEWORKSUMMARY_INSTRUCTOR_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const createCohort = (cohort) => {
  return async (dispatch, getState) => {
    try {
      dispatch({
        type: "COHORT_CREATE_REQUEST",
      });
      // const {
      //   userLogin: { userInfo },
      // } = getState();
      // const config = {
      //   headers: {
      //     "Content-Type": "application/json",
      //   },
      // };
      console.log(cohort);
      const params = {
        name: cohort.name,
        capacity: cohort.capacity,
        city: cohort.city,
        modeOfTeaching: cohort.modeOfTeaching,
        startDate: cohort.startDate,
        endDate: cohort.endDate,
      };
      const { data } = await axios.request({
        url:
          "https://localhost:5001/application/createcohort?" +
          querystring.stringify(params),
        method: "post",
        data: params,
      });

      dispatch({
        type: "COHORT_CREATE_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COHORT_CREATE_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const editCohort = (cohort) => {
  return async (dispatch, getState) => {
    try {
      dispatch({
        type: "COHORT_EDIT_REQUEST",
      });
      // const {
      //   userLogin: { userInfo },
      // } = getState();
      // const config = {
      //   headers: {
      //     Authorization: `Bearer ${userInfo.token}`,
      //   },
      // };
      const params = {
        cohortId: cohort.cohortId,
        name: cohort.name,
        capacity: cohort.capacity,
        city: cohort.city,
        modeOfTeaching: cohort.modeOfTeaching,
        startDate: cohort.startDate,
        endDate: cohort.endDate,
      };
      const { data } = await axios.request({
        url:
          "https://localhost:5001/application/updatecohort?" +
          querystring.stringify(params),
        method: "patch",
        data: params,
      });

      dispatch({
        type: "COHORT_EDIT_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COHORT_EDIT_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const archiveCohort = (id) => {
  return async (dispatch, getState) => {
    try {
      dispatch({
        type: "COHORT_ARCHIVE_REQUEST",
      });
      // const {
      //   userLogin: { userInfo },
      // } = getState();
      // const config = {
      //   headers: {
      //     Authorization: `Bearer ${userInfo.token}`,
      //   },
      // };
      const params = {
        cohortId: id,
      };
      const { data } = await axios.request({
        url:
          "https://localhost:5001/application/archivecohort?" +
          querystring.stringify(params),
        method: "patch",
        data: params,
      });

      dispatch({
        type: "COHORT_ARCHIVE_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COHORT_ARCHIVE_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const createCourse = (course) => {
  return async (dispatch, getState) => {
    try {
      dispatch({
        type: "COURSE_CREATE_REQUEST",
      });
      // const {
      //   userLogin: { userInfo },
      // } = getState();
      // const config = {
      //   headers: {
      //     Authorization: `Bearer ${userInfo.token}`,
      //   },
      // };
      const params = {
        name: course.courseName,
        description: course.description,
        durationHrs: course.hours,
      };
      const { data } = await axios.request({
        url:
          "https://localhost:5001/application/createcourse?" +
          querystring.stringify(params),
        method: "post",
        data: params,
      });

      dispatch({
        type: "COURSE_CREATE_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COURSE_CREATE_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const getCourse = (id) => {
  return async (dispatch) => {
    try {
      dispatch({ type: "COURSE_GET_REQUEST" });
      const { data } = await axios.get(
        "https://localhost:5001/application/getcourse",
        {
          params: { courseId: id },
        }
      );
      dispatch({
        type: "COURSE_GET_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COURSE_GET_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const getAllCourses = () => {
  return async (dispatch) => {
    try {
      dispatch({ type: "GET_ALL_COURSES_REQUEST" });
      const { data } = await axios.get(
        "https://localhost:5001/application/getcourses"
      );
      dispatch({
        type: "GET_ALL_COURSES_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "GET_ALL_COURSES_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const getAllInstructors = () => {
  return async (dispatch) => {
    try {
      dispatch({ type: "GET_ALL_INSTRUCTORS_REQUEST" });
      const { data } = await axios.get(
        "https://localhost:5001/application/getinstructors"
      );
      dispatch({
        type: "GET_ALL_INSTRUCTORS_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "GET_ALL_INSTRUCTORS_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const editCourse = (course) => {
  return async (dispatch, getState) => {
    try {
      dispatch({
        type: "COURSE_EDIT_REQUEST",
      });
      // const {
      //   userLogin: { userInfo },
      // } = getState();
      // const config = {
      //   headers: {
      //     Authorization: `Bearer ${userInfo.token}`,
      //   },
      // };
      const params = {
        courseId: course.courseId,
        name: course.name,
        description: course.description,
        durationHrs: course.durationHrs,
      };
      const { data } = await axios.request({
        url:
          "https://localhost:5001/application/updatecourse?" +
          querystring.stringify(params),
        method: "patch",
        data: params,
      });

      dispatch({
        type: "COURSE_EDIT_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COURSE_EDIT_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const archiveCourse = (id) => {
  return async (dispatch, getState) => {
    try {
      dispatch({
        type: "COURSE_ARCHIVE_REQUEST",
      });
      // const {
      //   userLogin: { userInfo },
      // } = getState();
      // const config = {
      //   headers: {
      //     Authorization: `Bearer ${userInfo.token}`,
      //   },
      // };
      const params = {
        courseId: id,
      };
      const { data } = await axios.request({
        url:
          "https://localhost:5001/application/archivecourse?" +
          querystring.stringify(params),
        method: "patch",
        data: params,
      });

      dispatch({
        type: "COURSE_ARCHIVE_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COURSE_ARCHIVE_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const getCoursesByCohortId = (id) => {
  return async (dispatch) => {
    try {
      dispatch({ type: "COURSES_GET_BY_COHORT_ID_REQUEST" });
      const { data } = await axios.get(
        "https://localhost:5001/application/getcoursesummary",
        {
          params: { cohortId: id },
        }
      );
      dispatch({
        type: "COURSES_GET_BY_COHORT_ID_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COURSES_GET_BY_COHORT_ID_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const manageCourseInstructor = () => {
  return async (dispatch) => {
    try {
      dispatch({ type: "MANAGE_COURSE_REQUEST" });
      // update the url later
      const { data } = await axios.get(
        "https://localhost:5001/application/getcourses"
      );
      dispatch({
        type: "MANAGE_COURSE_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "MANAGE_COURSE_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const assignCourse = (course) => {
  return async (dispatch, getState) => {
    try {
      dispatch({
        type: "COURSE_ASSIGN_REQUEST",
      });
      // const {
      //   userLogin: { userInfo },
      // } = getState();
      // const config = {
      //   headers: {
      //     Authorization: `Bearer ${userInfo.token}`,
      //   },
      // };
      const params = {
        cohortId: course.cohortId,
        courseId: course.courseId,
        instructorId: course.instructorId,
        startDate: course.startDate,
        endDate: course.endDate,
        resourcesLink: course.resourcesLink,
      };
      const { data } = await axios.request({
        url:
          "https://localhost:5001/application/assigncourse?" +
          querystring.stringify(params),
        method: "post",
        data: params,
      });

      dispatch({
        type: "COURSE_ASSIGN_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COURSE_ASSIGN_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const getAssignedCourse = (courseId, cohortId) => {
  return async (dispatch) => {
    try {
      dispatch({ type: "GET_ASSIGNED_COURSE_REQUEST" });
      const params = { courseId, cohortId };
      const { data } = await axios.request({
        url:
          "https://localhost:5001/application/getassignedcourse?" +
          querystring.stringify(params),
        method: "get",
        data: params,
      });
      dispatch({
        type: "GET_ASSIGNED_COURSE_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "GET_ASSIGNED_COURSE_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const editAssignedCourse = (course) => {
  return async (dispatch, getState) => {
    try {
      dispatch({
        type: "EDIT_ASSIGNED_COURSE_REQUEST",
      });
      // const {
      //   userLogin: { userInfo },
      // } = getState();
      // const config = {
      //   headers: {
      //     Authorization: `Bearer ${userInfo.token}`,
      //   },
      // };
      const params = {
        cohortId: course.cohortId,
        courseId: course.courseId,
        instructorId: course.instructorId,
        startDate: course.startDate,
        endDate: course.endDate,
        resourcesLink: course.resourcesLink,
      };
      const { data } = await axios.request({
        url:
          "https://localhost:5001/application/updateassignedcourse?" +
          querystring.stringify(params),
        method: "patch",
        data: params,
      });

      dispatch({
        type: "EDIT_ASSIGNED_COURSE_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "EDIT_ASSIGNED_COURSE_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

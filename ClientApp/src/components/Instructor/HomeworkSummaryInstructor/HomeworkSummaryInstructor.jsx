import React, { useEffect } from "react";
import { Table, Container, Button, Nav, Row, Col } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import {
  getHomeworkSummaryInstructor,
  getCoursesByCohortId,
  archiveHomework,
} from "../../../actions/instructorActions";
import { Link } from "react-router-dom";

const HomeworkSummaryInstructor = ({ match }) => {
  const cohortId = match.params.id;
  const courseId = match.params.courseId;
  const dispatch = useDispatch();
  useEffect(() => {
    // get cohort by id
    // populate the cohort data in here
    dispatch(getHomeworkSummaryInstructor(courseId, cohortId));
    dispatch(getCoursesByCohortId(cohortId));
  }, [dispatch]);
  const homeworkSummaryInstructor = useSelector(
    (state) => state.homeworkSummaryInstructor
  );
  const archiveHomeworkInstructorState = useSelector(
    (state) => state.archiveHomeworkInstructorState
  );

  const onArchive = (id) => {
    // dispatch(archiveHomework({ id }));
  };
  const { loading, error, homeworkSummary } = homeworkSummaryInstructor;
  const { courses } = useSelector((state) => state.getCoursesByCohortId);
  return (
    <React.Fragment>
      <Container>
        <Row>
          <Col xs={2}>
            <Nav defaultActiveKey="/home" className="flex-column">
              {courses.map((course, index) => (
                <Nav.Link
                  href={`/instructorhomework/${cohortId}/${course.item1.courseId}`}
                  key={index}
                >
                  {course.item1.name}
                </Nav.Link>
              ))}
            </Nav>
          </Col>
          <Col xs={10}>
            <Table>
              <thead>
                <tr>
                  <th>Homework Name</th>
                  <th>Due Date</th>
                  <th>Release Date</th>
                  <th>GitHub</th>
                  <th>Category</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {homeworkSummary
                  .filter((homework) => homework.archive == false)
                  .map((homework, index) => (
                    <tr key={index}>
                      <td>{homework.title}</td>
                      <td>{homework.dueDate}</td>
                      <td>{homework.releaseDate}</td>
                      <td>
                        <a target="_blank" href={homework.documentLink}>
                          GitHubLink
                        </a>
                      </td>
                      <td>
                        {homework.isAssignment ? "Assignment" : "Practice"}
                      </td>
                      <td>
                        <Link
                          to={`/gradingsummary/${homework.cohortId}/${homework.homeworkId}`}
                        >
                          Grades |{" "}
                        </Link>
                        <Link
                          to={`/homeworkviewinstructor/${homework.homeworkId}`}
                        >
                          Details
                        </Link>{" "}
                        | Edit |{" "}
                        <Link to={"#"} onClick={onArchive(homework.homeworkId)}>
                          Archive
                        </Link>
                      </td>
                    </tr>
                  ))}
              </tbody>
            </Table>
            <Button>Back</Button>{" "}
            <Button
              className="float-right"
              href={`/instructorcreatehomework/${cohortId}`}
            >
              Create
            </Button>
          </Col>
        </Row>
      </Container>
    </React.Fragment>
  );
};

export default HomeworkSummaryInstructor;

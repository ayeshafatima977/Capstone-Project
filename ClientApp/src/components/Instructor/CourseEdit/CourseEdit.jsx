import React, { useState, useEffect } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { editCourse, getCourse } from "../../../actions/instructorActions";
import Loader from "../../shared/Loader/Loader";
import Message from "../../shared/Message/Message";

const CourseEdit = ({ match, history }) => {
  const courseId = match.params.id;
  const [courseName, setCourseName] = useState("");
  const [hours, setHours] = useState("");
  const [description, setDescription] = useState("");
  const dispatch = useDispatch();
//(1) Add validation states
const [validated, setValidated] = useState(false);   
//----------------------------

// ! (10.1) Anti-tamper validation - States and Variables
const [validData, setValidData] = useState(false);
const [formSubmitted, setFormSubmitted] = useState(false);
let validFormData = false;
let formSubmitIndicator = false;
// ! ------------------------------------------------------

  const courseEdit = useSelector((state) => state.courseEdit);
  const getCourseDetail = useSelector((state) => state.getCourse);
  const { success } = courseEdit;
  const { loading, error, course } = getCourseDetail;

// ! (10.2) Anti-tamper validation - Validate (parameters)
function Validate(courseName,
  hours,  
  description
  ) {

  formSubmitIndicator = true;

  try {
      
    courseName = courseName.trim().toLowerCase();
    hours = hours.trim().toLowerCase();
    description = description.trim().toLowerCase();     

      if (!courseName) { validFormData = false; }
      else if (courseName.Length > 50) { validFormData = false; }
      else if (!hours) { validFormData = false; }
      else if (parseFloat(hours) > 999.99 || parseFloat(hours) < 0) { validFormData = false; console.log("hours: ", parseFloat(hours)); }
      else if (!description) { validFormData = false; console.log("description"); }
      else if (description.Length > 250) { validFormData = false; console.log("description length"); }      
     
      else { validFormData = true; console.log("All good :", validFormData); }
        }
    catch (Exception) {
      validFormData = false;
  }
};
// ! ------------------------------------------------------

  useEffect(() => {
    if (success) {
      dispatch({
        type: "COURSE_EDIT_RESET",
      });
    } else {
      if (!course || !course.name) {
        dispatch(getCourse(courseId));
      } else {
        setCourseName(course.name);
        setDescription(course.description);
        setHours(course.durationHrs);
      }
    }
  }, [dispatch, course, success]);

  const submitHandler = (e) => {
    //(2) Add form validation condition block if-else
    const form = e.currentTarget;
    if (form.checkValidity() === false) {
        e.preventDefault();
        e.stopPropagation();
    }
    setValidated(true);      
  //(3) Add business logic- No business Logic for now

    e.preventDefault();
  // ! (10.4) Anti-tamper validation - calling Validate     
  Validate(courseName,
    hours,
    description,
    );
  if (validFormData) {
    setValidData(validFormData);
    // ! ------------------------------------------------------

    console.log("edit course");
    dispatch(
      editCourse({
        courseId,
        name: courseName,
        description,
        durationHrs: hours,
      })
    );
  }
  else {
    // ! (10.5) Anti-tamper validation - Alert message conditions  
    setValidData(validFormData);
  }

// ! (10.6) Anti-tamper validation - Alert message conditions  
setFormSubmitted(formSubmitIndicator);
// ! 
};
  return (
    <React.Fragment>
      {loading ? (
        <Loader />
      ) : (
        <Container>
          <Row className="justify-content-md-center">
            <Col xs={12} md={6}>
              <h2>Course</h2>
              {success && (
                <Message variant="success">Update Successfully</Message>
              )}
               {/* (10.7) Anti-tamper validation - Alert message conditions   */}
               <p className=
                            {
                                formSubmitted ? (validData ? ((!loading && error) ? "alert alert-danger" :
                                    ((!loading && !error ) ? "alert alert-success" : "")) :
                                    "alert alert-danger") : ""
                            }
                            role="alert">
                            {
                                formSubmitted ? (validData ? ((!loading && error) ? "Unsuccessful attempt to create a cohort" :
                                    ((!loading && !error) ? "Cohort was successfully created" : "")) :
                                    "Error: Form were submitted with invalid data fields") : ""
                            }
                        </p>
                         {/* -----------------------------------------------  */}
              <Form noValidate validated={validated} onSubmit={submitHandler}>
                <Form.Group controlId="CourseName">
                  <Form.Label>Course Name</Form.Label>

                  <Form.Control
                    required
                    type="text"
                    maxLength ="50"
                    value={courseName}
                    onChange={(e) => setCourseName(e.target.value)}
                  ></Form.Control>
                  <Form.Control.Feedback type="invalid">
                    Please enter a course name.
                </Form.Control.Feedback>
                </Form.Group>
                <Form.Group controlId="Hours">
                  <Form.Label className="mr-5">Hours</Form.Label>
                  
                  <Form.Control
                    required
                    type="number"
                    min={0}
                    max={999.99}
                    step="0.25"
                    value={hours}
                    onChange={(e) => setHours(e.target.value)}
                  ></Form.Control>
                  <Form.Control.Feedback type="invalid">
                    Please fill in the Hours field.
                  </Form.Control.Feedback>
                  </Form.Group>
                  <Form.Group controlId="Description">
                  <Form.Label>Description</Form.Label>

                  <Form.Control as="textarea"
                    required
                    
                    maxlength="250"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                  ></Form.Control>
                  <Form.Control.Feedback type="invalid">
                    Please enter description for Course.
                </Form.Control.Feedback>
                </Form.Group>
                <button type="button" className="btn btn-link">
                  Back
                </button>{" "}
                <Button type="submit" className="float-right">
                  Save
                </Button>
              </Form>
            </Col>
          </Row>
        </Container>
      )}
    </React.Fragment>
  );
};

export default CourseEdit;

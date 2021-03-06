import React from "react";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Form, Button, Row, Col, Container, Modal } from "react-bootstrap";
import {
    registerUser,
    cohortSummaryInstructor,
} from "../../../actions/instructorActions";
import Loader from "../../shared/Loader/Loader";

const Register = ({ history }) => {
    const dispatch = useDispatch();
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [cohort, setCohort] = useState("");
    const [isInstructor, setIsInstructor] = useState(false);
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    const handleChange = () => {
        setIsInstructor(!isInstructor);
    };
    useEffect(() => {
            dispatch(cohortSummaryInstructor());
        },
        [dispatch]);
    const { success, error } = useSelector((state) => state.userRegisterState);
    const { cohorts, loading } = useSelector(
        (state) => state.cohortSummaryInstructor
    );
    /*Add validation states*/
    const [validated, setValidated] = useState(false);

/*Anti-tamper validation - States and Variables*/

    const [validData, setValidData] = useState(false);
    const [formSubmitted, setFormSubmitted] = useState(false);
    let validFormData = false;
    let formSubmitIndicator = false;
/* Anti-tamper validation - Validate (parameters)*/

    function Validate(name, email, password, cohort, isInstructor) {
        formSubmitIndicator = true;

        try {
            name = name.trim().toLowerCase();
            email = email.trim().toLowerCase();
            password = password.trim();
            cohort = cohort.trim().toLowerCase();
            isInstructor = String(isInstructor).trim().toLowerCase();
            if (!name) {
                validFormData = false;
            } else if (name.Length > 50) {
                validFormData = false;
            } else if (parseInt(cohort) > 2147483647 || parseInt(cohort) < 1) {
                validFormData = false;
            } else if (!isInstructor) {
                validFormData = false;
            } else if (!(isInstructor === "true" || isInstructor === "false")) {
                validFormData = false;
            } else if (!email) {
                validFormData = false;
            } else if (email.Length > 50) {
                validFormData = false;
            } else if (!password) {
                validFormData = false;
            } else if (password.Length < 8 || password.Length > 250) {
                validFormData = false;
            } else {
                if (
                    !/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-.]).{8,}$/.test(
                        password
                    )
                ) {
                    validFormData = false;
                } else if (!/^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(email)) {
                    validFormData = false;
                } else {
                    validFormData = true;
                }
            }
        } catch (Exception) {
            validFormData = false;
        }
    }

    const submitHandler = (e) => {
        /*Form validation condition block if-else*/

        const form = e.currentTarget;
        if (form.checkValidity() === false) {
            e.preventDefault();
            e.stopPropagation();
        }
        setValidated(true);
        e.preventDefault();

        /*Anti-tamper validation - calling Validate*/

        Validate(name, email, password, cohort, isInstructor);
        if (validFormData) {
            setValidData(validFormData);
            if (isInstructor) {
                handleShow();
            } else {
                e.preventDefault();
                dispatch(
                    registerUser({
                        cohort,
                        name,
                        password,
                        email,
                        isInstructor,
                    })
                );
            }
        } else {

            /*Anti-tamper validation - Alert message conditions*/

            setValidData(validFormData);
        }
        /*Anti-tamper validation - Alert message conditions*/

        setFormSubmitted(formSubmitIndicator);
    };

    const handleRegisterInstructor = () => {
        dispatch(
            registerUser({
                cohort,
                name,
                password,
                email,
                isInstructor,
            })
        );
        handleClose();
    };

    const goBack = () => {
        history.goBack();
    };
    return (
        <React.Fragment>
            {loading
                ? (
                    <Loader/>
                )
                : (
                    <Container>
                        <Row className="justify-content-md-center">
                            <Col xs={12} md={6}>
                                <h2>Register</h2>
                                <p
                                    className={
                  formSubmitted
                    ? validData
                      ? !loading && error
                        ? "alert alert-danger"
                        : !loading && !error && success
                        ? "alert alert-success"
                        : ""
                      : "alert alert-danger"
                    : ""
                }
                                    role="alert">
                                    {formSubmitted
                    ? validData
                    ? !loading && error
                    ? `Unsuccessful attempt to register user. ${error.data}`
                    : !loading && !error && success
                    ? "Successfully registered the user"
                    : ""
                    : "Error: Form was submitted with invalid data fields"
                    : ""}
                                </p>
                                <Form noValidate validated={validated} onSubmit={submitHandler}>
                                    <Form.Group controlId="name">
                                        <Form.Label>Name</Form.Label>
                                        <Form.Control
                                            required
                                            type="text"
                                            maxLength="50"
                                            value={name}
                                            onChange={(e) => setName(e.target.value)}>
                                        </Form.Control>
                                        <Form.Control.Feedback type="invalid">
                                            Please enter a name.
                                            <p>Max. 50 characters allowed</p>
                                        </Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group controlId="email">
                                        <Form.Label>Email Address</Form.Label>
                                        <Form.Control
                                            required
                                            type="email"
                                            maxLength="50"
                                            value={email}
                                            onChange={(e) => setEmail(e.target.value)}>
                                        </Form.Control>
                                        <Form.Control.Feedback type="invalid">
                                            Please enter a valid email.
                                            <p>e.g. youremailaddress@domain.com </p>
                                        </Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group controlId="password">
                                        <Form.Label>Password</Form.Label>
                                        <Form.Control
                                            required
                                            type="password"
                                            minLength="8"
                                            pattern="(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}"
                                            value={password}
                                            onChange={(e) => setPassword(e.target.value)}>
                                        </Form.Control>
                                        <Form.Control.Feedback type="invalid">
                                            Please enter a valid password.
                                            <p>
                                                Password must have at least: one upper case letter,one
                                                lower case letter, one digit , one special character,
                                                minimum 8 characters in length.
                                            </p>
                                        </Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group controlId="cohort">
                                        <Form.Label>Cohort</Form.Label>
                                        <Form.Control
                                            as="select"
                                            value={cohort}
                                            onChange={(e) => setCohort(e.target.value)}>
                                            {cohorts.filter((item) => item.archive == false).map((item, index) => (
                        <option key={index} value={item.cohortId}>
                        {item.name}
                      </option>
                    ))}
                                        </Form.Control>
                                    </Form.Group>
                                    <Form.Group controlId="isInstructor">
                                        <Form.Check
                                            type="checkbox"
                                            label="Instructor"
                                            value={isInstructor}
                                            onChange={handleChange}/>
                                    </Form.Group>
                                    <button type="button" className="btn btn-link" onClick={goBack}>
                                        Back
                                    </button>{" "}
                                    <Button className="float-right" type="submit" variant="primary">
                                        {" "}
                                        Register
                                    </Button>
                                </Form>
                            </Col>
                        </Row>
                    </Container>
                )}
            <Modal show={show} onHide={handleClose}>
                <Modal.Body>
                    You are giving this user, instructor (Create/Edit/Archive) privileges.
                    <p>Are you sure?</p>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleRegisterInstructor}>
                        Proceed
                    </Button>
                    <Button variant="primary" onClick={handleClose}>
                        Cancel
                    </Button>
                </Modal.Footer>
            </Modal>
        </React.Fragment>
    );
};

export default Register;
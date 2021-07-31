import React, { useEffect, useState } from "react";
import MaterialTable from "material-table";
import axios from "axios";
import { useHistory } from "react-router-dom";
import { Modal, Button } from "react-bootstrap";
const Home = () => {
	const [data, setData] = useState([]);
	const [expression, setExpression] = useState([]);
	const [dummyLoad, setDummyLoad] = useState(0);
	const [userData, setUserData] = useState({});
	const [show, setShow] = useState(false);

	const handleClose = () => setShow(false);
	const handleShow = async (userId) => {
		await getUserDetails(userId);
		setShow(true);
	};
	let history = useHistory();
	useEffect(() => {
		const config = {
			headers: {
				Authorization: `Bearer ${localStorage.getItem("token")}`
			}
		};
		axios
			.get("https://localhost:1000/api/questions", config)
			.then((response) => {
				// If request is good...
				let xData = response.data;
				xData.sort(function (a, b) {
					var c = new Date(a.createdOn);
					var d = new Date(b.createdOn);
					return d - c;
				});
				setData(xData);
			})
			.catch((error) => {
				console.log("error " + error);
			});
	}, [dummyLoad]);
	const questionSendHandler = () => {
		const config = {
			headers: {
				Authorization: `Bearer ${localStorage.getItem("token")}`
			}
		};

		const bodyParameters = {
			UserId: localStorage.getItem("userId"),
			UserName: localStorage.getItem("userName"),
			Expression: expression
		};
		axios
			.post("https://localhost:1000/api/questions", bodyParameters, config)
			.then((response) => {
				setDummyLoad(dummyLoad + 1);
			})
			.catch((err) => {
				console.log(err);
			});
	};

	const getUserDetails = (userId) => {
		const config = {
			headers: {
				Authorization: `Bearer ${localStorage.getItem("token")}`
			}
		};
		axios
			.get("https://localhost:1000/api/users/" + userId, config)
			.then((response) => {
				setUserData(response.data);
			})
			.catch((error) => {
				return "An error occurred";
			});
	};

	const logOutHandler = () => {
		localStorage.clear();
		history.push("/login");
	};
	return (
		<div>
			<nav class="navbar navbar-expand-lg navbar-light bg-light">
				<a class="navbar-brand">MathBot</a>
				<div class="collapse navbar-collapse" id="navbarTogglerDemo02">
					<ul class="navbar-nav mr-auto mt-2 mt-lg-0"></ul>
					<div
						class="form-inline my-2 my-lg-0"
						onClick={logOutHandler}
						style={{ cursor: "pointer" }}>
						Logout
					</div>
				</div>
			</nav>
			<div className="container-fluid">
				<div className="row" style={{ marginTop: "80px" }}>
					<div className="col">
						<div className="card">
							<div className="card-body">
								<h5 className="card-title">Add Question</h5>
								<div className="input-group mb-3">
									<input
										type="text"
										class="form-control"
										placeholder="Please enter expression with only +, -, /, * operators"
										aria-label="Expression"
										aria-describedby="basic-addon2"
										value={expression}
										onChange={(e) => {
											setExpression(e.target.value);
										}}
									/>
									<div class="input-group-append">
										<button
											class="btn btn-outline-secondary"
											type="button"
											onClick={questionSendHandler}>
											Send Question
										</button>
									</div>
								</div>
							</div>
						</div>
					</div>
					<div className="col">
						<div className="card">
							<div className="card-body">
								<h5 className="card-title">Question List</h5>
								<div style={{ maxWidth: "100%" }}>
									<MaterialTable
										columns={[
											{
												title: "Username",
												field: "userName",
												render: (row) => (
													<div
														style={{ cursor: "pointer" }}
														onClick={() => {
															handleShow(row.userId);
														}}>
														{row.userName}
													</div>
												)
											},
											{ title: "Expression", field: "expression" },
											{ title: "Result", field: "result" }
										]}
										data={data}
										title="Questions"
									/>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
			<Modal show={show} onHide={handleClose}>
				<Modal.Header closeButton>
					<Modal.Title>User Info</Modal.Title>
				</Modal.Header>
				<Modal.Body>
					<ul class="list-group">
						<li class="list-group-item d-flex  align-items-center">
							<span style={{ marginRight: "10px" }} class="badge badge-primary badge-pill">
								Username
							</span>
							{userData.userName}
						</li>
						<li class="list-group-item d-flex  align-items-center">
							<span style={{ marginRight: "10px" }} class="badge badge-primary badge-pill">
								First name
							</span>
							{userData.firstName}
						</li>
						<li class="list-group-item d-flex  align-items-center">
							<span style={{ marginRight: "10px" }} class="badge badge-primary badge-pill">
								Last name
							</span>
							{userData.lastName}
						</li>
						<li class="list-group-item d-flex  align-items-center">
							<span style={{ marginRight: "10px" }} class="badge badge-primary badge-pill">
								Email
							</span>
							{userData.email}
						</li>
						<li class="list-group-item d-flex  align-items-center">
							<span style={{ marginRight: "10px" }} class="badge badge-primary badge-pill">
								Date of birth
							</span>
							{userData.dateOfBirth}
						</li>
					</ul>
				</Modal.Body>
				<Modal.Footer>
					<Button variant="secondary" onClick={handleClose}>
						Close
					</Button>
				</Modal.Footer>
			</Modal>
		</div>
	);
};

export default Home;

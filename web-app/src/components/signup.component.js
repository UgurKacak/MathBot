import React, { useState } from "react";
import axios from "axios";
import ReactFullPageLoading from 'react-fullpage-loading';
const SignUp = () => {
	const [firstName, setFirstName] = useState("");
	const [lastName, setLastName] = useState("");
	const [email, setEmail] = useState("");
	const [dateOfBirth, setDateOfBirth] = useState("");
	const [userName, setUserName] = useState("");
	const [password, setPassword] = useState("");
	const [alert, setAlert] = useState("");
	const [msg, setMsg] = useState("");
	const [loading, setLoading] = useState(false);

	const RegisterHandler = () => {
		setAlert("");
		const bodyParameters = {
			userName,
			email,
			password,
			dateOfBirth,
			firstName,
			lastName
		};

		setLoading(true);
		axios
			.post("http://localhost:1000/api/users", bodyParameters)
			.then((response) => {
				setMsg("Registered successfully.Please login now.");
				setLoading(false);
			})
			.catch((err) => {
				if (err.response.data.title !== undefined) {
					setAlert(err.response.data.title);
					setLoading(false);
				} else {
					setAlert(err.response.data);
					setLoading(false);
				}
			});
	};
	return (
		<div className="outer">
			{loading ? <ReactFullPageLoading /> : null}
			<nav className="navbar navbar-expand-lg navbar-light bg-light">
				<a className="navbar-brand">MathBot</a>
				<div className="collapse navbar-collapse" id="navbarTogglerDemo02">
					<ul className="navbar-nav mr-auto mt-2 mt-lg-0">
						<li className="nav-item">
							<a className="nav-link" href="/login">
								Login
							</a>
						</li>
						<li className="nav-item">
							<a className="nav-link" href="/signup">
								Sign Up
							</a>
						</li>
					</ul>
				</div>
			</nav>
			<div className="inner" style={{marginTop:"30px"}}>
				<h3>Register</h3>
				<div className="alert alert-danger" role="alert" hidden={alert === "" ? true : false}>
					{alert}
				</div>
				<div className="success alert-success" role="alert" hidden={msg === "" ? true : false}>
					{msg}
				</div>
				<div className="form-group">
					<label>First name</label>
					<input
						type="text"
						className="form-control"
						placeholder="First name"
						value={firstName}
						onChange={(e) => {
							setFirstName(e.target.value);
						}}
					/>
				</div>

				<div className="form-group">
					<label>Last name</label>
					<input
						type="text"
						className="form-control"
						placeholder="Last name"
						value={lastName}
						onChange={(e) => {
							setLastName(e.target.value);
						}}
					/>
				</div>

				<div className="form-group">
					<label>Email</label>
					<input
						type="email"
						className="form-control"
						placeholder="Enter email"
						value={email}
						onChange={(e) => {
							setEmail(e.target.value);
						}}
					/>
				</div>

				<div className="form-group">
					<label>Username</label>
					<input
						type="text"
						className="form-control"
						placeholder="Enter username"
						value={userName}
						onChange={(e) => {
							setUserName(e.target.value);
						}}
					/>
				</div>

				<div className="form-group">
					<label>Date of birth</label>
					<input
						type="text"
						className="form-control"
						placeholder="Enter date of birth dd/mm/yyyy"
						value={dateOfBirth}
						onChange={(e) => {
							setDateOfBirth(e.target.value);
						}}
					/>
				</div>

				<div className="form-group">
					<label>Password</label>
					<input
						type="password"
						className="form-control"
						placeholder="Enter password"
						value={password}
						onChange={(e) => {
							setPassword(e.target.value);
						}}
					/>
				</div>

				<button type="submit" className="btn btn-dark btn-lg btn-block" onClick={RegisterHandler}>
					Register
				</button>
			</div>
		</div>
	);
};

export default SignUp;

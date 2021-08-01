import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import axios from "axios";
import ReactFullPageLoading from 'react-fullpage-loading';
const Login = () => {
	let history = useHistory();
	const [userName, setUserName] = useState("");
	const [password, setPassword] = useState("");
	const [alert, setAlert] = useState("");
	const [loading, setLoading] = useState(false);
	const LoginHandler = () => {
		setAlert("");
		const bodyParameters = {
			UserName: userName,
			Password: password
		};

		setLoading(true);
		axios
			.post("http://localhost:1000/api/auths", bodyParameters)
			.then((response) => {
				console.log(response.data);
				localStorage.setItem("token", response.data.accessToken);
				localStorage.setItem("userId", response.data.userId);
				localStorage.setItem("userName", response.data.userName);
				setLoading(false);
				history.push("/home");
				
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
				<a className="navbar-brand">
					MathBot
				</a>
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
			<div className="inner">
				<h3>Log in</h3>
				<div className="alert alert-danger" role="alert" hidden={alert === "" ? true : false}>
					{alert}
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

				<button type="submit" className="btn btn-dark btn-lg btn-block" onClick={LoginHandler}>
					Sign in
				</button>
			</div>
		</div>
	);
};

export default Login;

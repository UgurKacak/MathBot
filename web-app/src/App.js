import React, { useState } from "react";
import "../node_modules/bootstrap/dist/css/bootstrap.min.css";
import "./App.css";
import { BrowserRouter, Route, Switch } from "react-router-dom";

import Login from "./components/login.component";
import SignUp from "./components/signup.component";
import Home from "./components/home.component";

const App = () => {
	const [token, setToken] = useState(localStorage.getItem("token"));
	return (
		<BrowserRouter>
			<Switch>
				<Route exact path="/" component={token !== null ? () => <Home /> : () => <Login />} />
				<Route exact path="/login">
					<Login />
				</Route>
				<Route exact path="/signup">
					<SignUp />
				</Route>
				<Route exact path="/home">
					<Home />
				</Route>
			</Switch>
		</BrowserRouter>
	);
};

export default App;

import React, { Component } from "react";
import AuthService from "../services/authService";

const AuthContext = React.createContext({
    signinRedirectCallback: () => ({}),
    logout: () => ({}),
    signoutRedirectCallback: () => ({}),
    isAuthenticated: () => ({}),
    signinRedirect: () => ({}),
    signinSilentCallback: () => ({}),
    createSigninRequest: () => ({})
});

export const authService = new AuthService();
export const AuthConsumer = AuthContext.Consumer;
export class AuthProvider extends Component {
    authService;
    constructor(props) {
        super(props);
        this.authService = authService;
    }
    render() {
        return <AuthContext.Provider value={this.authService}>{this.props.children}</AuthContext.Provider>;
    }
}

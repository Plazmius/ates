import * as React from "react";
import { Route, Switch } from "react-router-dom";
import { Callback } from "../components/auth/callback";
import { Logout } from "../components/auth/logout";
import { LogoutCallback } from "../components/auth/logoutCallback";
import { PrivateRoute } from "./PrivateRoute";
import { Register } from "../components/auth/register";
import { SilentRenew } from "../components/auth/silentRenew";
import { Dashboard } from "../pages/Dashboard";
import {NotFound} from "../pages/NotFound";

export const Routes = () => (
    <Switch>
        <Route exact={true} path="/signin-oidc" component={Callback} />
        <Route exact={true} path="/logout" component={Logout} />
        <Route
            exact={true}
            path="/logout/callback"
            component={LogoutCallback}
        />
        <Route
            exact={true}
            path="/:lng(en|es|de|fr|pt|it)/register/:form?"
            component={Register}
        />
        <Route exact={true} path="/silentrenew" component={SilentRenew} />
        <PrivateRoute path="/dashboard" component={Dashboard} />
        <Route component={NotFound} />
    </Switch>
);

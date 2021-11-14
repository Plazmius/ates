/* /src/components/auth/logoutCallback.jsx */

import React from "react";
import { AuthConsumer } from "../../providers/AuthProvider";

export const LogoutCallback = () => (
    <AuthConsumer>
        {({ signoutRedirectCallback }) => {
            signoutRedirectCallback();
            return <span>loading</span>;
        }}
    </AuthConsumer>
);
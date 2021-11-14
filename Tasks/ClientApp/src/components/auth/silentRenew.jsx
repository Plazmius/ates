/* /src/components/auth/silentRenew.jsx */

import React from "react";

import { AuthConsumer } from "../../providers/AuthProvider";

export const SilentRenew = () => (
    <AuthConsumer>
        {({ signinSilentCallback }) => {
            signinSilentCallback();
            return <span>loading</span>;
        }}
    </AuthConsumer>
);